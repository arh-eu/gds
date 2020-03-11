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
