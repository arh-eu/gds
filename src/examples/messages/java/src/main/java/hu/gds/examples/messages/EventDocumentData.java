package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;
import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.Value;

import java.io.IOException;
import java.util.List;

/*
        Client --> GDS
        GDS --> Client
 */
public class EventDocumentData {

    /*
        [
	        ..., --> 'header fields'
	        [
                "events", --> 'table name'
                [ --> 'field descriptors'
                    ["id", "KEYWORD", ""],
                    ["some_field", "TEXT", ""],
                    ["images", "TEXT_ARRAY", "image/jpeg"]
                ],
                [ --> 'records'
                    [
                        "EVNT202000000000000000", --> 'id (in MessagePack format)'
                        "some_text", --> 'some_field (in MessagePack format)'
                        ["ATID202000000000000000"] --> 'images (in MessagePack format)'
                    ]
                ],
                {} --> 'returning options'
            ]
        ]
     */
    public static void packData(MessagePacker packer) throws IOException {
        //DATA
        packer.packArrayHeader(4);

        //'table name'
        packer.packString("events");

        //'field descriptors'
        packer.packArrayHeader(3);

        //1. field descriptor
        packer.packArrayHeader(3);
        //'field name'
        packer.packString("id");
        //'field type'
        packer.packString("KEYWORD");
        //'mime type'
        packer.packString("");

        //2. field descriptor
        packer.packArrayHeader(3);
        //'field name'
        packer.packString("some_field");
        //'field type'
        packer.packString("TEXT");
        //'mime type'
        packer.packString("");

        //3. field descriptor
        packer.packArrayHeader(3);
        //'field name'
        packer.packString("images");
        //'field type'
        packer.packString("TEXT_ARRAY");
        //'mime type'
        packer.packString("image/jpeg");

        //records
        packer.packArrayHeader(1);
        //1. record
        packer.packArrayHeader(3);
        //id
        packer.packString("EVNT202000000000000000");
        //some field
        packer.packString("some_text");
        //images
        packer.packArrayHeader(1);
        packer.packString("ATID202000000000000000");
    }

    public static void unpackData(MessageUnpacker unpacker) throws IOException {
        //DATA
        unpacker.unpackArrayHeader();

        //table name
        String tableName = unpacker.unpackString();

        //field descriptors
        int fieldDescriptorsArraySize = unpacker.unpackArrayHeader();

        for(int i = 0; i < fieldDescriptorsArraySize; ++i) {
            unpacker.unpackArrayHeader();
            //field name
            String fieldName = unpacker.unpackString();
            //field type
            String fieldType = unpacker.unpackString();
            //mime type
            String mimeType = unpacker.unpackString();
        }

        //records
        int recordsArraySize = unpacker.unpackArrayHeader();
        for(int j = 0; j < recordsArraySize; ++j) {
            List<Value> values = unpacker.unpackValue().asArrayValue().list();
        }
    }
}