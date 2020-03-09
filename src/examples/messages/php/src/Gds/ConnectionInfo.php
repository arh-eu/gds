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

use \App\Gds\Message\FragmentationInfo;

/**
 * Description of ConnectionInfo
 *
 * @author oliver.nagy
 */
class ConnectionInfo {
    
    /**
     *
     * @var type string
     */
    private $url;
    
    /**
     *
     * @var type FragmentationInfo
     */
    private $fragmentationInfo;
    
    /**
     *
     * @var type bool
     */
    private $serveOnTheSameConnection;
    
    /**
     *
     * @var type bool
     */
    private $fragmentationSupported;
    
    /**
     *
     * @var type ?int
     */
    private $fragmentTransmissionUnit;
    
    /**
     *
     * @var type ?string
     */
    private $password;
    
    public function __construct(
            string $url, 
            FragmentationInfo $fragmentationInfo, 
            bool $serveOnTheSameConnection,
            bool $fragmentationSupported,
            ?int $fragmentationTransmissionUnit,
            ?string $password) {
        $this->url = $url;
        $this->fragmentationInfo = $fragmentationInfo;
        $this->serveOnTheSameConnection = $serveOnTheSameConnection;
        $this->fragmentationSupported = $fragmentationSupported;
        $this->fragmentTransmissionUnit = $fragmentationTransmissionUnit;
        $this->password = $password;
    }
    
    public function getUrl(): string {
        return $this->url;
    }

    public function getFragmentationInfo(): FragmentationInfo {
        return $this->fragmentationInfo;
    }

    public function getServeOnTheSameConnection(): bool {
        return $this->serveOnTheSameConnection;
    }

    public function getFragmentationSupported(): bool {
        return $this->fragmentationSupported;
    }

    public function getFragmentTransmissionUnit(): ?int {
        return $this->fragmentTransmissionUnit;
    }

    public function getPassword(): ?string {
        return $this->password;
    }
}
