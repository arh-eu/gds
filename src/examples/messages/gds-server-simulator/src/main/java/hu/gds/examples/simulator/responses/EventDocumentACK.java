/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/02/03
 */

package hu.gds.examples.simulator.responses;

import hu.gds.examples.simulator.GDSSimulator;
import hu.gds.examples.simulator.MessageHeader;
import hu.gds.examples.simulator.requests.EventDocument;
import org.msgpack.core.MessageBufferPacker;

import java.io.IOException;

public class EventDocumentACK extends ACKBase {

    public EventDocumentACK(MessageHeader header, EventDocument document) {

    }

    @Override
    public void pack(MessageBufferPacker packer) throws IOException {
        packer.packArrayHeader(3);
        if (GDSSimulator.user_logged_in) {
            packer.packInt(200);

            packer.packArrayHeader(3);
            for (int i = 1; i <= 3; ++i) {
                packer.packArrayHeader(3);
                packer.packInt(202);
                packer.packNil();
                packer.packMapHeader(0);
            }
            packer.packNil();
        } else {
            notLoggedIn(packer);
        }
    }
}
