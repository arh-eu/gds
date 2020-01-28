package hu.gds.examples.clientsimulator;

import hu.gds.examples.clientsimulator.websocket.ResponseHandler;
import hu.gds.examples.clientsimulator.websocket.WebSocketClient;
import hu.gds.examples.messages.DataType;
import hu.gds.examples.messages.MessageManager;

import javax.net.ssl.SSLException;
import java.io.IOException;
import java.net.URISyntaxException;
import java.util.logging.Logger;

public class ClientSimulator {

    private WebSocketClient webSocketClient;
    private ResponseHandler responseHandler;

    public static final Logger logger = Logger.getLogger("logging");

    public ClientSimulator(String url) {
        this.responseHandler = new ResponseHandler() {
            @Override
            public void handleResponse(byte[] message) throws IOException {
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

    public void handleResponse(byte[] message) throws IOException {
        int dataType = MessageManager.unpackMessage(message);
        logger.info("WebSocket Client received '" + DataType.findByKey(dataType) + "' message");
    }

    public void handleResponse(String message) {
        logger.info("Websocket Client received message: " + message);
    }
}