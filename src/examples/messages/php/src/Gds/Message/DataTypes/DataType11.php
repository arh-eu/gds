<?php

namespace App\Gds\Message\DataTypes;

use App\Gds\Message\QueryContext;

/**
 * Ack query request data.
 *
 * GDS -> GUI
 *
 * @author bordacs
 */
class DataType11 implements \App\Gds\Message\ResponseData
{
    const TYPE = 11;

    private $rowCount;
    private $isLastPage;
    private $queryContext;
    private $fieldDescriptors;
    private $records;

    public function __construct(int $rowCount, bool $isLastPage, QueryContext $context, array $fieldDescriptors, array $records)
    {
        $this->rowCount = $rowCount;
        $this->isLastPage = $isLastPage;
        $this->queryContext = $context;
        $this->fieldDescriptors = $fieldDescriptors;
        $this->records = $records;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public static function fromArray(array $data) : \App\Gds\Message\ResponseData
    {
        return new self($data[0], !$data[2], QueryContext::fromArray($data[3]), $data[4], $data[5]);
    }

    public function getRowCount(): int
    {
        return $this->rowCount;
    }

    public function isLastPage(): bool
    {
        return $this->isLastPage;
    }

    public function getQueryContext(): QueryContext
    {
        return $this->queryContext;
    }

    public function getFieldDescriptors(): array
    {
        return $this->fieldDescriptors;
    }

    public function getRecords(): array
    {
        return $this->records;
    }
}
