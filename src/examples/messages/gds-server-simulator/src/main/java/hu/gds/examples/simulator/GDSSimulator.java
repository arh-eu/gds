/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator;

import hu.gds.examples.simulator.requests.Connection;
import hu.gds.examples.simulator.requests.NextQuery;
import hu.gds.examples.simulator.requests.Query;
import hu.gds.examples.simulator.responses.ConnectionACK;
import hu.gds.examples.simulator.responses.QueryACK;
import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessagePack;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;

public class GDSSimulator {
    public static boolean user_logged_in = false;

    public byte[] handleRequest(byte[] request) throws IOException {
        MessageUnpacker unpacker = MessagePack.newDefaultUnpacker(request);

        int arraySize = unpacker.unpackArrayHeader();
        if (arraySize != 11) {
            throw new IllegalStateException("The Header of the Message should be an array of size 11, found "
                    + arraySize + " elements instead!");
        }
        MessageHeader header = MessageHeader.unpack(unpacker);

        MessageBufferPacker packer = MessagePack.newDefaultBufferPacker();
        packer.packArrayHeader(11);
        String username = header.getUsername();

        switch (header.getDataType()) {
            case CONNECTION:
                MessageHeader.create(username, DataType.CONNECTION_ACK).pack(packer);
                new ConnectionACK(header, new Connection(unpacker)).pack(packer);
                break;
            case EVENT:
                break;
            case EVENT_ACK:
                break;
            case ATTACHMENT_REQUEST:
                break;
            case ATTACHMENT_REQUEST_ACK:
                break;
            case ATTACHMENT_RESPONSE:
                break;
            case ATTACHMENT_RESPONSE_ACK:
                break;
            case EVENT_DOCUMENT:
                break;
            case EVENT_DOCUMENT_ACK:
                break;
            case QUERY_REQUEST:

                MessageHeader.create(username, DataType.QUERY_REQUEST_ACK).pack(packer);
                new QueryACK(header, new Query(unpacker)).pack(packer);
                break;
            case NEXT_QUERY_PAGE_REQUEST:
                MessageHeader.create(username, DataType.QUERY_REQUEST_ACK).pack(packer);
                new QueryACK(header, new NextQuery(unpacker)).pack(packer);
                break;
            default:
                //should never reach
                break;
        }

        return packer.toByteArray();
    }
}
