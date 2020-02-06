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
            case EVENT_DOCUMENT_ACK:
                EventDocumentAckData.packData(packer);
                break;
            case QUERY_REQUEST:
                QueryRequestData.packData(packer);
                break;
            case NEXT_QUERY_PAGE_REQUEST:
                NextQueryPageData.packData(packer);
                break;
            default:
                throw new IllegalStateException("unknown message data type: " + dataType);
        }

        return packer.toByteArray();
    }

    public static DataType unpackMessage(byte[] message) throws IOException {
        MessageUnpacker unpacker = MessagePack.newDefaultUnpacker(message);

        //Wrapper array
        unpacker.unpackArrayHeader();

        //HEADER
        DataType dataType = MessageHeader.unpackHeader(unpacker);

        switch (dataType) {
            case CONNECTION:
                ConnectionData.unpackData(unpacker);
                break;
            case CONNECTION_ACK:
                ConnectionAckData.unpackData(unpacker);
                break;
            case EVENT_ACK:
                EventAckData.unpackData(unpacker);
                break;
            case ATTACHMENT_REQUEST:
                AttachmentRequestData.unpackData(unpacker);
                break;
            case ATTACHMENT_REQUEST_ACK:
                AttachmentRequestAckData.unpackData(unpacker);
                break;
            case ATTACHMENT_RESPONSE:
                AttachmentResponseData.unpackData(unpacker);
                break;
            case ATTACHMENT_RESPONSE_ACK:
                AttachmentResponseAckData.unpackData(unpacker);
                break;
            case EVENT_DOCUMENT:
                EventDocumentData.unpackData(unpacker);
                break;
            case EVENT_DOCUMENT_ACK:
                EventDocumentAckData.unpackData(unpacker);
                break;
            case QUERY_REQUEST:
                QueryRequestData.unpackData(unpacker);
                break;
            case QUERY_REQUEST_ACK:
                QueryRequestAckData.unpackData(unpacker);
                break;
            default:
                throw new IllegalStateException("unknown message data type: " + dataType);
        }

        return dataType;
    }
}
