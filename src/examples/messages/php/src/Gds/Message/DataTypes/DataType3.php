<?php

namespace App\Gds\Message\DataTypes;

/**
 * Ack event store or modification data
 *
 * GDS -> GUI
 *
 * @author mate
 */
class DataType3 implements \App\Gds\Message\ResponseData
{
    const TYPE = 3;

    private $status;
    private $notification;
    private $fieldDescriptors;
    private $details;

    public function __construct($status, $notification, $fieldDescriptors, $details)
    {
        $this->status = $status;
        $this->notification = $notification;
        $this->fieldDescriptors = $fieldDescriptors;
        $this->details = $details;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public static function fromArray(array $data) : \App\Gds\Message\ResponseData
    {
        $ackResult = $data[0];
        return new self($ackResult[0], $ackResult[1], $ackResult[2], $ackResult[3]);
    }

    public function getFieldDescriptors(): array
    {
        return $this->fieldDescriptors;
    }

    public function getStatus() : int
    {
        return $this->status;
    }

    public function getNotification() : ?string
    {
        return $this->notification;
    }

    public function getDetails() : array
    {
        return $this->details;
    }
}
