<?php

namespace App\Gds\Exception;

/**
 * @author bordacs
 */
class BadResponseException extends GdsException
{
    public function __construct(int $expected, int $actual, string $message = "", int $code = 0, \Throwable $previous = null)
    {
        $message = 'Invalid message data received: '.$actual.' (expected '.$expected.')';

        parent::__construct($message, $code, $previous);
    }

}
