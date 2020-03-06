<?php

namespace App\Gds\Exception;

/**
 * @author bordacs
 */
class UnauthorizedException extends GlobalErrorException
{
    public function __construct(string $message = null, int $code = 0, \Exception $previous = null)
    {
        parent::__construct(401, $message, $code, $previous);
    }
}
