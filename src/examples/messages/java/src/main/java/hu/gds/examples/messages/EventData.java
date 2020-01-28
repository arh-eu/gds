package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;
import org.msgpack.value.impl.ImmutableBinaryValueImpl;

import java.io.IOException;

public class EventData {

    /*
        [
            ..., --> 'header fields'
            [ --> 'data'
                "INSERT INTO events (id, some_field, images) VALUES('EVNT201811020039071890', 'some_text', array('ATID201811071434257890'));
                INSERT INTO events-@attachment (id, meta, image) VALUES('ATID201811071434257890', 'some_meta', 0x62696e6172795f6964315f6578616d706c65)", --> 'operations string block'
                { --> 'mapping of binary contents'
                    { "62696e6172795f69645f6578616d706c65": [1,2,3] }
                },
                [ --> 'execution priority structure'
                    [ --> '0. priority level'
                        { 1: true }
                    ],
                    [ --> '1. priority level'
                        { 2: false }
                    ]
                ]
            ]
        ]
     */
    public static void packData(MessagePacker packer) throws IOException {
        //DATA
        packer.packArrayHeader(3);

        //operations string block
        packer.packString("INSERT INTO events (id, some_field, images) VALUES('EVNT201811020039071890', 'some_text', array('ATID201811071434257890'));" +
                "INSERT INTO events-@attachment (id, meta, image) VALUES('ATID201811071434257890', 'some_meta', 0x62696e6172795f6964315f6578616d706c65)");

        //mapping of binary contents
        packer.packMapHeader(1);
        packer.packString("62696e6172795f69645f6578616d706c65");
        packer.packValue(new ImmutableBinaryValueImpl(new byte[]{1, 2, 3}));

        //execution priority structure
        packer.packArrayHeader(2);
        //0. priority level
        packer.packArrayHeader(1);
        packer.packMapHeader(1);
        packer.packInt(1);
        packer.packBoolean(true);
        //1. priority level
        packer.packArrayHeader(1);
        packer.packMapHeader(1);
        packer.packInt(2);
        packer.packBoolean(false);
    }
}
