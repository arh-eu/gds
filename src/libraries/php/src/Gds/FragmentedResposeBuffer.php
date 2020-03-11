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

namespace App\Gds;

use App\Gds\Message\Fragmentable;

/**
 * @author bordacs
 */
class FragmentedResponseBuffer implements ResponseBufferInterface
{
    /**
     * @var int
     */
    private $messageId;

    /**
     * @var \App\Gds\Message\ResponseData[]
     */
    private $dataFragments;

    public function __construct(string $messageId)
    {
        $this->messageId = $messageId;
        $this->dataFragments = array();
    }

    public function write(\App\Gds\Message\Response $response): void
    {
        $data = $response->getMessage()->getData();

        if (!($data instanceof Fragmentable)) {
            throw new \LogicException('Massage data must implement '.Fragmentable::class.'  interface!');
        }

        $this->dataFragments[] = $response->getMessage()->getData();
    }

    public function getResponseData(): Message\ResponseData
    {
        $fragments = $this->dataFragments;

        $data = clone(array_shift($fragments));

        foreach ($fragments as $fragment) {
            $data->concat($fragment);
        }

        return $data;
    }
}

