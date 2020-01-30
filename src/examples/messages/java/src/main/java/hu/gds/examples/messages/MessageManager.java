package hu.gds.examples.messages;

import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessagePack;
import org.msgpack.core.MessageUnpacker;

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
            case ATTACHMENT_REQUEST_ACK:
                AttachmentRequestAckData.packData(packer);
                break;
            case ATTACHMENT_RESPONSE:
                AttachmentResponseData.packData(packer);
                break;
            case ATTACHMENT_RESPONSE_ACK:
                AttachmentResponseData.packData(packer);
                break;
            case EVENT_DOCUMENT:
                EventDocumentData.packData(packer);
                break;
            default:
                throw new IllegalStateException("unknown message data type: " + dataType);
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
            case ATTACHMENT_REQUEST_ACK:
                AttachmentRequestAckData.unpackData(unPacker);
                break;
            case ATTACHMENT_RESPONSE:
                AttachmentResponseData.unpackData(unPacker);
                break;
            case ATTACHMENT_RESPONSE_ACK:
                AttachmentResponseAckData.unpackData(unPacker);
                break;
            case EVENT_DOCUMENT:
                EventDocumentData.unpackData(unPacker);
                break;
            default:
                throw new IllegalStateException("unknown message data type: " + dataType);
        }

        return dataType;
    }
}
