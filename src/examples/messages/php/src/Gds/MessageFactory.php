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

namespace App\Gds;

use App\Gds\Message\DataTypes\NullResponseData;

/**
 * @author bordacs
 */
class MessageFactory
{
    private static $dataTypeRegistry = array(
        1 => \App\Gds\Message\DataTypes\DataType1::class,
        3 => \App\Gds\Message\DataTypes\DataType3::class,
        5 => \App\Gds\Message\DataTypes\DataType5::class,
        9 => \App\Gds\Message\DataTypes\DataType9::class,
        11 => \App\Gds\Message\DataTypes\DataType11::class,
    );

    public static function createResponseData(int $type, ?array $data): \App\Gds\Message\ResponseData
    {
        if (!array_key_exists($type, self::$dataTypeRegistry)) {
            throw new \InvalidArgumentException('Unknown data type: ' . $type);
        }

        if (null === $data) {
            return new NullResponseData($type);
        }

        $dataClass = self::$dataTypeRegistry[$type];

        return $dataClass::fromArray($data);
    }
}
