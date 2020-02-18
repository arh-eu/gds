package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;

import java.io.IOException;

/*
        Client --> GDS
 */
public class QueryRequestData {

    /*
        [
            ..., --> 'header fields...'
            [ --> 'data'
                "SELECT * FROM events", --> 'select string block'
                "NONE", --> 'consistency type'
                10000 --> 'timeout'
            ]
        ]
     */
    public static void packData(MessagePacker packer) throws IOException {
        //DATA
        packer.packArrayHeader(3);

        //select string block
        packer.packString("SELECT * FROM events");

        //consistency type
        packer.packString("NONE");

        //timeout
        packer.packLong(10_000);
    }
}