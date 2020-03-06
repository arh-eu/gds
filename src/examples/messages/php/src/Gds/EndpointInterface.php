<?php

namespace App\Gds;

use Evenement\EventEmitterInterface;

/**
 * @author bordacs
 */
interface EndpointInterface extends EventEmitterInterface
{
    public function waitForMessage(): bool;

    public function onResponseSuccess(\App\Gds\Message\Response $response): void;

    /**
     * Returns true if error handled, false otherwise.
     *
     * @param int $statusCode
     * @param string $exceptionMessage
     * @param \App\Gds\Message\ResponseData $data
     * @return bool
     */
    public function handleGlobalError(int $statusCode, string $exceptionMessage, \App\Gds\Message\ResponseData $data): bool;
}
