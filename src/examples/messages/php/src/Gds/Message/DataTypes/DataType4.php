<?php

namespace App\Gds\Message\DataTypes;

/**
 * Attachment request data.
 *
 * GUI -> GDS
 *
 * @author bordacs
 */
class DataType4 implements \App\Gds\Message\MessageData
{
    const TYPE = 4;

    private $sql;

    public function __construct(string $sql)
    {
        $this->sql = $sql;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public function getData()
    {
        return $this->sql;
    }

    public function getTimeout(): int
    {
        return -1;
    }

}
