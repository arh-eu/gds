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

namespace App\Gds\Message\DataTypes;

/**
 * Ack event store or modification data
 *
 * GDS -> GUI
 *
 * @author mate
 */
class DataType3 implements \App\Gds\Message\ResponseData
{
    const TYPE = 3;

    private $status;
    private $notification;
    private $fieldDescriptors;
    private $details;

    public function __construct($status, $notification, $fieldDescriptors, $details)
    {
        $this->status = $status;
        $this->notification = $notification;
        $this->fieldDescriptors = $fieldDescriptors;
        $this->details = $details;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public static function fromArray(array $data) : \App\Gds\Message\ResponseData
    {
        $ackResult = $data[0];
        return new self($ackResult[0], $ackResult[1], $ackResult[2], $ackResult[3]);
    }

    public function getFieldDescriptors(): array
    {
        return $this->fieldDescriptors;
    }

    public function getStatus() : int
    {
        return $this->status;
    }

    public function getNotification() : ?string
    {
        return $this->notification;
    }

    public function getDetails() : array
    {
        return $this->details;
    }
}
