<?php

namespace App\Gds\Message\DataTypes;

/**
 * Description of DataType9
 *
 * @author oliver.nagy
 */
class DataType9 implements \App\Gds\Message\ResponseData 
{
    
    const TYPE = 9;
    
    /**
     *
     * @var type array
     */
    private $result;
    
    public function __construct(array $result) {
        $this->result = $result;
    }
    
    public function getResult(): array {
        return $this->result;
    }
    
    public function getType(): int 
    {
        return self::TYPE;
    }

    public static function fromArray(array $data): \App\Gds\Message\ResponseData 
    {
        $result = array();
        foreach($data as $item) 
        {
            array_push($result, \App\Gds\Message\DataType9Result::fromArray($item));
        }
        return new self($result);
    }
}
