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
 * Event store or modification” data.
 *
 * GUI -> GDS
 *
 * @author bordacs
 */
class DataType2 implements \App\Gds\Message\MessageData
{
    const TYPE = 2;

    private $sql;
    private $binaryContents;
    private $executionPriorityStructure;

    public function __construct(
            string $sql, 
            ?array $binaryContents,
            ?array $executionPriorityStructure)
    {
        $this->sql = $sql;
        $this->binaryContents = $binaryContents;
        $this->executionPriorityStructure = $executionPriorityStructure;
    }

    public function getSql(): string {
        return $this->sql;
    }

    public function getBinaryContents(): ?array {
        return $this->binaryContents;
    }

    public function getExecutionPriorityStructure(): ?array {
        return $this->executionPriorityStructure;
    }
    
    public function getType(): int
    {
        return self::TYPE;
    }

    public function getData()
    {
        return array(
            $this->sql, 
            $this->binaryContents == null ? new \MessagePack\Type\Map([]) : $this->binaryContents, 
            $this->executionPriorityStructure == null ? [] : $this->executionPriorityStructure);
    }

    public function getTimeout(): int
    {
        return -1;
    }
}
