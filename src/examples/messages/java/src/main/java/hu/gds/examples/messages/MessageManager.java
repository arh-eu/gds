package hu.gds.examples.messages;

import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessagePack;
import org.msgpack.core.MessageUnpacker;
import sun.plugin2.message.EventMessage;

import java.io.IOException;

public class MessageManager {

    public static byte[] packMessage(DataType dataType) throws IOException {
        MessageBufferPacker packer = MessagePack.newDefaultBufferPacker();

        //Wrapper array
        packer.packArrayHeader(11);

        //HEADER
        MessageHeader.packHeader(packer, dataType);

        switch (dataType) {
            case CONNECTION:
                ConnectionData.packData(packer);
                break;
            case CONNECTION_ACK:
                ConnectionAckData.packData(packer);
                break;
            case EVENT:
                EventData.packData(packer);
                break;
            case ATTACHMENT_REQUEST:
                AttachmentRequestData.packData(packer);
                break;
            default:
                //TODO: hiba
                break;
        }

        return packer.toByteArray();
    }

    public static DataType unpackMessage(byte[] message) throws IOException {
        MessageUnpacker unPacker = MessagePack.newDefaultUnpacker(message);

        //Wrapper array
        unPacker.unpackArrayHeader();

        //HEADER
        DataType dataType = MessageHeader.unpackHeader(unPacker);

        switch (dataType) {
            case CONNECTION:
                ConnectionData.unpackData(unPacker);
                break;
            case CONNECTION_ACK:
                ConnectionAckData.unpackData(unPacker);
                break;
            case EVENT_ACK:
                EventAckData.unpackData(unPacker);
                break;
            case ATTACHMENT_REQUEST:
                AttachmentRequestData.unpackData(unPacker);
                break;
            default:
                //TODO: hiba
                break;
        }

        return dataType;
    }
}
