<?php

namespace App\Gds\Exception;

/**
 * @author bordacs
 */
class InternalServerErrorException extends GlobalErrorException
{
    public function __construct(string $message = null, int $code = 0, \Exception $previous = null)
    {
        parent::__construct(500, $message, $code, $previous);
    }
}
