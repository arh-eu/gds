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

namespace App\Gds\Message;

class Message
{
    private $header;
    private $data;

    public function __construct(\App\Gds\Message\MessageHeader $header, \App\Gds\Message\Data $data)
    {
        $this->header = $header;
        $this->data = $data;
    }

    public static function create(string $username, int $createTime, int $requestTime, \App\Gds\Message\FragmentationInfo $fragmentation, \App\Gds\Message\Data $data)
    {
        $header = new \App\Gds\Message\MessageHeader($username, uniqid(), $createTime, $requestTime, $fragmentation, $data->getType());

        return new self($header, $data);
    }

    public function getId(): string
    {
        return $this->header->getMessageId();
    }

    public function setUsername(string $username): void
    {
        $this->header->setUsername($username);
    }

    public function getHeader(): \App\Gds\Message\MessageHeader
    {
        return $this->header;
    }

    public function getData(): \App\Gds\Message\Data
    {
        return $this->data;
    }

    public function getDataType(): int
    {
        return $this->data->getType();
    }
}