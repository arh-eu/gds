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

namespace App\Gds\Exception;

/**
 * @author bordacs
 */
class BadResponseException extends GdsException
{
    public function __construct(int $expected, int $actual, string $message = "", int $code = 0, \Throwable $previous = null)
    {
        $message = 'Invalid message data received: '.$actual.' (expected '.$expected.')';

        parent::__construct($message, $code, $previous);
    }

}