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

class ConnectionAckData extends MessageData {
    private $status;
    private $successAckData;
    private $unsuccessAckData;
    private $exception;
    
    function __construct(
            int $status, 
            ?ConnectionData $successData, 
            ?array $unsuccessData, 
            ?string $exception
    ) {
        $this->status = $status;
        $this->successAckData = $successData;
        $this->unsuccessAckData = $unsuccessData;
        $this->exception = $exception;
    }
    
    function getStatus(): int {
        return $this->status;
    }

    function getSuccessAckData(): ?ConnectionData {
        return $this->successAckData;
    }

    function getUnsuccessAckData(): ?array {
        return $this->unsuccessAckData;
    }

    function getException(): ?string {
        return $this->exception;
    }
    
    function getDataType(): int {
        return DataType::CONNECTION_ACK;
    }
    
    function isConnectionAckData(): bool {
        return true;
    }
    
    function asConnectionAckData(): ConnectionAckData {
        return $this;
    }

    function toArray(): array {
        $aData = array();
        $aData[0] = $this->status;
        if($this->successAckData != null) {
            $aData[1] = $this->successAckData->toArray();
        } else if($this->unsuccessAckData != null) {
            $aData[1] = (array) $this->unsuccessAckData;
        } else {
            $aData[1] = null;
        }
        $aData[2] = $this->exception;
        return $aData;
    }

    public function fillValuesWithArray(array $values) {
        
    }
}
