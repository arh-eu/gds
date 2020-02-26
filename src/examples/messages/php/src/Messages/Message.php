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

namespace Gds\Messages;

use Gds\Messages\Data\MessageData;
use Gds\Messages\Header\MessageHeader;

class Message {
    private $header;
    private $data;
    
    function __construct(MessageHeader $header, MessageData $data) {
        $this->header = $header;
        $this->data = $data;
    }
    
    function getHeader(): MessageHeader {
        return $this->header;
    }

    function getData(): MessageData {
        return $this->data;
    }

        
    function toArray(): array {
        return array($this->header->getUserName(), $this->header->getMessageId(), 
            $this->header->getCreateTime(), $this->header->getRequestTime(), 
            $this->header->getIsFragmented(), $this->header->getFirstFragment(), 
            $this->header->getLastFragment(), $this->header->getOffset(), 
            $this->header->getFullDataSize(), $this->header->getDataType(), 
            $this->data->toArray());
    }
    
    function toJson() {
        return json_encode($this);
    }
}
