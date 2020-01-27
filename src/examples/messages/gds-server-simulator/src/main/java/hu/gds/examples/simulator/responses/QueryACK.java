/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator.responses;

import hu.gds.examples.simulator.GDSSimulator;
import hu.gds.examples.simulator.MessageHeader;
import hu.gds.examples.simulator.requests.NextQuery;
import hu.gds.examples.simulator.requests.Query;
import org.msgpack.core.MessageBufferPacker;

import java.io.IOException;

public class QueryACK extends ACKBase {

    protected final boolean firstQuery;

    public QueryACK(MessageHeader header, Query query) {
        firstQuery = true;
    }

    public QueryACK(MessageHeader header, NextQuery query) {
        firstQuery = false;
    }

    @Override
    public void pack(MessageBufferPacker packer) throws IOException {
        packer.packArrayHeader(3);
        if (GDSSimulator.user_logged_in) {
            packer.packInt(globalStatus); //globalStatus OK

            packer.packArrayHeader(6); //object
            final int resultSize = 100;

            packer.packInt(resultSize);
            packer.packInt(10);
            packer.packBoolean(firstQuery);
            packer.packArrayHeader(9);
            for (int i = 1; i <= 9; ++i) {
                packer.packString("QueryContextField" + i);
            }
            //3 fields
            packer.packArrayHeader(3);
            for (int i = 1; i <= 3; ++i) {
                packer.packArrayHeader(3);
                packer.packString("field_name_" + i);
                packer.packString("field_type_" + i);
                packer.packString("mime_type_" + i);
            }

            //100 rows
            packer.packArrayHeader(resultSize);
            int start = firstQuery ? 1 : resultSize + 1;

            for (int i = start; i <= start + resultSize; ++i) {
                for (int j = 1; j <= 3; ++j) {
                    packer.packString("row_" + i + "_value_" + j);
                }
            }
            packer.packNil(); //global_exception null
        } else {
            notLoggedIn(packer);
        }
    }
}
