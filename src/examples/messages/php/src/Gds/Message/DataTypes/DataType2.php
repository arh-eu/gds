<?php

namespace App\Gds\Message\DataTypes;

/**
 * Event store or modification” data.
 *
 * GUI -> GDS
 *
 * @author bordacs
 */
class DataType2 implements \App\Gds\Message\MessageData
{
    const TYPE = 2;

    private $sql;
    private $binaryContents;
    private $executionPriorityStructure;

    public function __construct(
            string $sql, 
            ?array $binaryContents,
            ?array $executionPriorityStructure)
    {
        $this->sql = $sql;
        $this->binaryContents = $binaryContents;
        $this->executionPriorityStructure = $executionPriorityStructure;
    }

    public function getSql(): string {
        return $this->sql;
    }

    public function getBinaryContents(): ?array {
        return $this->binaryContents;
    }

    public function getExecutionPriorityStructure(): ?array {
        return $this->executionPriorityStructure;
    }
    
    public function getType(): int
    {
        return self::TYPE;
    }

    public function getData()
    {
        return array(
            $this->sql, 
            $this->binaryContents == null ? new \MessagePack\Type\Map([]) : $this->binaryContents, 
            $this->executionPriorityStructure == null ? [] : $this->executionPriorityStructure);
    }

    public function getTimeout(): int
    {
        return -1;
    }
}
