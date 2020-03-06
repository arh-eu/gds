<?php

namespace App\Gds\Exception;

/**
 * @author bordacs
 */
class BadRequestException extends GlobalErrorException
{
    public function __construct(string $message = null, int $code = 0, \Exception $previous = null)
    {
        parent::__construct(400, $message, $code, $previous);
    }
}
