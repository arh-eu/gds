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
 * Attachment request data.
 *
 * GUI -> GDS
 *
 * @author bordacs
 */
class DataType4 implements \App\Gds\Message\MessageData
{
    const TYPE = 4;

    private $sql;

    public function __construct(string $sql)
    {
        $this->sql = $sql;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public function getData()
    {
        return $this->sql;
    }

    public function getTimeout(): int
    {
        return -1;
    }

}
