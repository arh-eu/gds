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

/**
 * @author bordacs
 */
class MessageHeader
{
    const USERNAME = 0;
    const MESSAGE_ID = 1;
    const CREATE_TIME = 2;
    const REQUEST_TIME = 3;
    const FRAGMENTATION_IF = 4;
    const FRAGMENTATION_FF = 5;
    const FRAGMENTATION_LF = 6;
    const FRAGMENTATION_OFFSET = 7;
    const FRAGMENTATION_FDS = 8;
    const DATA_TYPE = 9;

    private $username;
    private $messageId;
    private $createTime;
    private $requestTime;
    private $fragmentation;
    private $dataType;

    public function __construct(string $username, string $messageId, int $createTime, int $requestTime, \App\Gds\Message\FragmentationInfo $fragmentation, int $dataType)
    {
        $this->username = $username;
        $this->messageId = $messageId;
        $this->createTime = $createTime;
        $this->requestTime = $requestTime;
        $this->fragmentation = $fragmentation;
        $this->dataType = $dataType;
    }

    public static function fromArray(array $data)
    {
        return new self(
            $data[self::USERNAME],
            $data[self::MESSAGE_ID],
            $data[self::CREATE_TIME],
            $data[self::REQUEST_TIME],
            new FragmentationInfo(
                $data[self::FRAGMENTATION_IF],
                $data[self::FRAGMENTATION_FF],
                $data[self::FRAGMENTATION_LF],
                $data[self::FRAGMENTATION_OFFSET],
                $data[self::FRAGMENTATION_FDS]
            ),
            $data[self::DATA_TYPE]
        );
    }

    public function asArray()
    {
        return array(
            $this->username,
            $this->messageId,
            $this->createTime,
            $this->requestTime,
            $this->fragmentation->isFirstFragment(),
            $this->fragmentation->isFirstFragment(),
            $this->fragmentation->isLastFragment(),
            $this->fragmentation->offset(),
            $this->fragmentation->fullDataSize(),
            $this->dataType
        );
    }

    public function setUsername(string $username): void
    {
        $this->username = $username;
    }

    public function getUsername(): ?string
    {
        return $this->username;
    }

    public function getMessageId(): string
    {
        return $this->messageId;
    }

    public function getCreateTime(): int
    {
        return $this->createTime;
    }

    public function getRequestTime(): int
    {
        return $this->requestTime;
    }

    public function getFragmentation(): \App\Gds\Message\FragmentationInfo
    {
        return $this->fragmentation;
    }

    public function getDataType(): int
    {
        return $this->dataType;
    }
}
