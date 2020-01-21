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
        MessageUnpacker unPacker = MessagePack.newDefaultUnpacker(message);

        //Wrapper array
        unPacker.unpackArrayHeader();

        //HEADER
        Utils.unpackHeader(unPacker);

        //DATA
        unPacker.unpackArrayHeader();

        //status
        int status = unPacker.unpackInt();

        //ack type data array
        if (unPacker.getNextFormat().getValueType().isArrayType()) { //SUCCESS ACK
            unPacker.unpackArrayHeader();

            //serve on the same connection
            boolean serveOnTheSameConnection = unPacker.unpackBoolean();

            //protocol version number
            int protocolVersionNumber = unPacker.unpackInt();

            //fragmentation supported
            boolean fragmentationSupported = unPacker.unpackBoolean();

            //fragmentation transmission unit
            Integer fragmentationTransmissionUnit = Utils.unpackInteger(unPacker);

            if (unPacker.hasNext()) {
                if (!unPacker.getNextFormat().getValueType().isNilType()) {
                    //reserved fields
                    unPacker.unpackArrayHeader();
                    //password
                    String password = Utils.unpackString(unPacker);
                }
            }
        } else { //UNSUCCESS ACK
            Map<Integer, String> ackTypeDataMap = new HashMap<>();
            for (Map.Entry<Value, Value> entry : unPacker.unpackValue().asMapValue().map().entrySet()) {
                ackTypeDataMap.put(entry.getKey().asIntegerValue().asInt(), entry.getValue().asStringValue().asString());
            }
        }

        //exception
        String exception = Utils.unpackString(unPacker);
    }
}
