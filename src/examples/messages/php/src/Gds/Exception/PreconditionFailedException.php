<?php

namespace App\Gds\Exception;

/**
 * @author bordacs
 */
class PreconditionFailedException extends GlobalErrorException
{
    public function __construct(string $message = null, int $code = 0, \Exception $previous = null)
    {
        parent::__construct(412, $message, $code, $previous);
    }
}
