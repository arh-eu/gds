package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;

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

    public static void unpackData(MessageUnpacker unpacker) throws IOException {
        //DATA
        unpacker.unpackArrayHeader();

        //select string block
        String selectStringBlock = unpacker.unpackString();

        //consistency type
        int consistencyType = unpacker.unpackInt();

        //timeout
        long timeout = unpacker.unpackLong();

        if(unpacker.hasNext()) {
            //query page size
            int queryPageSize = unpacker.unpackInt();

            if(unpacker.hasNext()) {
                //query type
                int queryType = unpacker.unpackInt();
            }
        }
    }
}