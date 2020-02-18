/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator.responses;

import hu.gds.examples.simulator.GDSSimulator;
import hu.gds.examples.simulator.MessageHeader;
import hu.gds.examples.simulator.requests.Connection;
import org.msgpack.core.MessageBufferPacker;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.Objects;

public class ConnectionACK extends ACKBase {

    private final static String allowed_user = "user";
    private final Connection data;
    private final Map<Integer, String> errors;


    public ConnectionACK(MessageHeader header, Connection request) {
        if (!Objects.equals(allowed_user, header.getUsername())) {
            globalStatus = 401;
            globalException = "This user is not allowed!";
            data = null;
            errors = new HashMap<>();
            errors.put(0, "There is no user named '" + header.getUsername() + "'!");
        } else {
            data = request;
            errors = null;
            GDSSimulator.user_logged_in = true;
        }
    }

    @Override
    public void pack(MessageBufferPacker packer) throws IOException {
        packer.packArrayHeader(3);

        packer.packInt(globalStatus);

        if (data == null) {
            packer.packMapHeader(1);
            Map.Entry<Integer, String> entry = errors.entrySet().iterator().next();
            packer.packInt(entry.getKey());
            packer.packString(entry.getValue());
        } else {
            data.pack(packer);
        }
        if(globalException == null) {
            packer.packNil();
        } else {
            packer.packNil();
        }
    }
}
