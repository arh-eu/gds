<?php

require '..\vendor\autoload.php';

use Gds\Messages\Message;
use Gds\Messages\Header\MessageHeader;
use Gds\Messages\Data\ConnectionData;
use Gds\Messages\Data\ConnectionAckData;
use Gds\Messages\MessageManager;

//Connection test
/*
$header = new MessageHeader("user", "message_id", 1000, 1000, false, NULL, NULL, NULL, NULL, 0);
$data = new ConnectionData(false, 1, false, null, array("asdasdas"));
$message = new Message($header, $data);

$packed = MessageManager::packMessage($message);
$unpacked = MessageManager::unpackMessage($packed);
*/

//Connection Ack test
$header = new MessageHeader("user", "message_id", 1000, 1000, false, NULL, NULL, NULL, NULL, 1);
$successAckData = new ConnectionData(false, 1, false, null, array("asdasdas"));
$unsuccessData = array("1"=>"asd");
$data = new ConnectionAckData(200, null, $unsuccessData, null);

$message = new Message($header, $data);

$packed = MessageManager::packMessage($message);
$unpacked = MessageManager::unpackMessage($packed);

//$file = 'message';
//file_put_contents($file, $packed);


