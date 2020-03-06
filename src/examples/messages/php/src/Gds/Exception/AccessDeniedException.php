<?php

namespace App\Gds\Exception;

/**
 * @author bordacs
 */
class AccessDeniedException extends GlobalErrorException
{
    public function __construct(string $message = null, int $code = 0, \Exception $previous = null)
    {
        parent::__construct(403, $message, $code, $previous);
    }
}
