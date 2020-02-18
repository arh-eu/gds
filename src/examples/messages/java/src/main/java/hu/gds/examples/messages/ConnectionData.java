package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;
import org.msgpack.core.MessageUnpacker;
import static hu.gds.examples.messages.MessagePackUtil.*;

import java.io.IOException;

/*
        Client --> GDS
        GDS --> Client
 */
public class ConnectionData {

    /*
        [
            ..., --> 'header fields'
            [ --> 'data'
                false, --> 'serve on the same connection'
                1, --> 'protocol version number'
                false, --> 'fragmentation supported'
                null --> 'fragmentation transmission unit'
            ]
        ]
     */
    public static void packData(MessagePacker packer) throws IOException {
        //DATA
        //Write the value 5 if you want to use the 'password' field as well
        packer.packArrayHeader(4);

        //serve on the same connection
        packer.packBoolean(false);

        //protocol version number
        packer.packInt(1);

        //fragmentation supported
        packer.packBoolean(false);

        //fragmentation transmission unit
        packer.packNil();

        /*
        //reserved fields
        packer.packArrayHeader(1);
        //password
        packer.packString("password_example");
        */
    }

    public static void unpackData(MessageUnpacker unpacker) throws IOException {
        //DATA
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
    }
}
