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
 * Ack attachment request data.
 *
 * GDS -> GUI
 *
 * @author bordacs
 */
class DataType5 extends DataType6
{
    const TYPE = 5;

    /**
     * @var int
     */
    private $status;

    private $remainedWaitTimeMillis;

    private $responseTimestampInMillis;

    public function __construct(int $status, array $requestIds, string $ownerTable, string $attachmentId, array $ownerIds, ?string $meta, ?int $ttl, ?int $toValid, ?string $binary, int $remainedWaitTimeMillis)
    {
        $this->status = $status;
        $this->remainedWaitTimeMillis = $remainedWaitTimeMillis;
        $this->responseTimestampInMillis = intval(microtime(true)*1000);

        parent::__construct($requestIds, $ownerTable, $attachmentId, $ownerIds, $meta, $ttl, $toValid, $binary);
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public static function fromArray(array $data) : \App\Gds\Message\ResponseData
    {
        $result = $data[1];

        $attachment = null;
        // Nonrecoverable error
        // A csatolmányt a forrás ügyfél helyreállíthatatlanul nem tudta
        // előállítani, ezért a későbbiekben sem lesz előállítható
        if (is_array($result['attachment']) && empty($result['attachment'])) {
            $this->remainedWaitTimeMillis = -1;
        }
        else {
            $attachment = array_key_exists('attachment', $result)?$result['attachment']:null;
        }

        return new self(
            $data[0],
            $result['requestids'],
            $result['ownertable'],
            $result['attachmentid'],
            $result['ownerids'],
            array_key_exists('meta', $result)?$result['meta']:null,
            array_key_exists('ttl', $result)?$result['ttl']:null,
            array_key_exists('to_valid', $result)?$result['to_valid']:null,
            $attachment,
            $data[2]
        );
    }

    /**
     * -1 indicates not recoverably error
     *
     * @return int
     */
    public function getRemainedWaitTimeInMillis(): int
    {
        if (-1 == $this->remainedWaitTimeMillis) {
            return  -1;
        }

        return max(0, $this->remainedWaitTimeMillis - (intval(microtime(true)*1000)-$this->responseTimestampInMillis));
    }
}
