<?php

namespace App\Gds\Message\DataTypes;

/**
 * @author bordacs
 */
class NullResponseData implements \App\Gds\Message\ResponseData
{
    /**
     * @var int
     */
    private $type;

    public function __construct(int $type)
    {
        $this->type = $type;
    }

    public function getType(): int
    {
        return $this->type;
    }

    public static function fromArray(array $data): \App\Gds\Message\ResponseData
    {
        return new self($data[0]);
    }

}
