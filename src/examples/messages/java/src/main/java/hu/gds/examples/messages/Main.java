package hu.gds.examples.messages;

import java.io.IOException;

public class Main {

    public static void main(String[] args) throws IOException {
        byte[] message = attachmentResponseMessagePackExample();
        attachmentResponseMessageUnpackExample(message);
    }

    private static byte[] connectionMessagePackExample() throws IOException {
        return ConnectionMessageExample.packMessage();
    }

    private static void connectionMessageUnpackExample(byte[] message) throws IOException {
        ConnectionMessageExample.unpackMessage(message);
    }

    private static byte[] connectionAckMessagePackExample() throws IOException {
        return ConnectionAckMessageExample.packMessage();
    }

    private static void connectionAckMessageUnpackExample(byte[] message) throws IOException {
        ConnectionAckMessageExample.unpackMessage(message);
    }

    private static byte[] eventMessagePackExample() throws IOException {
        return EventMessageExample.packMessage();
    }

    private static void eventAckMessageUnpackExample(byte[] message) throws IOException {
        EventAckMessageExample.unpackMessage(message);
    }

    private static byte[] attachmentRequestMessagePackExample() throws IOException {
        return AttachmentRequestMessageExample.packMessage();
    }

    private static void attachmentRequestMessageUnpackExample(byte[] message) throws IOException {
        AttachmentRequestMessageExample.unpackMessage(message);
    }

    private static byte[] attachmentRequestAckMessagePackExample() throws IOException {
        return AttachmentRequestAckMessageExample.packMessage();
    }

    private static void attachmentRequestAckMessageUnpackExample(byte[] message) throws IOException {
        AttachmentRequestAckMessageExample.unpackMessage(message);
    }

    private static byte[] attachmentResponseMessagePackExample() throws IOException {
        return AttachmentResponseMessageExample.packMessage();
    }

    private static void attachmentResponseMessageUnpackExample(byte[] message) throws IOException {
        AttachmentResponseMessageExample.unpackMessage(message);
    }
}
