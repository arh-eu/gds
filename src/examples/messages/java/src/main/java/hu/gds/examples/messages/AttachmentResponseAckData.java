package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;

/*
        Client --> GDS
        GDS --> Client
 */
public class AttachmentResponseAckData {

    /*
        [
            ... --> 'header fields'
            [ --> 'data'
                200, --> status
                [ --> 'ack type data array'
                    201, --> 'status'
                ],
                null --> 'exception'
            ]
        ]
     */
    public static void packData(MessagePacker packer) throws IOException {

    }

    public static void unpackData(MessageUnpacker unpacker) throws IOException {

    }
}