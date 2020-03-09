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
 * Wrapper class for MessageData implementations.
 *
 * @author bordacs
 */
class Ack implements MessageData
{
    /**
     * @var int
     */
    private $status;

    /**
     * @var \App\Gds\MessageData
     */
    private $data;

    /**
     * @var string
     */
    private $exceptionMessage;

    public function __construct(\App\Gds\Message\MessageData $data, int $status = Response::STATUS_OK, string $exceptionMessage = '')
    {
        $this->data = $data;
        $this->status = $status;
        $this->exceptionMessage = $exceptionMessage;
    }

    /**
     * {@inheritDoc}
     */
    public function getTimeout(): int
    {
        return -1;
    }

    /**
     * {@inheritDoc}
     */
    public function getData()
    {
        return array(
            $this->status,
            $this->data->getData(),
            $this->exceptionMessage
        );
    }

    /**
     * {@inheritDoc}
     */
    public function getType(): int
    {
        return $this->data->getType();
    }

}