<?php

namespace App\Gds\Message;

/**
 * @author bordacs
 */
class FragmentationInfo
{
    /**
     * @var bool
     */
    private $isFragmented;

    /**
     * @var bool|null
     */
    private $isFirstFragment;

    /**
     * @var bool|null
     */
    private $isLastFragment;

    /**
     * @var int|null
     */
    private $offset;

    /**
     * @var int|null
     */
    private $fullDataSize;

    public function __construct(bool $isFragmented, ?bool $isFirstFragment = null, ?bool $isLastFragment = null, ?int $offset = null, ?int $fullDataSize = null)
    {
        $this->isFragmented = $isFragmented;

        if ($isFragmented) {
            $this->offset = $offset;
            $this->isFirstFragment = $isFirstFragment;
            $this->isLastFragment = $isLastFragment;
            $this->fullDataSize = $fullDataSize;
        }
    }

    public static function noFragmentation(): \App\Gds\Message\FragmentationInfo
    {
        return new self(false);
    }

    public function isFragmented(): bool
    {
        return $this->isFragmented;
    }

    public function isFirstFragment(): ?bool
    {
        return $this->isFirstFragment;
    }

    public function isLastFragment(): ?bool
    {
        return $this->isLastFragment;
    }

    public function offset(): ?int
    {
        return $this->offset;
    }

    public function fullDataSize(): ?int
    {
        return $this->fullDataSize;
    }
}
