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

namespace App\Gds\Message;

/**
 * Description of DataType9Result
 *
 * @author oliver.nagy
 */
class DataType9Result
{
    /**
     *
     * @var type int
     */
    private $status;
    
    /**
     *
     * @var type string
     */
    private $notification;
    
    public function __construct(int $status, ?string $notification)
    {
        $this->status = $status;
        $this->notification = $notification;
    }
    
    public function getStatus(): int
    {
        return $this->status;
    }

    public function getNotification(): ?string 
    {
        return $this->notification;
    }
    
    public static function fromArray($data): DataType9Result 
    {
        return new self($data[0], $data[1]);
    }
}
