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
