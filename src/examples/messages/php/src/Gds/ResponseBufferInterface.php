<?php

namespace App\Gds;

/**
 * @author bordacs
 */
interface ResponseBufferInterface
{
    public function write(\App\Gds\Message\Response $response): void;

    public function getResponseData(): \App\Gds\Message\ResponseData;
}
