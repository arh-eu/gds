package hu.gds.examples.messages;

import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessagePack;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;

public class AttachmentRequestMessageExample {

    /*
        [
            ..., --> 'header fields'
            "SELECT meta, data, "@to_valid" FROM "events-@attachment" WHERE id = ’ATID201811071434257890’ and ownerid = ’EVNT201811020039071890’ FOR UPDATE WAIT 86400"
        ]
     */
    public static byte[] packMessage() throws IOException {
        MessageBufferPacker packer = MessagePack.newDefaultBufferPacker();

        //Wrapper array
        packer.packArrayHeader(11);

        //HEADER
        Utils.packHeader(packer, DataType.ATTACHMENT_REQUEST.getValue());

        //DATA
        packer.packString("SELECT meta, data, \"@to_valid\" FROM \"events-@attachment\" WHERE id = ’ATID201811071434257890’ and ownerid = ’EVNT201811020039071890’ FOR UPDATE WAIT 86400");

        return packer.toByteArray();
    }

    public static void unpackMessage(byte[] message) throws IOException {
        MessageUnpacker unpacker = MessagePack.newDefaultUnpacker(message);

        //Wrapper array
        unpacker.unpackArrayHeader();

        //HEADER
        Utils.unpackHeader(unpacker);

        //DATA
        String request = unpacker.unpackString();
    }
}
