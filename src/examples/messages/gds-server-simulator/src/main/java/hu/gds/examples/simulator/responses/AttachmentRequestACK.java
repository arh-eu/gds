/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/02/03
 */

package hu.gds.examples.simulator.responses;

import hu.gds.examples.simulator.GDSSimulator;
import hu.gds.examples.simulator.MessageHeader;
import hu.gds.examples.simulator.requests.AttachmentRequest;
import org.msgpack.core.MessageBufferPacker;

import java.io.IOException;

public class AttachmentRequestACK extends ACKBase {
    public AttachmentRequestACK(MessageHeader header, AttachmentRequest request) {
    }

    @Override
    public void pack(MessageBufferPacker packer) throws IOException {
        packer.packArrayHeader(3);
        if (GDSSimulator.user_logged_in) {
            packer.packInt(200);

            packer.packArrayHeader(3);

            packer.packInt(201);
            packer.packMapHeader(8);

            packer.packString("requestids");
            packer.packArrayHeader(2);
            packer.packString("request_id_1");
            packer.packString("request_id_2");

            packer.packString("ownertable");
            packer.packString("owner_table");

            packer.packString("attachmentid");
            packer.packString(System.currentTimeMillis() + "-id");

            packer.packString("ownerids");
            packer.packArrayHeader(1);
            packer.packString("owner1");

            packer.packString("meta");
            packer.packString("image/bmp");

            packer.packString("ttl");
            packer.packLong(60 * 60 * 1000);

            packer.packString("to_valid");
            packer.packLong(60 * 60 * 1000);

            packer.packString("attachment");

            packPixel(packer);

            packer.packLong(0);

            packer.packNil();
        } else {
            notLoggedIn(packer);
        }
    }
}
