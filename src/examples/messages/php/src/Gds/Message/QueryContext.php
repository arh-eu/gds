<?php

namespace App\Gds\Message;

/**
 * @author mikus
 */
class QueryContext
{
    private $id;
    private $sql;
    private $numOfGotMatches;
    private $requestTime;
    private $consistency;
    private $bucket; //bucket id of the given page's last record
    private $gdsDescriptor; //the server GDS descriptor of the request
    private $lastRecordValues;
    private $notUsedPartitions;

    /**
     * Only local property. This should not be sent to GDS.
     *
     * @var null|\App\Entity\User
     */
    private $owner;

    private function __construct(
        string $id,
        string $sql,
        int $gotMatches,
        int $requestTime,
        string $consistency,
        string $bucketId,
        array $gdsDescriptor,
        array $lastRecordValues,
        array $partitionsNotTouchedYet = array()
    )
    {
        $this->id = $id;
        $this->sql = $sql;
        $this->numOfGotMatches = $gotMatches;
        $this->requestTime = $requestTime;
        $this->consistency = $consistency;
        $this->bucket = $bucketId;
        $this->gdsDescriptor = $gdsDescriptor;
        $this->lastRecordValues = $lastRecordValues;
        $this->notUsedPartitions = $partitionsNotTouchedYet;
    }

    public function setOwner(\App\Entity\User $owner)
    {
        $this->owner = $owner;
    }

    public function getOwner() : ?\App\Entity\User
    {
        return $this->owner;
    }

    public static function fromArray(array $data) : self
    {
        return new self($data[0], $data[1], $data[2], $data[3], $data[4], $data[5], $data[6], $data[7], $data[8]);
    }

    public function toArray() : array
    {
        return array(
            $this->id,
            $this->sql,
            $this->numOfGotMatches,
            $this->requestTime,
            $this->consistency,
            $this->bucket,
            $this->gdsDescriptor,
            $this->lastRecordValues,
            $this->notUsedPartitions
        );
    }
}
