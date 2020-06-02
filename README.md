# Globessey Data Server

Globessey Data Server (GDS) allows storing, transmitting, and querying large amounts of data, all through different levels of authority that are adjustable in detail. The GDS can manage meta data and the associated attachments. It is a scalable, high availability system that natively supports load balancing.

## Getting Started

If you want to use the system as a user, you can use its features by sending messages which are embedded into [MessagePack](https://msgpack.org/) packages (they are also on [GitHub](https://github.com/msgpack)). 
We provide [SDK](https://github.com/arh-eu/gds/wiki/SDK)s in some languages, so you may not need to implement it from scratch. 
If the SDK you are looking for is not found, see the [Specification](https://github.com/arh-eu/gds/wiki/Specification) for the implementation. 

See the [Wiki](https://github.com/arh-eu/gds/wiki) for more details.

### SDK Examples

The following examples show the basic usage of SDKs written in different languages. See the [SDK](https://github.com/arh-eu/gds/wiki/SDK) section for more detailed instructions.

The examples show how to connect to a GDS instance, send an event with an attachment, and get this attachment. 
The examples do not cover the full functionality of the GDS, they only provide insight into the use of SDKs.
In addition, other operations can be performed, such as executing queries, retrieving events, and so on.

- [Java](#Java)
- [C#](#C)
- [PHP](#PHP)

#### Java

The SDK installation, the source code and other details can be found [here](https://github.com/arh-eu/gds-java-sdk).

First, we create the WebSocket client object, and connect to a GDS instance. 
```java
final Logger logger = Logger.getLogger("logging");

final GDSWebSocketClient client = new GDSWebSocketClient(
        "ws://127.0.0.1:8080/gate",
        "user",
        null,
        logger
);
```

We also subscribe to the MessageListener to access the received messages and to be notified of the connection state changes.
```java
client.setMessageListener(new MessageListener() {
    client.setMessageListener(new MessageListener() {
    @Override
    public void onMessageReceived(MessageHeader header, MessageData data) {
        // ...
    }
    @Override
    public void onConnected() {
        // ...
    }
    @Override
    public void onDisconnected() {
        // ...
    }
});
```

```java
client.connect();
```

If the connection was successful (client.connected() returns true, or if we have received a notification), we can send an event message.
```java
MessageHeader eventMessageHeader = MessageManager.createMessageHeaderBase("user", "870da92f-7fff-48af-825e-05351ef97acd", System.currentTimeMillis(), System.currentTimeMillis(), false, null, null, null, null, MessageDataType.CONNECTION_0);

List<String> operationsStringBlock = new ArrayList<String>();
operationsStringBlock.add("INSERT INTO events (id, some_field, images) VALUES('EVNT202001010000000000', 'some_field', array('ATID202001010000000000'));INSERT INTO \"events-@attachment\" (id, meta, data) VALUES('ATID202001010000000000', 'some_meta', 0x62696e6172795f6964315f6578616d706c65)");
Map<String, byte[]> binaryContentsMapping = new HashMap<>();
binaryContentsMapping.put("62696e6172795f69645f6578616d706c65", new byte[] { 1, 2, 3 });
MessageData eventMessageData = MessageManager.createMessageData2Event(operationsStringBlock, binaryContentsMapping, new ArrayList<PriorityLevelHolder>());

byte[] eventMessage = MessageManager.createMessage(eventMessageHeader, eventMessageData);

client.sendMessage(eventMessage);
```

If you want to get the attachment of to the prevoiusly stored event, you can send an attachment request message.
```java
MessageHeader eventMessageHeader = MessageManager.createMessageHeaderBase("user", "870da92f-7fff-48af-825e-05351ef97acd", System.currentTimeMillis(), System.currentTimeMillis(), false, null, null, null, null, MessageDataType.CONNECTION_0);
MessageData eventMessageData  = MessageManager.createMessageData4AttachmentRequest("SELECT * FROM \"events-@attachment\" WHERE id='ATID202001010000000000' and ownerid='EVNT202001010000000000' FOR UPDATE WAIT 86400");
byte[] eventMessage = MessageManager.createMessage(header, data);

client.sendMessage(eventMessage);
```

At the end, we close the websocket connection as well.
```java
client.close();
```

#### C#

The SDK installation, the source code and other details can be found [here](https://github.com/arh-eu/gds-csharp-sdk).

First, we create the WebSocket client object, and connect to a GDS instance.
```csharp
GDSWebSocketClient client = new GDSWebSocketClient("ws://127.0.0.1:8080/gate");
```

We also subscribe to the MessageListener to access the received messages.
```csharp
client.MessageReceived += Client_MessageReceived;

static void Client_MessageReceived(object sender, Tuple<Message, MessagePackSerializationException> e)
{
    //...
}
```

```csharp
client.ConnectSync();
```

If the connection was successful (client.IsConnected() returns true, or if we have received a notification), we can send an event message. 
Let’s see how we can send the message in synchronously.
```csharp
MessageHeader eventMessageHeader = MessageManager.GetHeader("user", "c08ea082-9dbf-4d96-be36-4e4eab6ae624", 1582612168230, 1582612168230, false, null, null, null, null, DataType.Event);
string operationsStringBlock = "INSERT INTO events (id, some_field, images) VALUES('EVNT202001010000000000', 'some_field', array('ATID202001010000000000'));INSERT INTO \"events-@attachment\" (id, meta, data) VALUES('ATID202001010000000000', 'some_meta', 0x62696e6172795f6964315f6578616d706c65)";
Dictionary<string, byte[]> binaryContentsMapping = new Dictionary<string, byte[]> { { "62696e6172795f69645f6578616d706c65", new byte[] { 1, 2, 3 } } };
MessageData eventMessageData = MessageManager.GetEventData(operationsStringBlock, binaryContentsMapping);
Message eventMessage = MessageManager.GetMessage(eventMessageHeader, eventMessageData);

Tuple<Message, MessagePackSerializationException> eventResponse = client.SendSync(eventMessage, 3000);
if (eventResponse.Item2 == null)
{
    Message eventResponseMessage = eventResponse.Item1;
    if (eventResponseMessage.Header.DataType.Equals(DataType.EventAck))
    {
        EventAckData eventAckData = eventResponseMessage.Data.AsEventAckData();
        // do something with the response data...
    }
}
```

If you want to get the attachment of to the prevoiusly stored event, you can send an attachment request message. Let's see an asynchronous example.
```csharp
MessageHeader attachmentRequestMessageHeader = MessageManager.GetHeader("user", "0292cbc8-df50-4e88-8be9-db392db07dbc", 1582612168230, 1582612168230, false, null, null, null, null, DataType.AttachmentRequest);
MessageData attachmentRequestMessageData = MessageManager.GetAttachmentRequestData("SELECT * FROM \"events-@attachment\" WHERE id='ATID202001010000000000' and ownerid='EVNT202001010000000000' FOR UPDATE WAIT 86400");
Message attachmentRequestMessage = MessageManager.GetMessage(attachmentRequestMessageHeader, attachmentRequestMessageData);

client.SendAsync(attachmentRequestMessage);
```

At the end, we close the websocket connection as well.

```csharp
client.CloseSync();
```

#### PHP

The SDK installation, the source code and other details can be found [here](https://github.com/arh-eu/gds-php-sdk).

First, we need to specify our connection details.
```php
$connectionInfo = new \App\Gds\ConnectionInfo(
        "ws://user@127.0.0.1:8080/gate",
        \App\Gds\Message\FragmentationInfo::noFragmentation(),
        true,
        0x01000000,
        false,
        null,
        null);

$logger = new \App\Gds\CustomLogger();
$eventLoop = new React\EventLoop\StreamSelectLoop();
$connection = new \App\Gds\Connection($connectionInfo, $eventLoop, $logger);
```

After that, we create a Gateway object. A Gateway object acts as a websocket gateway between application and a remote GDS instance.

```php
$gateway =  new \App\Gds\Gateway($connection, array('timeout' => 10), $logger);
```

Now, we can create the event message. It is not necessary to explicit create and send a connection message because it is done in the background based on the connection info.
```php
$eventMessageHeader = new \App\Gds\Message\MessageHeader("user", "0dc35f9d-ad70-46aa-8983-e57880b53c8b", time(), time(), App\Gds\Message\FragmentationInfo::noFragmentation(), 2);
$operationsStringBlock = "INSERT INTO events (id, some_field, images) VALUES('EVNT202001010000000000', 'some_field', array('ATID202001010000000000'));INSERT INTO \"events-@attachment\" (id, meta, data) VALUES('ATID202001010000000000', 'some_meta', 0x62696e6172795f6964315f6578616d706c65)";
$binaryContentsMapping = array("62696e6172795f69645f6578616d706c65" => pack("C*", 23, 17, 208));
$eventMessageData = new App\Gds\Message\DataTypes\DataType2($operationsStringBlock, $binaryContentsMapping, null);
$eventMessage = new \App\Gds\Message\Message($eventMessageHeader, $eventMessageData);
```

```php
$endpoint = new App\Gds\CustomEndpoint($gateway, $eventMessage);
$endpoint->start();
$response = $endpoint->getResponse();
```

### Carmen Cloud Service

[Here](https://github.com/arh-eu/carmen-cloud) you can read about our Carmen Cloud Service.
