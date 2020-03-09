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
 * Get next query page data
 *
 * GUI -> GDS
 *
 * @author mikus
 */
class DataType12 implements \App\Gds\Message\MessageData
{
    const TYPE = 12;

    private $queryContext;
    private $timeout;

    public function __construct(\App\Gds\Message\QueryContext $queryContext, int $timeout)
    {
        $this->queryContext = $queryContext;
        $this->timeout = $timeout;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public function getTimeout(): int
    {
        return $this->timeout;
    }

    public function getData()
    {
        return array($this->queryContext->toArray(), $this->timeout);
    }
}