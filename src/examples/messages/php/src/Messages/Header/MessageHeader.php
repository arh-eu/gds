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

namespace Gds\Messages\Header;

class MessageHeader {
    private $userName;
    private $messageId;
    private $createTime;
    private $requestTime;
    private $isFragmented;
    private $firstFragment;
    private $lastFragment;
    private $offset;
    private $fullDataSize;
    private $dataType;
    
    function __construct(
            string $userName, 
            string $messageId,
            int $createTime, 
            int $requestTime, 
            bool $isFragmented, 
            ?bool $firstFragment, 
            ?bool $lastFragment, 
            ?int $offset, 
            ?int $fullDataSize, 
            int $dataType
    ) {
        $this->userName = $userName;
        $this->messageId = $messageId;
        $this->createTime = $createTime;
        $this->requestTime = $requestTime;
        $this->isFragmented = $isFragmented;
        $this->firstFragment = $firstFragment;
        $this->lastFragment = $lastFragment;
        $this->offset = $offset;
        $this->fullDataSize = $fullDataSize;
        $this->dataType = $dataType;
    }
    
    function getUserName(): string {
        return $this->userName;
    }

    function getMessageId(): string {
        return $this->messageId;
    }

    function getCreateTime(): int {
        return $this->createTime;
    }

    function getRequestTime(): int {
        return $this->requestTime;
    }

    function getIsFragmented(): bool {
        return $this->isFragmented;
    }

    function getFirstFragment(): ?bool {
        return $this->firstFragment;
    }

    function getLastFragment(): ?bool {
        return $this->lastFragment;
    }

    function getOffset(): ?int {
        return $this->offset;
    }

    function getFullDataSize(): ?int {
        return $this->fullDataSize;
    }

    function getDataType(): int {
        return $this->dataType;
    }
    
    function toJson() {
        return json_encode($this);
    }
}

