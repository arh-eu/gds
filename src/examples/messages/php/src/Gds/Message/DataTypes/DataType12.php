<?php

namespace App\Gds\Message\DataTypes;

/**
 * Get next query page data
 *
 * GUI -> GDS
 *
 * @author mikus
 */
class DataType12 implements \App\Gds\Message\MessageData
{
    const TYPE = 12;

    private $queryContext;
    private $timeout;

    public function __construct(\App\Gds\Message\QueryContext $queryContext, int $timeout)
    {
        $this->queryContext = $queryContext;
        $this->timeout = $timeout;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public function getTimeout(): int
    {
        return $this->timeout;
    }

    public function getData()
    {
        return array($this->queryContext->toArray(), $this->timeout);
    }
}