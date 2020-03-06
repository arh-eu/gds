<?php

namespace App\Gds\Message;

/**
 * @author bordacs
 */
interface MessageData extends Data
{
    public function getTimeout(): int;

    /**
     * @return mixed
     */
    public function getData();
}
