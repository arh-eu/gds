package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;
import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.Value;
import org.msgpack.value.impl.ImmutableBinaryValueImpl;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static hu.gds.examples.messages.MessagePackUtil.*;

public class AttachmentRequestAckData {

    /*
        DIRECTION: Client --> GDS
       [
           ..., --> 'header fields'
           [ --> 'data'
               200, --> status
               [
                   201, --> status
                   { --> 'ack type data map'
                       'requestids': ['793ed37a-a30c-44cb-848b-ad30c1c52358'], --> 'request ids'
                       'ownertable': 'events', --> 'owner table'
                       'attachmentid': 'ATID202000000000000000', --> 'attachment id'
                       'ownerids': ['EVNT202000000000000000'], --> 'owner ids'
                       'meta': 'some_meta', --> 'meta'
                       'ttl': 86 400 000, --> ttl
                       'to_valid': null, --> 'to valid'
                       'attachment': [1,2,3] --> 'attachment'
                   },
                   null --> 'remained wait time millis'
               ],
               null --> 'exception'
           ]
       ]
    */
    public static void packData(MessagePacker packer) throws IOException {
        //DATA
        packer.packArrayHeader(3);

        //'global' status
        packer.packInt(200);

        //ack type data array
        packer.packArrayHeader(3);

        //'local' status
        packer.packInt(201);

        //result
        packer.packMapHeader(8);

        //request ids
        //map key
        packer.packString("requestids");
        //map value
        packer.packArrayHeader(1);
        packer.packString("793ed37a-a30c-44cb-848b-ad30c1c52358");

        //owner table
        //map key
        packer.packString("ownertable");
        //map value
        packer.packString("events");

        //attachment id
        //map key
        packer.packString("attachmentid");
        //map value
        packer.packString("ATID201811071434257890");

        //owner ids
        //map key
        packer.packString("ownerids");
        //may value
        packer.packArrayHeader(1);
        packer.packString("EVNT201811020039071890");

        //meta
        //map key
        packer.packString("meta");
        //map value
        packer.packString("some_meta");

        //ttl
        //map key
        packer.packString("ttl");
        //map value
        packer.packLong(86_400_000);

        //to valid
        //map key
        packer.packString("to_valid");
        //map value
        packer.packNil();

        //attachment
        //map key
        packer.packString("attachment");
        //map value
        packer.packValue(new ImmutableBinaryValueImpl(new byte[]{1,2,3}));

        //remained wait time millis
        packer.packNil();

        //exception
        packer.packNil();
    }

    public static void unpackData(MessageUnpacker unpacker) throws IOException {
        //DATA
        unpacker.unpackArrayHeader();

        //'global' status
        int globalStatus = unpacker.unpackInt();

        //ack type data array
        if(!unpacker.getNextFormat().getValueType().isNilType()) { //SUCCESS ACK
            unpacker.unpackArrayHeader();

            //'local' status
            int localStatus = unpacker.unpackInt();

            //result
            Map<Integer, String> resultMap = new HashMap<>();
            for (Map.Entry<Value, Value> entry : unpacker.unpackValue().asMapValue().map().entrySet()) {
                String key = entry.getKey().asStringValue().asString();
                Value value = entry.getValue();
                switch (key) {
                    //request ids
                    case "requestids":
                        List<String> requestIds = new ArrayList<>();
                        for(Value requestId: value.asArrayValue().list()) {
                            requestIds.add(requestId.asStringValue().asString());
                        }
                        break;
                    //owner table
                    case "ownertable":
                        String ownerTable = value.asStringValue().asString();
                        break;
                    //attachment id
                    case "attachmentid":
                        String attachmentId = value.asStringValue().asString();
                        break;
                    //owner ids
                    case "ownerids":
                        List<String> ownerIds = new ArrayList<>();
                        for(Value ownerId: value.asArrayValue().list()) {
                            ownerIds.add(ownerId.asStringValue().asString());
                        }
                        break;
                    //meta
                    case "meta":
                        String meta = value.isNilValue() ? null : value.asStringValue().asString();
                        break;
                    //ttl
                    case "ttl":
                        Long ttl = value.isNilValue() ? null : value.asNumberValue().toLong();
                        break;
                    //to valid
                    case "to_valid":
                        Long toValid = value.isNilValue() ? null : value.asNumberValue().toLong();
                        break;
                    //attachment
                    case "attachment":
                        byte[] attachment = value.asBinaryValue().asByteArray();
                        break;
                }
            }

            //remained wait time millis
            Long remainedWaitTimeMillis = unpackLong(unpacker);

        } else { //UNSUCCESS ACK
            unpacker.unpackNil();
        }

        //exception
        String exception = unpackString(unpacker);
    }
}