package hu.gds.examples.clientsimulator;

import hu.gds.examples.messages.MessageManager;

import java.io.IOException;

public class Main {

    public static void main(String args[]) throws IOException {
        //ClientSimulator simulator = new ClientSimulator("wss://echo.websocket.org");
        ClientSimulator simulator = new ClientSimulator("ws://127.0.0.1:8080/websocket");
        simulator.connect();
        simulator.sendMessage(MessageManager.packMessage(0));
    }
}
