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
 * Query request data.
 *
 * GUI -> GDS
 * 
 * @author bordacs
 */
class DataType10 implements \App\Gds\Message\MessageData
{
    const TYPE = 10;

    const CONSISTENCY_PAGE = 'PAGE';
    const CONSISTENCY_PAGES = 'PAGES';
    const CONSISTENCY_NONE = 'NONE';

    private $sql;
    private $consistency;
    private $timeout;

    public function __construct(string $sql, int $timeout, string $consistency = self::CONSISTENCY_PAGES)
    {
        $this->sql = $sql;
        $this->consistency = $consistency;
        $this->timeout = $timeout;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public function getData()
    {
        return array($this->sql, $this->consistency, $this->timeout);
    }

    public function getTimeout(): int
    {
        return $this->timeout;
    }
}
