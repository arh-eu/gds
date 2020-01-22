package hu.gds.examples.messages;

import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessagePack;
import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.Value;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class ConnectionAckMessageExample {

    /*
        [
            ..., --> 'header fields'
            [ --> 'data'
                200, --> 'status'
                [ --> 'ack type data array'
                    false, --> 'serve on the same connection'
                    1, --> 'protocol version number'
                    false, --> 'fragmentation supported'
                    null --> 'fragmentation transmission unit'
                ],
                null --> 'exception'
            ],
        ]
     */
    public static byte[] packMessage() throws IOException {
        MessageBufferPacker packer = MessagePack.newDefaultBufferPacker();

        //Wrapper array
        packer.packArrayHeader(11);

        //HEADER
        Utils.packHeader(packer, DataType.CONNECTION_ACK.getValue());

        //DATA
        packer.packArrayHeader(3);

        //status
        packer.packInt(200);

        //ack type data array
        packer.packArrayHeader(4);

        //serve on the same connection
        packer.packBoolean(false);

        //protocol version number
        packer.packInt(1);

        //fragmentation supported
        packer.packBoolean(false);

        //fragmentation transmission unit
        packer.packNil();

        //exception
        packer.packNil();

        return packer.toByteArray();
    }

    public static void unpackMessage(byte[] message) throws IOException {
        MessageUnpacker unpacker = MessagePack.newDefaultUnpacker(message);

        //Wrapper array
        unpacker.unpackArrayHeader();

        //HEADER
        Utils.unpackHeader(unpacker);

        //DATA
        unpacker.unpackArrayHeader();

        //status
        int status = unpacker.unpackInt();

        //ack type data array
        if (unpacker.getNextFormat().getValueType().isArrayType()) { //SUCCESS ACK
            unpacker.unpackArrayHeader();

            //serve on the same connection
            boolean serveOnTheSameConnection = unpacker.unpackBoolean();

            //protocol version number
            int protocolVersionNumber = unpacker.unpackInt();

            //fragmentation supported
            boolean fragmentationSupported = unpacker.unpackBoolean();

            //fragmentation transmission unit
            Integer fragmentationTransmissionUnit = Utils.unpackInteger(unpacker);

            if (unpacker.hasNext()) {
                if (!unpacker.getNextFormat().getValueType().isNilType()) {
                    //reserved fields
                    unpacker.unpackArrayHeader();
                    //password
                    String password = Utils.unpackString(unpacker);
                }
            }
        } else { //UNSUCCESS ACK
            Map<Integer, String> ackTypeDataMap = new HashMap<>();
            for (Map.Entry<Value, Value> entry : unpacker.unpackValue().asMapValue().map().entrySet()) {
                ackTypeDataMap.put(entry.getKey().asIntegerValue().asInt(), entry.getValue().asStringValue().asString());
            }
        }

        //exception
        String exception = Utils.unpackString(unpacker);
    }
}
