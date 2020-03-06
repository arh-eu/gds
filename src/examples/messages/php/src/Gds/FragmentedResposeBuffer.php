<?php

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

