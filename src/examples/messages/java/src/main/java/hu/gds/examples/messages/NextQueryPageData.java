package hu.gds.examples.messages;

import org.msgpack.core.MessagePacker;

import java.io.IOException;

public class NextQueryPageData {

    /*
        [
            ..., --> 'header fields'
            [
                "2b22a5a84966df20a3a44793476a55c45bc06418d964bc1d9009a6e859a1bf4e", --> 'scroll id'
                "SELECT * FROM events", --> 'query'
                100, --> 'delivered number of hits'
                202000000000000000, --> 'query start time'
                "NONE", --> 'consistency type'
                "bucket_id", --> 'last bucket id'
                [ --> 'gds descriptor'
                    "gds_cluster", --> 'gds cluster'
                    "gds_node", --> 'gds node'
                ],
                [], --> 'field values'
                [] --> 'partition names'
            ]
        ]
     */
    public static void packData(MessagePacker packer) throws IOException {
        //DATA
        packer.packArrayHeader(9);

        //scroll id
        packer.packString("2b22a5a84966df20a3a44793476a55c45bc06418d964bc1d9009a6e859a1bf4e");

        //query
        packer.packString("SELECT * FROM events");

        //delivered number of hits
        packer.packInt(100);

        //query start time
        packer.packLong(202000000000000000L);

        //consistency type
        packer.packString("NONE");

        //last bucket id
        packer.packString("bucket_id");

        //gds descriptor
        packer.packArrayHeader(2);

        //gds cluster
        packer.packString("gds_cluster");

        //gds node
        packer.packString("gds_node");

        //field values
        packer.packArrayHeader(0);

        //partition names
        packer.packArrayHeader(0);
    }
}
