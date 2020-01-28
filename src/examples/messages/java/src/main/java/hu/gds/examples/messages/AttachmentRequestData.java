package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;

public class AttachmentRequestData {

    /*
        [
            ..., --> 'header fields'
            "SELECT meta, data, "@to_valid" FROM "events-@attachment" WHERE id = ’ATID201811071434257890’ and ownerid = ’EVNT201811020039071890’ FOR UPDATE WAIT 86400"
        ]
     */
    public static void packData(MessagePacker packer) throws IOException {
        //DATA
        packer.packString("SELECT meta, data, \"@to_valid\" FROM \"events-@attachment\" WHERE id = ’ATID201811071434257890’ and ownerid = ’EVNT201811020039071890’ FOR UPDATE WAIT 86400");
    }

    public static void unpackData(MessageUnpacker unpacker) throws IOException {
        //DATA
        String request = unpacker.unpackString();
    }
}