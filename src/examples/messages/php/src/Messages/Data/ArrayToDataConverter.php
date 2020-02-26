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

class ArrayToDataConverter {
    
    static function getConnectionData($data): ConnectionData {
        return new ConnectionData(
                        $data[0], 
                        $data[1], 
                        $data[2], 
                        $data[3], 
                        count($data) == 5 ? $data[4] : null);
    }
    
    static function getConnectionAckData($data): ConnectionAckData {
        $status = $data[0];
        if(array_values($data[1]) === $data[1]) {
            $successAckData = ArrayToDataConverter::getConnectionData($data[1]);
            $unsuccessAckData = null;
        } else if($data[1] != null) {
            $unsuccessAckData = $data[1];
            $successAckData = null;
        }
        $exception = $data[2];
        return new ConnectionAckData($status, $successAckData, $unsuccessAckData, $exception);
    }
}
