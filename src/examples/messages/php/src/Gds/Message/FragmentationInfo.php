<?php
/*
 * Copyright 2020 ARH Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
