<?php

namespace App\Gds\Message\DataTypes;

/**
 * Ack start connection data.
 *
 * GDS -> GUI
 *
 * @author bordacs
 */
class DataType1 implements \App\Gds\Message\ResponseData
{
    const TYPE = 1;

    private $clusterName;
    private $serveOnTheSameConnection;
    private $protocolVersionNumber;
    private $fragmentationSupported;
    private $fragmentTransmissionUnit;
    private $password;
    
    public function __construct(bool $serveOnTheSameConnection, int $protocolVersionNumber, bool $fragmentationSupported, ?int $fragmentTransmissionUnit, ?string $password)
    {
        $this->serveOnTheSameConnection = $serveOnTheSameConnection;
        $this->protocolVersionNumber = $protocolVersionNumber;
        $this->fragmentationSupported = $fragmentationSupported;
        $this->fragmentTransmissionUnit = $fragmentTransmissionUnit;
        $this->password = $password;
    }

    public function getType(): int
    {
        return self::TYPE;
    }

    public static function fromArray(array $data) : \App\Gds\Message\ResponseData
    {
        if (5 == count($data)) {
            return new self($data[0], $data[1], $data[2], $data[3], $data[4][0]);
        }
        else {
            return new self($data[1], $data[2], $data[3], $data[4], null);
        }
    }
}
