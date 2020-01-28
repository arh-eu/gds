package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;
import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.Value;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

import static hu.gds.examples.messages.MessagePackUtil.*;

public class ConnectionAckData {

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
    public static void packData(MessagePacker packer) throws IOException {
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
    }

    public static void unpackData(MessageUnpacker unpacker) throws IOException {
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
            Integer fragmentationTransmissionUnit = unpackInteger(unpacker);

            if (unpacker.hasNext()) {
                if (!unpacker.getNextFormat().getValueType().isNilType()) {
                    //reserved fields
                    unpacker.unpackArrayHeader();
                    //password
                    String password = unpackString(unpacker);
                }
            }
        } else { //UNSUCCESS ACK
            Map<Integer, String> ackTypeDataMap = new HashMap<>();
            for (Map.Entry<Value, Value> entry : unpacker.unpackValue().asMapValue().map().entrySet()) {
                Integer key = entry.getKey().asIntegerValue().asInt();
                String value = entry.getValue().asStringValue().asString();
                ackTypeDataMap.put(key, value);
            }
        }

        //exception
        String exception = unpackString(unpacker);
    }
}
