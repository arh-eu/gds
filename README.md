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
- [C++](#C)
- [C#](#C-1)
- [PHP](#PHP)
- [Python](#Python)

#### Java

The SDK installation, the source code and other details can be found [here](https://github.com/arh-eu/gds-java-sdk).

First, we create the WebSocket client object, and connect to a GDS instance. 
```java
final Logger logger = Logger.getLogger("logging");

final GDSWebSocketClient client = new GDSWebSocketClient(
        "ws://127.0.0.1:8888/gate",
        "user",
        null,
        logger
);
```

We also subscribe to the MessageListener to access the received messages and to be notified of the connection state changes.
```java
client.setMessageListener(new MessageListener() {
    @Override
    public void onMessageReceived(MessageHeader header, MessageData data) {
        System.out.println(data.getTypeHelper().getMessageDataType() + " message received!");
        //do something with the message...
    }

    @Override
    public void onConnected() {
        System.out.println("Client connected!");
    }

    @Override
    public void onConnectionFailed(String s) {
        System.out.println("Connection failed: " + s);
    }

    @Override
    public void onDisconnected() {
        System.out.println("Client disconnected!");
    }
});
```

```java
client.connect();
```

If the connection was successful (client.connected() returns true, or if we have received a notification), we can send an event message.
```java
MessageIdGenerator messageIdGenerator = new MessageIdGenerator("TEST", "yyMMddhhmmssSSS");
String eventId = messageIdGenerator.nextId();
String attachmentId = messageIdGenerator.nextId();

int[] binaryData = {
        0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a, 0x00, 0x00, 0x00, 0x0d,
        0x49, 0x48, 0x44, 0x52, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x09,
        0x08, 0x06, 0x00, 0x00, 0x00, 0xe0, 0x91, 0x06, 0x10, 0x00, 0x00, 0x00,
        0x01, 0x73, 0x52, 0x47, 0x42, 0x00, 0xae, 0xce, 0x1c, 0xe9, 0x00, 0x00,
        0x00, 0x04, 0x67, 0x41, 0x4d, 0x41, 0x00, 0x00, 0xb1, 0x8f, 0x0b, 0xfc,
        0x61, 0x05, 0x00, 0x00, 0x00, 0x09, 0x70, 0x48, 0x59, 0x73, 0x00, 0x00,
        0x0e, 0xc3, 0x00, 0x00, 0x0e, 0xc3, 0x01, 0xc7, 0x6f, 0xa8, 0x64, 0x00,
        0x00, 0x00, 0x2d, 0x49, 0x44, 0x41, 0x54, 0x28, 0x53, 0x63, 0xf8, 0x4f,
        0x04, 0x20, 0x4d, 0xd1, 0x5b, 0x19, 0x15, 0x30, 0x46, 0x67, 0x83, 0x00,
        0x69, 0x8a, 0xf0, 0x01, 0xe2, 0x15, 0x21, 0x1b, 0x8d, 0x0c, 0x60, 0xe2,
        0x18, 0x6e, 0x42, 0xc6, 0x30, 0x40, 0x84, 0x75, 0xff, 0xff, 0x03, 0x00,
        0x18, 0xd8, 0x27, 0x9c, 0x9f, 0xb7, 0xe9, 0xa0, 0x00, 0x00, 0x00, 0x00,
        0x49, 0x45, 0x4e, 0x44, 0xae, 0x42, 0x60, 0x82
};
ByteArrayOutputStream baos = new ByteArrayOutputStream();
DataOutputStream dos = new DataOutputStream(baos);
for (int pixel : binaryData) {
    dos.writeByte(pixel);
}
byte[] byteArray = baos.toByteArray();
try {
    MessageData data = MessageManager.createMessageData2Event(
            new ArrayList<String>() {{
                add("INSERT INTO multi_event (id, plate, speed, images) VALUES('" + eventId + "', 'ABC123', 90, array('" + attachmentId +"'))");
                add("INSERT INTO \"multi_event-@attachment\" (id, meta, data) VALUES('" + attachmentId + "', 'some_meta', 0x62696e6172795f6964315f6578616d706c65)");
            }},
            new HashMap<String, byte[]>() {{
                put("binary_id1_example", byteArray);
            }},
            new ArrayList<>());
    client.sendMessage(data);
} catch (Throwable e) {
    e.printStackTrace();
}
```

If you want to get the attachment of to the previously stored event, you can send an attachment request message.
```java
try {
    MessageData data = MessageManager.createMessageData4AttachmentRequest(
            "SELECT * FROM \"multi_event-@attachment\" WHERE id='" + attachmentId +"' and ownerid='" + eventId +"' FOR UPDATE WAIT 86400");
    client.sendMessage(data);
} catch (Throwable e) {
    e.printStackTrace();
}
```

At the end, we close the websocket connection as well.
```java
client.close();
```

#### C++

The SDK installation, the source code and other details can be found [here](https://github.com/arh-eu/gds-cpp-sdk).

You can obtain a pointer for the implementation object by calling the static `gds_lib::connection::GDSInterface::create(..)` method. This has two overloads depending on if you want to use TLS security or not. If not, the only url that should be passed is the GDS gate url (fe. `192.168.111.222:8888/gate`).

```cpp
std::shared_ptr<gds_lib::connection::GDSInterface> mGDSInterface = gds_lib::connection::GDSInterface::create("192.168.111.222:8888/gate");
```


The client communicates with callbacks since it runs on a separate thread. You can specify your own callback function for the interface. It has four public members for this:
```cpp
struct GDSInterface {
  //...
  std::function<void()> on_open;
  std::function<void(gds_lib::gds_types::GdsMessage &)> on_message;
  std::function<void(int, const std::string&)> on_close;
  std::function<void(int, const std::string&)> on_error;
  //...
};
```

Your client code simply has to assign a value for these after you create the client:

```cpp
mGDSInterface->on_open    = [](){
	std::cout << "Client is open!" << std::endl;
};

mGDSInterface->on_close   = [](int status, const std::string& reason){
		std::cout << "Client closed: " << reason << " (code: " <<  code << ")" << std::endl;
};

mGDSInterface->on_error   = [](int code, const std::string& reason) {
	std::cout << "WebSocket returned error: " << reason << " (error code: " <<  code << ")" << std::endl;
};


mGDSInterface->on_message = [](gds_lib::gds_types::GdsMessage &msg) {
	std::cout << "I received a message!" << std::endl;
};
```

To start your client you should simply invoke the `start()` method. This will initialize and create the WebSocket connection to the GDS.
Keep in mind that this does not send the login message, that is done by manually if you use the SDK.

```cpp
mGDSInterface->start();
```	

Once the connection is ready (the `on_open` callback has been invoked), you are ready to send messages to the GDS and receive the replies.

```cpp
GdsMessage fullMessage = create_default_message();

fullMessage.dataType = GdsMsgType::LOGIN;

std::shared_ptr<GdsLoginMessage> loginBody(new GdsLoginMessage());
loginBody->serve_on_the_same_connection = false;
loginBody->fragmentation_supported = false;

fullMessage.messageBody = loginBody;
mGDSInterface->send(fullMessage);
```


If you no longer need the client, you should invoke the `close()` method, which sends the standard close message for the WebSocket connection.

```cpp
mGDSInterface->close();
```

For additional details please check the C++ page and the [SDK usage](https://github.com/arh-eu/gds-cpp-sdk#sdk-usage) chapter.

#### C#

The SDK installation, the source code and other details can be found [here](https://github.com/arh-eu/gds-csharp-sdk).

First, we create the WebSocket client object, and connect to a GDS instance.
```csharp
GdsWebSocketClient client = new GdsWebSocketClient("ws://127.0.0.1:8888/gate", "user", null);
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
client.Connect();
```

If the connection was successful (client.IsConnected() returns true, or if we have received a notification), we can send an event message. 
Let’s see how we can send the message in synchronously.
```csharp
MessageHeader eventMessageHeader = MessageManager.GetHeader("user", "c08ea082-9dbf-4d96-be36-4e4eab6ae624", 1582612168230, 1582612168230, false, null, null, null, null, DataType.Event);
string operationsStringBlock = "INSERT INTO multi_event (id, plate, images) VALUES('EVNT2006241023125470', 'ABC123', array('ATID2006241023125470'));INSERT INTO \"multi_event-@attachment\" (id, meta, data) VALUES('ATID2006241023125470', 'some_meta', 0x62696e6172795f69645f6578616d706c65)";
Dictionary<string, byte[]> binaryContentsMapping = new Dictionary<string, byte[]> { { "62696e6172795f69645f6578616d706c65", new byte[] { 1, 2, 3 } } };
MessageData eventMessageData = MessageManager.GetEventData(operationsStringBlock, binaryContentsMapping);
Message eventMessage = MessageManager.GetMessage(eventMessageHeader, eventMessageData);

try
{
    Message eventResponseMessage = client.SendSync(eventMessage, 3000);        
    if (eventResponseMessage.Header.DataType.Equals(DataType.EventAck))
    {
        EventAckData eventAckData = eventResponseMessage.Data.AsEventAckData();
        // do something with the response data...
    }
}
catch(TimeoutException exception)
{
    // ...
}
catch (MessagePackSerializationException exception)
{
    // ...
}
```

If you want to get the attachment of to the prevoiusly stored event, you can send an attachment request message. Let's see an asynchronous example.
```csharp
MessageHeader attachmentRequestMessageHeader = MessageManager.GetHeader("user", "0292cbc8-df50-4e88-8be9-db392db07dbc", 1582612168230, 1582612168230, false, null, null, null, null, DataType.AttachmentRequest);
MessageData attachmentRequestMessageData = MessageManager.GetAttachmentRequestData("SELECT * FROM \"multi_event-@attachment\" WHERE id='ATID2006241023125470' and ownerid='EVNT2006241023125470' FOR UPDATE WAIT 86400");
Message attachmentRequestMessage = MessageManager.GetMessage(attachmentRequestMessageHeader, attachmentRequestMessageData);

client.SendAsync(attachmentRequestMessage);
```

At the end, we close the websocket connection as well.

```csharp
client.Close();
```

#### PHP

The SDK installation, the source code and other details can be found [here](https://github.com/arh-eu/gds-php-sdk).

First, we need to specify our connection details.
```php
$connectionInfo = new \App\Gds\ConnectionInfo(
        "ws://user@127.0.0.1:8888/gate",
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
$operationsStringBlock = "INSERT INTO multi_event (id, plate, images) VALUES('EVNT2006241023125470', 'ABC123', array('ATID2006241023125470'));INSERT INTO \"multi_event-@attachment\" (id, meta, data) VALUES('ATID2006241023125470', 'some_meta', 0x62696e6172795f69645f6578616d706c65)";
$binaryContentsMapping = array("62696e6172795f69645f6578616d706c65" => pack("C*", 23, 17, 208));
$eventMessageData = new App\Gds\Message\DataTypes\DataType2($operationsStringBlock, $binaryContentsMapping, null);
$eventMessage = new \App\Gds\Message\Message($eventMessageHeader, $eventMessageData);
```

```php
$endpoint = new App\Gds\CustomEndpoint($gateway, $eventMessage);
$endpoint->start();
$response = $endpoint->getResponse();
```


#### Python

The SDK installation, the source code and other details can be found [here](https://github.com/arh-eu/gds-python-sdk).

We can specify our connection details.
```python
class CustomGDSClient(GDSClient.WebsocketClient):
    def __init__(self, **kwargs):
        super().__init__(url="ws://192.168.111.222:8888/gate", **kwargs)
```

To provide the client logic, we need to override the `client_code(..)` method from our base class. The signature is the following:

```python

class CustomGDSClient(GDSClient.WebsocketClient):
    def __init__(self, **kwargs):
        super().__init__(url="ws://192.168.111.222:8888/gate", **kwargs)

    async def client_code(self, ws: websockets.WebSocketClientProtocol):
        #our code comes here
        pass

```

Now, we can create the event message. It is not necessary to explicit create and send a connection message because it is done in the background based on the connection info. If you want to customize the information, see the Python wiki for more details.

```python
    async def client_code(self, ws: websockets.WebSocketClientProtocol):
        insert_string = "INSERT INTO multi_event (id, plate, images) VALUES('EVNT2006241023125470', 'ABC123', array('ATID2006241023125470'));INSERT INTO \"multi_event-@attachment\" (id, meta, data) VALUES('ATID2006241023125470', 'some_meta', 0x62696e6172795f69645f6578616d706c65)"
        await self.send_and_wait_event(ws, insert_string)
```

To handle the reply the `event_ack(..)` function should be overwritten. By default it prints the response to the console and saves it as a `JSON` file in the `exports` folder.

```python
    def event_ack(self, response: list, **kwargs):
        #super().event_ack(response, **kwargs)
        pass
```
