<?php

namespace App\Gds\Message;

/**
 * @author bordacs
 */
interface AttachmentData extends ResponseData
{
    public function getBinary(): string;

    public function binaryReceived(): bool;

    public function getRequestIds(): array;

    public function getOwnerTable(): string;

    public function getAttachmentId(): string;


}
