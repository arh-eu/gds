/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/29
 */

package hu.gds.examples.simulator.responses;

import hu.gds.examples.simulator.DataType;
import hu.gds.examples.simulator.GDSSimulator;
import org.msgpack.core.MessageBufferPacker;

import java.io.IOException;

public class InvalidACK extends ACKBase {
    private final DataType dataType;

    public InvalidACK(DataType dataType) {
        this.dataType = dataType;
    }

    @Override
    public void pack(MessageBufferPacker packer) throws IOException {
        packer.packArrayHeader(3);
        if (GDSSimulator.user_logged_in) {
            packer.packInt(400); // BAD_REQUEST
            packer.packNil();
            String message = "GDS cannot serve " + dataType.name() + " requests!";
            packer.packString(message);
        } else {
            notLoggedIn(packer);
        }
    }
}
