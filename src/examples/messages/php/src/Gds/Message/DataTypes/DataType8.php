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
 * Description of DataType8
 *
 * @author oliver.nagy
 */
class DataType8 implements \App\Gds\Message\MessageData 
{
    
    const TYPE = 8;
   
    /**
     *
     * @var type string
     */
    private $tableName;
    
    /**
     *
     * @var type array
     */
    private $fieldDescriptors;
    
    /**
     *
     * @var type array
     */
    private $records;
    
    public function __construct(string $tableName, array $fieldDescriptors, array $records) 
    {
        $this->tableName = $tableName;
        $this->fieldDescriptors = $fieldDescriptors;
        $this->records = $records;
    }
    
    public function getTableName(): string 
    {
        return $this->tableName;
    }

    public function getFieldDescriptors(): array 
    {
        return $this->fieldDescriptors;
    }

    public function getRecords(): array
    {
        return $this->records;
    }
        
    public function getData() 
    {
        return array($this->tableName, $this->fieldDescriptors, $this->records);
    }

    public function getTimeout(): int 
    {
        return -1;
    }

    public function getType(): int 
    {
        return self::TYPE;
    }
}
