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

use App\Gds\Message\DataTypes as DataTypes;
use App\Gds\Message\Message;
use App\Gds\Message\MessageHeader;
use App\Gds\Message\Response;
use App\Gds\Message\FragmentationInfo;
use App\Gds\Exception\ConnectionFailedException;
use App\Gds\Exception\ConnectionException;
use MessagePack\MessagePack;
use Ratchet\RFC6455\Messaging\Frame;
use Evenement\EventEmitterTrait;
use Psr\Log\LoggerInterface;
use App\Gds\ConnectionInfo;

/**
 * GDS websocket connection
 *
 * To receive messages, client must subscribe the 'messageReceived' event.
 *
 * <code>
 *     $connection = new Connection($url, $eventLoop);
 *     $connection->on('messageReceived', function(\App\Gds\Message\Response $response) {
 *         //TODO: do something with $response
 *     });
 * </code>
 *
 * @author bordacs
 */
class Connection implements ConnectionInterface
{
    use EventEmitterTrait;

    /**
     * @var string
     */
    private $url;

    /**
     * @var string
     */
    private $username;
    
    private $connectionInfo;
    
    /**
     * @var \React\EventLoop\LoopInterface
     */
    public $eventLoop;

    /**
     * @var \Ratchet\Client\WebSocket
     */
    private $connection;

    /**
     * Indicates, that GDS is ready to receive messages.
     *
     * @var bool
     */
    private $established = false;

    /**
     * Indicates, that websocket connection closed.
     *
     * @var bool
     */
    private $closed = false;

    /**
     * @var \MessagePack\Packer
     */
    private $packer;

    /**
     * @var \Exception[]
     */
    private $errors;

    /**
     * @var \Psr\Log\LoggerInterface
     */
    private $logger;

    public function __construct(ConnectionInfo $connectionInfo, \React\EventLoop\LoopInterface $eventLoop, LoggerInterface $logger)
    {
        $this->connectionInfo = $connectionInfo;
        $this->eventLoop = $eventLoop;
        $this->logger = $logger;

        $this->username = parse_url($connectionInfo->getUrl(), PHP_URL_USER);

        if ('' == $this->username) {
            throw new ConnectionFailedException('Missing GDS username. Username must be specified in the URL in format: \'ws://username@host/path\'.');
        }

        $this->url = str_replace($this->username.'@', '', $connectionInfo->getUrl());
        
        $this->packer = new \MessagePack\Packer(\MessagePack\PackOptions::FORCE_ARR);
        $this->packer->registerTransformer(new \MessagePack\TypeTransformer\MapTransformer());
        $this->packer->registerTransformer(new \App\Gds\Message\MessagePack\MessageTransformer());

        $this->errors = array();
    }

    public function __destruct()
    {
        if (null != $this->connection) {
            $this->connection->close();
        }
    }

    public function established(): bool
    {
        return $this->established;
    }

    public function closed(): bool
    {
        return $this->closed;
    }

    public function establish(): void
    {
        $connector = new \Ratchet\Client\Connector($this->eventLoop, new \React\Socket\Connector($this->eventLoop, array(
            'timeout' => 10,
            'dns' => false
        )));

        $connector($this->url, [], ['Origin' => 'http://localhost'])->then(
            function(\Ratchet\Client\WebSocket $connection) {
                $this->connection = $connection;

                $this->connection->on('message', function(\Ratchet\RFC6455\Messaging\MessageInterface $message) {
                    $this->onMessageReceived($message);
                });

                $this->connection->on('gdsConnectionReady', function() {
                    $this->onGdsConnectionReady();
                });

                $this->connection->on('gdsConnectionFailed', function(\App\Gds\Message\Response $response) {
                    $this->onGdsConnectionFailed($response);
                });

                $this->connection->on('error', function(\Exception $error, \Ratchet\Client\WebSocket $connection) {
                    $this->onConnectionError($error, $connection);
                });

                $this->connection->on('close', function($code, $reason) {
                    $this->onConnectionClose($code, $reason);
                });

                $data = new DataTypes\DataType0($this->connectionInfo->getServeOnTheSameConnection(), $this->connectionInfo->getProtocolVersionNumber(), $this->connectionInfo->getFragmentationSupported(), $this->connectionInfo->getFragmentTransmissionUnit(), $this->connectionInfo->getPassword());
                $header = new MessageHeader($this->username, uniqid(), time(), time(), $this->connectionInfo->getFragmentationInfo(), $data->getType());

                $this->logger->debug('[GDS.Connection] Initiate GDS connection: sending start connection data.', array('url'=>$this->url, 'username'=>$this->username));

                $this->connection->send($this->createFrame(new Message($header, $data)));
            },
            function(\Exception $e) {
                $this->logger->error('[GDS.Connection] Connection failed.', array('error'=>$e->getMessage()));
                $this->errors[] = new ConnectionFailedException($e->getMessage());
            }
        )->done();
    }

    public function close(): void
    {
        if (null !== $this->connection) {
            $this->connection->close();
        }
    }

    /**
     * @param \App\Gds\Message\Message $message
     * @return void
     * @throws \RuntimeException
     */

    public function sendMessage(\App\Gds\Message\Message $message): void
    {
        if (!$this->established) {
            $this->logger->error('[GDS.Connection] Unable to send message: connection is not established.');

            throw new ConnectionException('Unable to send message: connection is not established');
        }

        if ($this->closed) {
            $this->logger->error('[GDS.Connection] Unable to send message: connection closed.');

            throw new ConnectionException('Unable to send message: connection closed');
        }

        $message->setUsername($this->username);

        $this->logger->debug('[GDS.Connection] Sending message', array('id'=>$message->getId(), 'type'=>$message->getDataType(), 'data'=>$message->getData()->getData()));

        $this->connection->send($this->createFrame($message));
    }

    private function onGdsConnectionReady()
    {
        $this->established = true;
    }

    private function onGdsConnectionFailed(\App\Gds\Message\Response $response)
    {
        $this->close();

        $this->logger->error('[GDS.Connection] Connection failed.', array('status'=>$response->getStatus(), 'message'=>$response->getExceptionMessage()));

        throw new ConnectionFailedException($response->getExceptionMessage());
    }

    private function onConnectionError(\Exception $error, \Ratchet\Client\WebSocket $connection)
    {
        $this->close();

        $this->logger->error('[GDS.Connection] Connection error.', array('error'=>$error->getMessage()));

        $error = new ConnectionException($error->getMessage());

        $this->errors[] = $error;

        throw $error;
    }

    private function onConnectionClose($code, $reason)
    {
        $this->closed = true;

        if (Frame::CLOSE_NORMAL != $code) {
            $this->logger->debug('[GDS.Connection] Connection unexpectedly closed.', array('code'=>$code, 'reason'=>$reason));
        }
        else {
            $this->logger->debug('[GDS.Connection] Connection closed.');
        }
    }

    private function onMessageReceived(\Ratchet\RFC6455\Messaging\MessageInterface $message)
    {
        $response = Response::fromArray(MessagePack::unpack((string)$message));

        $this->logger->debug('[GDS.Connection] Message received.', array('messageId'=>$response->getMessageId(), 'type'=>$response->getDataType(), 'status'=>$response->getStatus(), 'exeptionMessage'=>$response->getExceptionMessage()));

        if (1 == $response->getDataType()) {
            if (Response::STATUS_OK == $response->getStatus()) {
                $this->connection->emit('gdsConnectionReady');
            }
            else {
                $this->connection->emit('gdsConnectionFailed', array($response));
            }
        }
        else {
            $this->emit('messageReceived', array($response));
        }
    }

    private function createFrame(\App\Gds\Message\Message $message)
    {
        return new Frame($this->packer->pack($message), true, Frame::OP_BINARY);
    }

    public function getLastError(): ?\Exception
    {
        if (empty($this->errors)) {
            return null;
        }

        return array_pop($this->errors);
    }
}
