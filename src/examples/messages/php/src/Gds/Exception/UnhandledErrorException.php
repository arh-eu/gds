<?php

namespace App\Gds\Exception;

/**
 * @author bordacs
 */
class UnhandledErrorException extends GlobalErrorException
{
    public function __construct(int $statusCode, string $message = null, int $code = 0, \Exception $previous = null)
    {
        parent::__construct($statusCode, $message, $previous, $code);
    }
}
