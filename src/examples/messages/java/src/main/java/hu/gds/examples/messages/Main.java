package hu.gds.examples.messages;

import java.io.IOException;

public class Main {

    public static void main(String[] args) throws IOException {
        //connectionMessageExample();
        //connectionAckMessageExample();
        eventMessageExample();
    }

    private static void connectionMessageExample() throws IOException {
        //pack message
        byte[] message = ConnectionMessageExample.packMessage();
        //unpack message
        ConnectionMessageExample.unpackMessage(message);
    }

    private static void connectionAckMessageExample() throws IOException {
        //pack message
        byte[] message = ConnectionAckMessageExample.packMessage();
        //unpack message
        ConnectionAckMessageExample.unpackMessage(message);
    }

    public static void eventMessageExample() throws IOException {
        //pack message
        byte[] message = EventMessageExample.packMessage();
    }
}
