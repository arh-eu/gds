<?php

namespace App\Gds\Message\DataTypes;

/**
 * Description of DataType8
 *
 * @author oliver.nagy
 */
class DataType8 implements \App\Gds\Message\MessageData 
{
    
    const TYPE = 8;
   
    /**
     *
     * @var type string
     */
    private $tableName;
    
    /**
     *
     * @var type array
     */
    private $fieldDescriptors;
    
    /**
     *
     * @var type array
     */
    private $records;
    
    public function __construct(string $tableName, array $fieldDescriptors, array $records) 
    {
        $this->tableName = $tableName;
        $this->fieldDescriptors = $fieldDescriptors;
        $this->records = $records;
    }
    
    public function getTableName(): string 
    {
        return $this->tableName;
    }

    public function getFieldDescriptors(): array 
    {
        return $this->fieldDescriptors;
    }

    public function getRecords(): array
    {
        return $this->records;
    }
        
    public function getData() 
    {
        return array($this->tableName, $this->fieldDescriptors, $this->records);
    }

    public function getTimeout(): int 
    {
        return -1;
    }

    public function getType(): int 
    {
        return self::TYPE;
    }
}
