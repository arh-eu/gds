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

class CustomEndpoint extends \App\Gds\Endpoint
{
    /**
     * @var \App\Gds\Message\Message
     */
    private $message;

    /**
     * @var \App\Gds\Message\Response
     */
    private $response;

    public function __construct(\App\Gds\Gateway $gateway, \App\Gds\Message\Message $message)
    {
        parent::__construct($gateway);
        $this->message = $message;
    }

    public function onResponseSuccess(\App\Gds\Message\Response $response): void
    {
        $this->response = $response;
        $this->terminate();
    }

    public function start(): void
    {
        $this->gateway->send($this->message, $this);
    }

    public function getResponse() : \App\Gds\Message\Response
    {
        return $this->response;
    }
}