/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator.responses;

import hu.gds.examples.simulator.Packable;
import org.msgpack.core.MessageBufferPacker;

import java.io.IOException;
import java.nio.ByteBuffer;

public abstract class ACKBase implements Packable {
    protected int globalStatus = 200; //OK
    protected String globalException = null;

    protected void notLoggedIn(MessageBufferPacker packer) throws IOException {
        packer.packInt(401); //UNAUTHORIZED
        packer.packNil(); //no body
        packer.packString("This user does not exist or has not sent Connection request yet!");
    }

    protected void packPixel(MessageBufferPacker packer) throws IOException {
        //Binary representation of a white pixel in BMP format
        int[] binaryData = {
                0x42, 0x4D, 0x3A, 0x0, 0x0, 0x0, 0x0, 0x0,
                0x0, 0x0, 0x36, 0x0, 0x0, 0x0, 0x28, 0x0,
                0x0, 0x0, 0x1, 0x0, 0x0, 0x0, 0x1, 0x0,
                0x0, 0x0, 0x1, 0x0, 0x18, 0x0, 0x0, 0x0,
                0x0, 0x0, 0x4, 0x0, 0x0, 0x0, 0x0, 0x0,
                0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
                0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xFF, 0xFF,
                0xFF, 0x0
        };
        ByteBuffer byteBuffer = ByteBuffer.allocate(binaryData.length * 4);
        byteBuffer.asIntBuffer().put(binaryData);
        byte[] bytes = byteBuffer.array();
        packer.packBinaryHeader(bytes.length);
        packer.writePayload(bytes);
    }
}
