/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator;

import hu.gds.examples.simulator.requests.Connection;
import hu.gds.examples.simulator.requests.Event;
import hu.gds.examples.simulator.requests.NextQuery;
import hu.gds.examples.simulator.requests.Query;
import hu.gds.examples.simulator.responses.ConnectionACK;
import hu.gds.examples.simulator.responses.EventACK;
import hu.gds.examples.simulator.responses.InvalidACK;
import hu.gds.examples.simulator.responses.QueryACK;
import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessagePack;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;
import java.util.logging.Logger;

public class GDSSimulator {
    public static boolean user_logged_in = false;
    private static final Logger LOGGER = Logger.getLogger("GDSSimulator");

    public byte[] handleRequest(byte[] request) throws IOException {
        MessageUnpacker unpacker = MessagePack.newDefaultUnpacker(request);

        int arraySize = unpacker.unpackArrayHeader();
        if (arraySize != 11) {
            LOGGER.warning("Invalid header format");
            throw new IllegalStateException("The Header of the Message should be an array of size 11, found "
                    + arraySize + " elements instead!");
        }
        MessageHeader header = MessageHeader.unpack(unpacker);

        MessageBufferPacker packer = MessagePack.newDefaultBufferPacker();
        packer.packArrayHeader(11);
        String username = header.getUsername();

        DataType headerDataType = header.getDataType();


        LOGGER.info("GDS has received a message of type '" + headerDataType.name() + "'..");
        switch (headerDataType) {
            case CONNECTION:
                MessageHeader.create(username, DataType.CONNECTION_ACK).pack(packer);
                new ConnectionACK(header, new Connection(unpacker)).pack(packer);
                LOGGER.info("Sending back the CONNECTION_ACK..");
                break;
            case EVENT:
                MessageHeader.create(username, DataType.EVENT_ACK).pack(packer);
                new EventACK(header, new Event(unpacker)).pack(packer);
                LOGGER.info("Sending back the EVENT_ACK..");
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
            case QUERY_REQUEST:
                MessageHeader.create(username, DataType.QUERY_REQUEST_ACK).pack(packer);
                new QueryACK(header, new Query(unpacker)).pack(packer);
                break;
            case NEXT_QUERY_PAGE_REQUEST:
                MessageHeader.create(username, DataType.QUERY_REQUEST_ACK).pack(packer);
                new QueryACK(header, new NextQuery(unpacker)).pack(packer);
                break;


            case CONNECTION_ACK:
            case EVENT_ACK:
            case EVENT_DOCUMENT_ACK:
            case QUERY_REQUEST_ACK:
                //error
                MessageHeader.create(username, headerDataType).pack(packer);
                new InvalidACK(headerDataType).pack(packer);
                break;

            default:
                //should never reach
                break;
        }


        LOGGER.info("GDS Response successfully sent!");
        return packer.toByteArray();
    }
}