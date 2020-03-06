<?php

namespace App\Gds\Message\DataTypes;

/**
 * Start connection data.
 *
 * GUI -> GDS
 *
 * @author bordacs
 */
class DataType0 implements \App\Gds\Message\MessageData
{
    const TYPE = 0;

    private $serveOnTheSameConnection;
    private $protocolVersionNumber = 0x01000000;
    private $fragmentationSupported;
    private $fragmentTransmissionUnit;
    private $password;

    public function __construct(bool $serveOnTheSameConnection, bool $fragmentationSupported, ?int $fragmentTransmissionUnit, ?string $password)
    {
        $this->serveOnTheSameConnection = $serveOnTheSameConnection;
        $this->fragmentationSupported = $fragmentationSupported;
        if ($fragmentationSupported) {
            $this->fragmentTransmissionUnit = $fragmentTransmissionUnit;
        }
        $this->password = $password;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public function getData()
    {
        return array($this->serveOnTheSameConnection, $this->protocolVersionNumber, $this->fragmentationSupported, $this->fragmentTransmissionUnit, array($this->password));
    }

    public function getTimeout(): int
    {
        return -1;
    }


}
