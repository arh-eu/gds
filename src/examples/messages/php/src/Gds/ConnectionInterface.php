<?php

namespace App\Gds;

/**
 * @author bordacs
 */
interface ConnectionInterface extends \Evenement\EventEmitterInterface
{
    /**
     * Indicates the GDS connection is established and ready to send/receive messages.
     */
    public function established(): bool;

    /**
     * Indicates the underlying connection is closed.
     */
    public function closed(): bool;

    public function establish(): void;

    public function close(): void;

    /**
     * Send message async.
     *
     * @param \App\Gds\Message\Message $message
     * @return void
     */
    public function sendMessage(\App\Gds\Message\Message $message): void;

    /**
     * @return \Exception|null
     */
    public function getLastError(): ?\Exception;
}
