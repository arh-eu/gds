<?php

namespace App\Gds\Message\DataTypes;

/**
 * Attachment response data.
 *
 * GDS -> GUI
 *
 * @author bordacs
 */
class DataType6 implements \App\Gds\Message\AttachmentData
{
    const TYPE = 6;

    /**
     * Az összes olyan kérelem azonosítót tartalmazza, amellyel az ügyfél a csatolmányt kérelmezte.
     *
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

    /**
     * @var string[]
     */
    private $ownerIds;

    /**
     * @var string
     */
    private $meta;

    /**
     * A csatolmány megőrzési ideje ezredmásodpercben, a tárolás idejéhez képest.
     *
     * @var int
     */
    private $ttl;

    /**
     * A csatolmány megőrzési ideje ezredmásodperc alapú epoch timestampként.
     *
     * @var int
     */
    private $toValid;

    /**
     * @var string
     */
    private $binary;

    public function __construct(array $requestIds, string $ownerTable, string $attachmentId, array $ownerIds, ?string $meta, ?int $ttl, ?int $toValid, ?string $binary)
    {
        $this->requestIds = $requestIds;
        $this->ownerTable = $ownerTable;
        $this->attachmentId = $attachmentId;
        $this->ownerIds = $ownerIds;
        $this->meta = $meta;
        $this->ttl = $ttl;
        $this->toValid = $toValid;
        $this->binary = $binary;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public static function fromArray(array $data) : \App\Gds\Message\ResponseData
    {
        $result = $data[0];

        $attachment = null;

        // Not recoverable error
        // A csatolmányt a forrás ügyfél helyreállíthatatlanul nem tudta
        // előállítani, ezért a későbbiekben sem lesz előállítható
        if (is_array($result['attachment']) && empty($result['attachment'])) {
            $this->remainedWaitTimeMillis = -1;
        }
        else {
            $attachment = array_key_exists('attachement', $result)?$result['attachment']:null;
        }

        return new self(
            $result['requestids'],
            $result['ownertable'],
            $result['attachmentid'],
            $result['ownerids'],
            array_key_exists('meta', $result)?$result['meta']:null,
            array_key_exists('ttl', $result)?$result['ttl']:null,
            array_key_exists('to_valid', $result)?$result['to_valid']:null,
            $attachment
        );
    }

    /*
    public function getData()
    {
        $data = array("requestids" => $this->requestIds, "ownertable" => $this->ownerTable, "attachmentid" => $this->attachmentId, "ownerids" => $this->ownerIds, "ttl" => $this->ttl, "attachment" => $this->binary);
        if($this->meta != null) 
        {
            $data["meta"] = $this->meta;
        }
    }
    */
    
    public function binaryReceived(): bool
    {
        return null !== $this->binary;
    }

    public function getBinary(): string
    {
        return $this->binary;
    }

    public function getAttachmentId(): string
    {
        return $this->attachmentId;
    }

    public function getOwnerTable(): string
    {
        return $this->ownerTable;
    }

    public function getRequestIds(): array
    {
        return $this->requestIds;
    }
}
