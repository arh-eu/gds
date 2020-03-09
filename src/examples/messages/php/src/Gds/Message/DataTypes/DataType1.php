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
 * Ack start connection data.
 *
 * GDS -> GUI
 *
 * @author bordacs
 */
class DataType1 implements \App\Gds\Message\ResponseData
{
    const TYPE = 1;

    private $clusterName;
    private $serveOnTheSameConnection;
    private $protocolVersionNumber;
    private $fragmentationSupported;
    private $fragmentTransmissionUnit;
    private $password;
    
    public function __construct(bool $serveOnTheSameConnection, int $protocolVersionNumber, bool $fragmentationSupported, ?int $fragmentTransmissionUnit, ?string $password)
    {
        $this->serveOnTheSameConnection = $serveOnTheSameConnection;
        $this->protocolVersionNumber = $protocolVersionNumber;
        $this->fragmentationSupported = $fragmentationSupported;
        $this->fragmentTransmissionUnit = $fragmentTransmissionUnit;
        $this->password = $password;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public static function fromArray(array $data) : \App\Gds\Message\ResponseData
    {
        if (5 == count($data)) {
            return new self($data[0], $data[1], $data[2], $data[3], $data[4][0]);
        }
        else {
            return new self($data[1], $data[2], $data[3], $data[4], null);
        }
    }
}
