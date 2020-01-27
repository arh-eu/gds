/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator.responses;

import hu.gds.examples.simulator.Packable;
import org.msgpack.core.MessageBufferPacker;

import java.io.IOException;

public abstract class ACKBase implements Packable {
    protected int globalStatus = 200; //OK
    protected String globalException = null;

    protected void notLoggedIn(MessageBufferPacker packer) throws IOException {
        packer.packInt(401); //UNAUTHORIZED
        packer.packNil(); //no body
        packer.packString("This user does not exist or has not sent Connection request yet!");
    }
}
