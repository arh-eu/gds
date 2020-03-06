<?php

namespace App\Gds\Exception;

/**
 * @author bordacs
 */
class GlobalErrorException extends GdsException
{
    private $statusCode;

    public function __construct(int $statusCode, string $message = null, int $code = 0, \Exception $previous = null)
    {
        $this->statusCode = $statusCode;

        parent::__construct($message, $code, $previous);
    }
    
    public function getStatusCode()
    {
        return $this->statusCode;
    }
}
