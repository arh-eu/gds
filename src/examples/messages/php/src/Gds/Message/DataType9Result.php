<?php
namespace App\Gds\Message;

/**
 * Description of DataType9Result
 *
 * @author oliver.nagy
 */
class DataType9Result
{
    /**
     *
     * @var type int
     */
    private $status;
    
    /**
     *
     * @var type string
     */
    private $notification;
    
    public function __construct(int $status, ?string $notification)
    {
        $this->status = $status;
        $this->notification = $notification;
    }
    
    public function getStatus(): int
    {
        return $this->status;
    }

    public function getNotification(): ?string 
    {
        return $this->notification;
    }
    
    public static function fromArray($data): DataType9Result 
    {
        return new self($data[0], $data[1]);
    }
}
