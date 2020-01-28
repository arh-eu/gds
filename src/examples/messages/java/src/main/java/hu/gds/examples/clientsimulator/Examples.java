package hu.gds.examples.clientsimulator;

import hu.gds.examples.messages.DataType;
import hu.gds.examples.messages.MessageManager;

import java.io.IOException;

public class Examples {

    public static void main(String args[]) throws IOException {
        ClientSimulator simulator = new ClientSimulator("ws://127.0.0.1:8080/websocket");
        simulator.connect();

        sendConnectionMessage(simulator);
    }

    private static void sendConnectionMessage(ClientSimulator simulator) throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.CONNECTION));
    }

    public static void sendConnectionAckMessage(ClientSimulator simulator) throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.CONNECTION_ACK));
    }
}
