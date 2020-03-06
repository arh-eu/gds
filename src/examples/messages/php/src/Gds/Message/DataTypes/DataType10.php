<?php

namespace App\Gds\Message\DataTypes;

/**
 * Query request data.
 *
 * GUI -> GDS
 * 
 * @author bordacs
 */
class DataType10 implements \App\Gds\Message\MessageData
{
    const TYPE = 10;

    const CONSISTENCY_PAGE = 'PAGE';
    const CONSISTENCY_PAGES = 'PAGES';
    const CONSISTENCY_NONE = 'NONE';

    private $sql;
    private $consistency;
    private $timeout;

    public function __construct(string $sql, int $timeout, string $consistency = self::CONSISTENCY_PAGES)
    {
        $this->sql = $sql;
        $this->consistency = $consistency;
        $this->timeout = $timeout;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public function getData()
    {
        return array($this->sql, $this->consistency, $this->timeout);
    }

    public function getTimeout(): int
    {
        return $this->timeout;
    }
}
