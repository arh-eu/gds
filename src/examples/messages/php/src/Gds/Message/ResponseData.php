<?php

namespace App\Gds\Message;

/**
 * @author bordacs
 */
interface ResponseData extends Data
{
    public static function fromArray(array $data): self;
}
