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
 * Ack attachment response data.
 *
 * GUI -> GDS
 *
 * @author bordacs
 */
class DataType7 implements \App\Gds\Message\MessageData
{
    const TYPE = 7;

    /**
     * @var int
     */
    private $status;

    /**
     * @var string[]
     */
    private $requestIds;

    /**
     * @var string
     */
    private $ownerTable;

    /**
     * @var string
     */
    private $attachmentId;


    public function __construct(int $status, array $requestIds, string $ownerTable, string $attachmentId)
    {
        $this->status = $status;
        $this->requestIds = $requestIds;
        $this->ownerTable = $ownerTable;
        $this->attachmentId = $attachmentId;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public function getData()
    {
        return array($this->status, new \MessagePack\Type\Map(array(
            'requestids' => $this->requestIds,
            'ownertable' => $this->ownerTable,
            'attachmentid' => $this->attachmentId
        )));
    }

    public function getTimeout(): int
    {
        return -1;
    }
}
