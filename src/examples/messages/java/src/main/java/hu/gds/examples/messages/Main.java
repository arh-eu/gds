package hu.gds.examples.messages;

import java.io.IOException;

public class Main {

    public static void main(String[] args) throws IOException {

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

    public static byte[] eventMessagePackExample() throws IOException {
        return EventMessageExample.packMessage();
    }

    public static void eventAckMessageUnpackExample(byte[] message) throws IOException {
        EventAckMessageExample.unpackMessage(message);
    }
}
