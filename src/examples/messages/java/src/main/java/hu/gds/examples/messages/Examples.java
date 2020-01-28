package hu.gds.examples.messages;

import java.io.IOException;

public class Examples {

    public static void main(String[] args) throws IOException {
        byte[] message = packConnectionMessage();
        unpackConnectionMessage(message);
    }

    private static byte[] packConnectionMessage() throws IOException {
        return MessageManager.packMessage(DataType.CONNECTION);
    }

    private static void unpackConnectionMessage(byte[] message) throws IOException {
        MessageManager.unpackMessage(message);
    }

    private static byte[] packConnectionAckMessage() throws IOException {
        return MessageManager.packMessage(DataType.CONNECTION_ACK);
    }

    private static void unpackConnectionAckMessage(byte[] message) throws IOException {
        MessageManager.unpackMessage(message);
    }
}
