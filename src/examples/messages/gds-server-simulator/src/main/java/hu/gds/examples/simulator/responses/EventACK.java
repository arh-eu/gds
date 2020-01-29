/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/29
 */

package hu.gds.examples.simulator.responses;

import hu.gds.examples.simulator.GDSSimulator;
import hu.gds.examples.simulator.MessageHeader;
import hu.gds.examples.simulator.requests.Event;
import org.msgpack.core.MessageBufferPacker;

import java.io.IOException;

public class EventACK extends ACKBase {

    public EventACK(MessageHeader header, Event event) {

    }

    @Override
    public void pack(MessageBufferPacker packer) throws IOException {
        packer.packArrayHeader(3);
        if (GDSSimulator.user_logged_in) {
            packer.packInt(200);
            packer.packArrayHeader(2); //

            packer.packArrayHeader(4); //
            packer.packInt(201);
            packer.packNil();
            packer.packArrayHeader(0);
            packer.packArrayHeader(1);
            packer.packArrayHeader(6);
            packer.packInt(201);
            packer.packString("EVNT202001290039071890");
            packer.packString("sample_table");
            packer.packBoolean(true);
            packer.packInt(1);
            packer.packNil();

            packer.packArrayHeader(4); //
            packer.packInt(201);
            packer.packNil();
            packer.packArrayHeader(0);
            packer.packArrayHeader(1);
            packer.packArrayHeader(6);
            packer.packInt(201);
            packer.packString("ATID202001290039071890");
            packer.packString("sample_table-@attachment");
            packer.packBoolean(true);
            packer.packInt(1);
            packer.packNil();

            packer.packNil();
        } else {
            notLoggedIn(packer);
        }
    }
}
