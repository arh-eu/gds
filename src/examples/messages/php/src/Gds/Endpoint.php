<?php

namespace App\Gds;

use Evenement\EventEmitterTrait;

/**
 * @author bordacs
 */
abstract class Endpoint implements EndpointInterface
{
    use EventEmitterTrait;

    /**
     * @var \App\Gds\Gateway
     */
    protected $gateway;

    /**
     * @var bool
     */
    private $waitForMessage = true;

    public function __construct(\App\Gds\Gateway $gateway)
    {
        $this->gateway = $gateway;
    }

    public function waitForMessage(): bool
    {
        return $this->waitForMessage;
    }

    public function handleGlobalError(int $statusCode, string $exceptionMessage, \App\Gds\Message\ResponseData $data): bool
    {
        return false;
    }

    protected function terminate(): void
    {
        $this->waitForMessage = false;
    }

    abstract public function onResponseSuccess(\App\Gds\Message\Response $response): void;

    abstract public function start(): void;
}
