<?php

namespace App;

class CustomEndpoint extends \App\Gds\Endpoint
{
    /**
     * @var \App\Gds\Message\Message
     */
    private $message;

    /**
     * @var \App\Gds\Message\Response
     */
    private $response;

    public function __construct(\App\Gds\Gateway $gateway, \App\Gds\Message\Message $message)
    {
        parent::__construct($gateway);
        $this->message = $message;
    }

    public function onResponseSuccess(\App\Gds\Message\Response $response): void
    {
        $this->response = $response;
        $this->terminate();
    }

    public function start(): void
    {
        $this->gateway->send($this->message, $this);
    }

    public function getResponse() : \App\Gds\Message\Response
    {
        return $this->response;
    }
}