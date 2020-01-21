package hu.gds.examples.messages;

import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessagePack;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;

public class ConnectionMessageExample {

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
    public static byte[] packMessage() throws IOException {
        MessageBufferPacker packer = MessagePack.newDefaultBufferPacker();

        //Wrapper array
        packer.packArrayHeader(11);

        //HEADER
        Utils.packHeader(packer, DataType.CONNECTION.getValue());

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
    }
}
