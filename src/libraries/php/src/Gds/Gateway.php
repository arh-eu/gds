<?php
/*
 * Copyright 2020 ARH Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace App\Gds;

use App\Gds\Message\Response;
use App\Gds\Exception\GatewayTimeoutException;
use Psr\Log\LoggerInterface;

/**
 * Acts as a websocket gateway between application and a remote GDS instance.
 *
 * @author bordacs
 */
class Gateway
{
    /**
     * @var mixed[]
     */
    private $options;

    /**
     * @var \React\EventLoop\LoopInterface
     */
    private $eventLoop;

    /**
     * Indicates whether event loop running.
     *
     * @var bool
     */
    private $running = false;

    /**
     * @var \App\Gds\Connection
     */
    private $connection;

    /**
     * Indicates, that gateway was cancelled. Cancelled gateway cannot resume.
     *
     * @var bool
     */
    private $cancelled = false;

    /**
     * Indexed by endpoint object's hash
     *
     * @var \App\Gds\EndpointInterface[]
     */
    private $endpoints;

    /**
     * @var \SplQueue<\App\Gds\Message\Message>
     */
    private $messageQueue;

    /**
     * Endpoints object's hashes, indexed by message ids.
     *
     * @var string[]
     */
    private $messageRegistry;

    private $messageQueueTimer;

    /**
     * @var \Psr\Log\LoggerInterface
     */
    private $logger;

    /**
     *
     * @param \App\Gds\ConnectionInterface $connection
     * @param array $options
     */
    public function __construct(ConnectionInterface $connection, array $options, LoggerInterface $logger)
    {
        $this->connection = $connection;
        $this->eventLoop = $connection->eventLoop;
        $this->logger = $logger;

        $this->connection->on('messageReceived', function(\App\Gds\Message\Response $response) {
            $this->onMessageReceived($response);
        });

        if(is_null($options)) 
        {
            $this->options = array();
        }
        else
        {
            $this->options = $options;
        }        
        if(!array_key_exists('timeout', $this->options))
        {
            $this->options['timeout'] = 10;
        }

        $this->endpoints = array();
        $this->messageQueue = new \SplQueue();
        $this->messageRegistry = array();
    }

    public function __destruct()
    {
        if (false === $this->cancelled) {
            $this->cancel();
        }
    }

    public function onMessageReceived(\App\Gds\Message\Response $response)
    {
        $endpoint = $this->endpoints[$this->messageRegistry[$response->getMessageId()]];

        if (null === $endpoint) {
            $this->logger->warning('[GDS.Gateway] Unexpected message received: no endpoint found.', array('messageId'=>$response->getMessageId(), 'type'=>$response->getDataType()));
            throw new \RuntimeException('Unexpected message received: no endpoint found (message id: '.$response->getMessageId().')');
        }

        if (Response::STATUS_OK != $response->getStatus()) {
            if (!$endpoint->waitForMessage()) {
                $this->handleGlobalError($response->getStatus(), $response->getExceptionMessage());
            }
            else {
                if (!$endpoint->handleGlobalError($response->getStatus(), $response->getExceptionMessage(), $response->getData())) {
                    $this->handleGlobalError($response->getStatus(), $response->getExceptionMessage());
                }

            }
        }
        
        if ($endpoint->waitForMessage()) {
            $endpoint->emit('response', array($response));
        }
        else {
            $this->logger->error('[GDS.Gateway] Unexpected message received: endpoint is terminated.', array('endpoint'=>get_class($endpoint), 'messageId'=>$response->getMessageId()));
            throw new \RuntimeException('Unexpected message received: endpoint is terminated (endpoint: '. get_class($endpoint).', message id: '.$response->getMessageId().')');
        }
    }

    private function handleGlobalError(int $statusCode, string $exceptionMessage)
    {
        if (400 <= $statusCode) {

            $e = null;

            switch ($statusCode) {
                case 400: $e = new \App\Gds\Exception\BadRequestException($exceptionMessage); break;
                case 401: $e = new \App\Gds\Exception\UnauthorizedException($exceptionMessage); break;
                case 403: $e = new \App\Gds\Exception\AccessDeniedException($exceptionMessage); break;
                case 406: $e = new \App\Gds\Exception\NotAcceptableException($exceptionMessage); break;
                case 408: $e = new \App\Gds\Exception\RequestTimeoutException($exceptionMessage); break;
                case 409: $e = new \App\Gds\Exception\ConflictException($exceptionMessage); break;
                case 412: $e = new \App\Gds\Exception\PreconditionFailedException($exceptionMessage); break;
                case 429: $e = new \App\Gds\Exception\TooManyRequestsException($exceptionMessage); break;
                case 500: $e = new \App\Gds\Exception\InternalServerErrorException($exceptionMessage); break;
                case 509: $e = new \App\Gds\Exception\BandwidthLimitExceededException($exceptionMessage); break;
                default: $e = new \App\Gds\Exception\UnhandledErrorException($statusCode, $exceptionMessage);
            }

            $this->cancel();

            throw $e;
        }
    }


    /**
     * @param Message\Message $message
     * @param \App\Gds\EndpointInterface $endpoint
     * @throws \Exception
     */
    public function send(\App\Gds\Message\Message $message, \App\Gds\EndpointInterface $endpoint): void
    {
        if (!($endpoint instanceof EndpointInterface)) {
            throw new \InvalidArgumentException('Endpoint must implement '.EndpointInterface::class.'.');
        }

        $this->logger->info('[GDS.Gateway] Enqueue message.', array('messageId'=>$message->getId(), 'endpoint'=>get_class($endpoint)));

        $this->messageQueue->enqueue($message);

        $oid = spl_object_hash($endpoint);

        if (!array_key_exists($oid, $this->endpoints)) {
            $this->endpoints[$oid] = $endpoint;
            $endpoint->on('response', array($endpoint, 'onResponseSuccess'));
        }

        $this->messageRegistry[$message->getId()] = $oid;

        if (!$this->running) {
            $this->start();
        }
    }

    /**
     * @throws \Exception
     */
    private function start()
    {
        if ($this->cancelled) {
            throw new \RuntimeException('Gateway was previously cancelled');
        }

        try {
            if (!$this->running) {
                if (!$this->connection->established()) {
                    $this->connection->establish();

                    $this->eventLoop->addTimer($this->options['timeout'], function() {
                        $this->logger->warning('[GDS.Gateway] Gateway timeout.', array('ttl'=>$this->options['timeout']));
                        $this->cancel();
                        throw new GatewayTimeoutException('Gateway timed out after '.$this->options['timeout'].' seconds');
                    });

                    $this->waitForGdsConnectionReady(function() {
                        $this->initializeTimers();
                    });
                }

                $this->running = true;
                $this->eventLoop->run();
            }
        }
        catch (\Exception $e) {
            throw $e;
        }
    }

    private function waitForGdsConnectionReady(callable $callback)
    {
        $this->eventLoop->addPeriodicTimer(0.05, function($timer) use ($callback) {
            if ($this->connection->established()) {
                $this->eventLoop->cancelTimer($timer);
                call_user_func($callback);
            }
            else {
                $lastError = $this->connection->getLastError();
                if (null !== $lastError) {
                    $this->cancel();
                    throw $lastError;
                }
            }
        });
    }

    private function initializeTimers()
    {
        $this->messageQueueTimer = $this->eventLoop->addPeriodicTimer(0.05, function() {
            try {
                while (!$this->messageQueue->isEmpty()) {
                    $this->connection->sendMessage($this->messageQueue->dequeue());
                }
            }
            catch (\RuntimeException $e) {
                $this->cancel();
                throw $e;
            }
        });

        $this->eventLoop->addPeriodicTimer(0.5, function() {
            if (!$this->connection->established() || !$this->messageQueue->isEmpty()) {
                return;
            }

            $suspend = true;
            foreach ($this->endpoints as $endpoint) {
                if ($endpoint->waitForMessage()) {
                    $suspend = false;
                    break;
                }
            }

            if ($suspend) {
                $this->suspend();
            }
        });
    }

    private function suspend()
    {
        if ($this->running) {
            $this->eventLoop->stop();
            $this->running = false;
        }
    }

    private function cancel()
    {
        if ($this->running) {
            if ($this->connection->established()) {
                $this->connection->close();
            }

            $this->eventLoop->stop();
            $this->running = false;
            $this->messageQueue = new \SplQueue();

            $this->logger->debug('[GDS.Gateway] Gateway cancelled.');
        }


        $this->cancelled = true;
    }
}
