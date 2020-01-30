package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;
import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.Value;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

import static hu.gds.examples.messages.MessagePackUtil.unpackInteger;
import static hu.gds.examples.messages.MessagePackUtil.unpackString;

public class EventDocumentAckData {

    /*
        [
            ..., --> 'header fields'
            [
                200, --> 'status'
                [ --> 'ack type data array'
                    [ --> '1. result'
                        201, --> 'status'
                        null, --> 'notification'
                        {} --> 'return values'
                    ]
                ],
                null --> 'exception'
            ]
        ]
     */
    public static void packData(MessagePacker packer) throws IOException {
        //DATA
        packer.packArrayHeader(3);

        //status
        packer.packInt(200);

        //ack type data array
        packer.packArrayHeader(1);

        //1. result
        packer.packArrayHeader(3);
        //status
        packer.packInt(201);
        //notification
        packer.packNil();
        //return values
        packer.packMapHeader(0);

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
            int resultsArraySize = unpacker.unpackArrayHeader();
            for(int i = 0; i < resultsArraySize; ++i) {
                unpacker.unpackArrayHeader();

                //status
                int localStatus = unpacker.unpackInt();

                //notification
                String notification = unpackString(unpacker);

                unpacker.unpackMapHeader();
            }

        } else { //UNSUCCESS ACK
            unpacker.unpackNil();
        }

        //exception
        String exception = unpackString(unpacker);
    }
}