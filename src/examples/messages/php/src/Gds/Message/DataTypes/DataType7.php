<?php

namespace App\Gds\Message\DataTypes;

/**
 * Ack attachment response data.
 *
 * GUI -> GDS
 *
 * @author bordacs
 */
class DataType7 implements \App\Gds\Message\MessageData
{
    const TYPE = 7;

    /**
     * @var int
     */
    private $status;

    /**
     * @var string[]
     */
    private $requestIds;

    /**
     * @var string
     */
    private $ownerTable;

    /**
     * @var string
     */
    private $attachmentId;


    public function __construct(int $status, array $requestIds, string $ownerTable, string $attachmentId)
    {
        $this->status = $status;
        $this->requestIds = $requestIds;
        $this->ownerTable = $ownerTable;
        $this->attachmentId = $attachmentId;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public function getData()
    {
        return array($this->status, new \MessagePack\Type\Map(array(
            'requestids' => $this->requestIds,
            'ownertable' => $this->ownerTable,
            'attachmentid' => $this->attachmentId
        )));
    }

    public function getTimeout(): int
    {
        return -1;
    }
}
