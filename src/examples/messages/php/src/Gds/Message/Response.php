<?php

namespace App\Gds\Message;

use App\Gds\MessageFactory;

class Response
{
    const ACK = 10;
    const ACK_STATUS = 0;
    const ACK_DATA = 1;
    const ACK_EXCEPTION = 2;

    const STATUS_OK = 200;
    const STATUS_CREATED = 201;
    const STATUS_ACCEPTED = 202;
    const STATUS_NOT_MODIFIED = 304;
    const STATUS_BAD_REQUEST = 400;
    const STATUS_UNAUTHORIZED = 401;
    const STATUS_FORBIDDEN = 403;
    const STATUS_NOT_ACCEPTABLE = 406;
    const STATUS_REQUEST_TIMEOUT = 408;
    const STATUS_CONFLICT = 409;
    const STATUS_PRECONDITION_FAILED = 412;
    const STATUS_TOO_MANY_REQUESTS = 429;
    const STATUS_INTERNAL_SERVER_ERROR = 500;
    const STATUS_BANDWIDTH_LIMIT_EXCEEDED = 509;

    public static $statusTexts = array(
        200 => 'OK',
        201 => 'Created',
        202 => 'Accepted',
        304 => 'Not Modified',
        400 => 'Bad Request',
        401 => 'Unauthorized',
        403 => 'Forbidden',
        406 => 'Not Acceptable',
        408 => 'Request Timeout',
        409 => 'Conflict',
        412 => 'Precondition Failed',
        429 => 'Too Many Requests',
        500 => 'Internal Server Error',
        509 => 'Bandwidth Limit Exceeded'
    );

    /**
     * @var \App\Gds\Message\MessageHeader
     */
    private $header;

    /**
     * @var \App\Gds\Message\ResponseData
     */
    private $data;

    /**
     * @var int
     */
    private $status;

    /**
     * @var string
     */
    private $exceptionMessage;

    public function __construct(\App\Gds\Message\MessageHeader $header, \App\Gds\Message\ResponseData $data, int $status, string $exceptionMessage = null)
    {
        $this->header = $header;
        $this->data = $data;
        $this->status = $status;
        $this->exceptionMessage = $exceptionMessage;
    }

    public static function fromArray(array $data): \App\Gds\Message\Response
    {
        $header = MessageHeader::fromArray($data);

        $ack = $data[self::ACK];

        if (self::STATUS_OK != $ack[self::ACK_STATUS]) { 
            $ack[self::ACK_DATA] = null;
        }

        return new self(
            $header,
            MessageFactory::createResponseData($header->getDataType(), $ack[self::ACK_DATA]),
            $ack[self::ACK_STATUS],
            $ack[self::ACK_EXCEPTION]
        );
    }

    public function getMessageId(): string
    {
        return $this->header->getMessageId();
    }

    public function getData(): \App\Gds\Message\ResponseData
    {
        return $this->data;
    }

    public function getDataType(): int
    {
        return $this->data->getType();
    }

    public function getStatus(): int
    {
        return $this->status;
    }

    public function getExceptionMessage(): ?string
    {
        return $this->exceptionMessage;
    }
}