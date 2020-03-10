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
 * Description of DataType9
 *
 * @author oliver.nagy
 */
class DataType9 implements \App\Gds\Message\ResponseData 
{
    
    const TYPE = 9;
    
    /**
     *
     * @var type array
     */
    private $result;
    
    public function __construct(array $result) {
        $this->result = $result;
    }
    
    public function getResult(): array {
        return $this->result;
    }
    
    public function getType(): int 
    {
        return self::TYPE;
    }

    public static function fromArray(array $data): \App\Gds\Message\ResponseData 
    {
        $result = array();
        foreach($data as $item)
        {
            array_push($result, \App\Gds\Message\DataType9Result::fromArray($item));
        }
        return new self($result);
    }
}
