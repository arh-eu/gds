<?php

namespace App\Gds\Message;

/**
 * Wrapper class for MessageData implementations.
 *
 * @author bordacs
 */
class Ack implements MessageData
{
    /**
     * @var int
     */
    private $status;

    /**
     * @var \App\Gds\MessageData
     */
    private $data;

    /**
     * @var string
     */
    private $exceptionMessage;

    public function __construct(\App\Gds\Message\MessageData $data, int $status = Response::STATUS_OK, string $exceptionMessage = '')
    {
        $this->data = $data;
        $this->status = $status;
        $this->exceptionMessage = $exceptionMessage;
    }

    /**
     * {@inheritDoc}
     */
    public function getTimeout(): int
    {
        return -1;
    }

    /**
     * {@inheritDoc}
     */
    public function getData()
    {
        return array(
            $this->status,
            $this->data->getData(),
            $this->exceptionMessage
        );
    }

    /**
     * {@inheritDoc}
     */
    public function getType(): int
    {
        return $this->data->getType();
    }

}