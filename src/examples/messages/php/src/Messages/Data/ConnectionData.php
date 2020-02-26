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

namespace Gds\Messages\Data;

class ConnectionData extends MessageData {
    private $serveOnTheSameConnection;
    private $protocolVersionNumber;
    private $fragmentationSupported;
    private $fragmentationTransmissionUnit;
    private $reservedFields;
    
    function __construct(
            bool $serveOnTheSameConnection, 
            int $protocolVersionNumber,
            bool $fragmentationSupported, 
            ?int $fragmentationTransmissionUnit,
            ?array $reservedFields
    ) {
        $this->serveOnTheSameConnection = $serveOnTheSameConnection;
        $this->protocolVersionNumber = $protocolVersionNumber;
        $this->fragmentationSupported = $fragmentationSupported;
        $this->fragmentationTransmissionUnit = $fragmentationTransmissionUnit;
        $this->reservedFields = $reservedFields;
    }
    
    function getServeOnTheSameConnection(): bool {
        return $this->serveOnTheSameConnection;
    }

    function getProtocolVersionNumber(): int {
        return $this->protocolVersionNumber;
    }

    function getFragmentationSupported(): bool {
        return $this->fragmentationSupported;
    }

    function getFragmentationTransmissionUnit(): ?int {
        return $this->fragmentationTransmissionUnit;
    }

    function getReservedFields(): ?array {
        return $this->reservedFields;
    }
    
    function getDataType(): int {
        return DataType::CONNECTION;
    }
    
    function isConnectionData(): bool {
        return true;
    }
    
    function asConnectionData(): ConnectionData {
        return $this;
    }

    function toArray(): array {
        return array_values((array) $this);
    }
}
