<?php

namespace App\Gds\Message;

/**
 * @author bordacs
 */
interface Fragmentable
{
   public function concat(\App\Message\ResponseData $data): void;
}
