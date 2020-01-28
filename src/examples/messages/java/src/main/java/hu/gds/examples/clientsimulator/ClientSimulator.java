package hu.gds.examples.clientsimulator;

import hu.gds.examples.clientsimulator.websocket.ResponseHandler;
import hu.gds.examples.clientsimulator.websocket.WebSocketClient;

import javax.net.ssl.SSLException;
import java.net.URISyntaxException;

public class ClientSimulator {

    private WebSocketClient webSocketClient;
    private ResponseHandler responseHandler;

    public ClientSimulator(String url) {
        this.responseHandler = new ResponseHandler() {
            @Override
            public void handleResponse(byte[] message) {
                ClientSimulator.this.handleResponse(message);
            }

            @Override
            public void handleResponse(String message) {
                ClientSimulator.this.handleResponse(message);
            }
        };
        try {
            this.webSocketClient = new WebSocketClient(url, responseHandler);
        } catch (SSLException | URISyntaxException e) {
            e.printStackTrace();
        }
    }

    public void connect() {
        try {
            webSocketClient.connect();
        } catch (Throwable throwable) {
            throwable.printStackTrace();
        }
    }

    public void close() {
        try {
            webSocketClient.close();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public void sendMessage(byte[] message) {
        try {
            webSocketClient.send(message);
        } catch (Throwable throwable) {
            throwable.printStackTrace();
        }
    }

    public void handleResponse(byte[] message) {
        System.out.println(new String(message));
    }

    public void handleResponse(String message) {
        System.out.println(message);
    }
}