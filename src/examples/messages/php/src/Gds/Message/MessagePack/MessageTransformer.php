<?php

namespace App\Gds\Message\MessagePack;

use App\Gds\Message\Message;
use App\Gds\Message\MessageData;
use MessagePack\Packer;
use MessagePack\TypeTransformer\Packable;

/**
 * @author bordacs
 */
class MessageTransformer implements Packable
{
    public function pack(Packer $packer, $value) : ?string
    {
        if (!($value instanceof Message)) {
            return null;
        }

        $data = $value->getData();
        if (!($data instanceof MessageData)) {
            throw new \LogicException('Only MessageData can be packed!');
        }

        $header = $value->getHeader();
        $fragmentation = $header->getFragmentation();

        return $packer->pack(array(
            $header->getUserName(),
            $header->getMessageId(),
            $header->getCreateTime(),
            $header->getRequestTime(),
            $fragmentation->isFragmented(),
            $fragmentation->isFirstFragment(),
            $fragmentation->isLastFragment(),
            $fragmentation->offset(),
            $fragmentation->fullDataSize(),
            $data->getType(),
            $data->getData()
        ));
    }
}
