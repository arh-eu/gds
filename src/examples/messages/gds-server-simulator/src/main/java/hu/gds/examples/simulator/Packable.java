/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator;

import org.msgpack.core.MessageBufferPacker;

import java.io.IOException;

public interface Packable {
    void pack(MessageBufferPacker packer) throws IOException;
}
