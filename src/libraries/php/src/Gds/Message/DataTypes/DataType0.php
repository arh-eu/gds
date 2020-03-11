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
 * Start connection data.
 *
 * GUI -> GDS
 *
 * @author bordacs
 */
class DataType0 implements \App\Gds\Message\MessageData
{
    const TYPE = 0;

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
        if ($fragmentationSupported) {
            $this->fragmentTransmissionUnit = $fragmentTransmissionUnit;
        }
        $this->password = $password;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public function getData()
    {
        return array($this->serveOnTheSameConnection, $this->protocolVersionNumber, $this->fragmentationSupported, $this->fragmentTransmissionUnit, array($this->password));
    }

    public function getTimeout(): int
    {
        return -1;
    }


}
