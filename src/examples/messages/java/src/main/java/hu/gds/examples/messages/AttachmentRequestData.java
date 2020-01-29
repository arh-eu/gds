package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;

public class AttachmentRequestData {

    /*
        DIRECTION: Client --> GDS
        [
            ..., --> 'header fields'
            "SELECT meta, data, "@to_valid" FROM "events-@attachment" WHERE id = ’ATID202000000000000000’ and ownerid = ’EVNT202000000000000000’ FOR UPDATE WAIT 86400"
        ]
     */
    public static void packData(MessagePacker packer) throws IOException {
        //DATA
        packer.packString("SELECT meta, data, \"@to_valid\" FROM \"events-@attachment\" WHERE id = ’ATID202000000000000000’ and ownerid = ’EVNT202000000000000000’ FOR UPDATE WAIT 86400");
    }

    public static void unpackData(MessageUnpacker unpacker) throws IOException {
        //DATA
        String request = unpacker.unpackString();
    }
}