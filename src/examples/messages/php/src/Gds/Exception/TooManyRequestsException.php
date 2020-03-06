<?php

namespace App\Gds\Exception;

/**
 * @author bordacs
 */
class TooManyRequestException extends GlobalErrorException
{
    public function __construct(string $message = null, int $code = 0, \Exception $previous = null)
    {
        parent::__construct(429, $message, $code, $previous);
    }
}
