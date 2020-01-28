package hu.gds.examples.clientsimulator;

import hu.gds.examples.messages.ConnectionMessageExample;

import javax.net.ssl.SSLException;
import java.io.IOException;
import java.net.URISyntaxException;

public class Test {

    public static void main(String args[]) throws IOException {
        ClientSimulator simulator = new ClientSimulator("ws://127.0.0.1:8080/websocket");
        //ClientSimulator simulator = new ClientSimulator("wss://echo.websocket.org");
        simulator.connect();
        simulator.sendMessage(ConnectionMessageExample.packMessage());


    }
}
