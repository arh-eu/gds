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

use MessagePack\Packer;
use MessagePack\BufferUnpacker;

use Gds\Messages\Header\MessageHeader;
use Gds\Messages\Data\ArrayToDataConverter;

class MessageManager {
    
    static function packMessage(Message $message): string {
        $packer = new Packer();
        return $packer->pack($message->toArray());
    }
    
    static function unpackMessage($binary): Message {
        $unpacker = new BufferUnpacker();
        $unpacker->reset($binary);
        $values = $unpacker->unpack();
        $header = new MessageHeader(
                $values[0],
                $values[1],
                $values[2],
                $values[3],
                $values[4],
                $values[5],
                $values[6],
                $values[7],
                $values[8],
                $values[9]);
        $data = $values[10];
        switch($header->getDataType()) {
            case 0:
                return new Message($header, ArrayToDataConverter::getConnectionData($data));
            case 1:
                return new Message($header, ArrayToDataConverter::getConnectionAckData($data));
            default:
                throw new Exception('Unknown data type');
        }
    }
}
