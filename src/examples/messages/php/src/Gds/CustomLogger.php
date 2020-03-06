<?php

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

namespace App\Gds;

/**
 * Description of CustomLogger
 *
 * @author oliver.nagy
 */
class CustomLogger extends \Psr\Log\AbstractLogger {
    
    private $channel;

    public function __construct($name = 'YourLoggerChannel') {
        $this->channel = $name;
    }

    public function log($level, $message, array $context = array()) {
        error_log(
            sprintf(
                '%s.%s: %s',
                $this->channel,
                strtoupper($level),
                $this->interpolate($message, $context)
            )
        );
    }

    protected function interpolate($message, array $context = array()) {
        // build a replacement array with braces around the context keys
        $replace = array();
        foreach ($context as $key => $val) {
            $replace['{' . $key . '}'] = $val;
        }

        // interpolate replacement values into the message and return
        return strtr($message, $replace);
    }
}
