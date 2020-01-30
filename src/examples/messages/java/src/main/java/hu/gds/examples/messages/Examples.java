package hu.gds.examples.messages;

import java.io.IOException;

public class Examples {

    public static void main(String[] args) throws IOException {
        byte[] message = packEventDocumentAckMessage();
        unpackMessage(message);
    }

    private static void unpackMessage(byte[] message) throws IOException {
        MessageManager.unpackMessage(message);
    }

    private static byte[] packConnectionMessage() throws IOException {
        return MessageManager.packMessage(DataType.CONNECTION);
    }

    private static byte[] packConnectionAckMessage() throws IOException {
        return MessageManager.packMessage(DataType.CONNECTION_ACK);
    }

    private static byte[] packEventMessage() throws IOException {
        return MessageManager.packMessage(DataType.EVENT);
    }

    private static byte[] packAttachmentRequestMessage() throws IOException {
        return MessageManager.packMessage(DataType.ATTACHMENT_REQUEST);
    }

    private static byte[] packAttachmentRequestAck() throws IOException {
        return MessageManager.packMessage(DataType.ATTACHMENT_REQUEST_ACK);
    }

    private static byte[] packAttachmentResponse() throws IOException {
        return MessageManager.packMessage(DataType.ATTACHMENT_RESPONSE);
    }

    private static byte[] packAttachmentResponseAck() throws IOException {
        return MessageManager.packMessage(DataType.ATTACHMENT_RESPONSE_ACK);
    }

    private static byte[] packEventDocumentMessage() throws IOException {
        return MessageManager.packMessage(DataType.EVENT_DOCUMENT);
    }

    private static byte[] packEventDocumentAckMessage() throws IOException {
        return MessageManager.packMessage(DataType.EVENT_DOCUMENT_ACK);
    }
}
