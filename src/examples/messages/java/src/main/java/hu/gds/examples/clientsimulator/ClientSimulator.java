package hu.gds.examples.clientsimulator;

import hu.gds.examples.clientsimulator.websocket.ResponseHandler;
import hu.gds.examples.clientsimulator.websocket.WebSocketClient;
import hu.gds.examples.messages.DataType;
import hu.gds.examples.messages.MessageManager;

import java.util.logging.Logger;

public class ClientSimulator {
    private WebSocketClient webSocketClient;
    private ResponseHandler responseHandler;
    private ReceivedMessageHandler receivedMessageHandler;

    public static final Logger logger = Logger.getLogger("logging");

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
        } catch (Throwable throwable) {
            logger.severe("An error occured while creating client simulator: " + throwable.getMessage());
        }
    }

    public ClientSimulator(String url, ReceivedMessageHandler receivedMessageHandler) {
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
        this.receivedMessageHandler = receivedMessageHandler;
        try {
            this.webSocketClient = new WebSocketClient(url, responseHandler);
        } catch (Throwable throwable) {
            logger.severe("An error occured while creating client simulator: " + throwable.getMessage());
        }
    }

    public void connect() {
        try {
            webSocketClient.connect();
        } catch (Throwable throwable) {
            logger.severe("An error occured while connecting to server: " + throwable.getMessage());
        }
    }

    public void close() {
        try {
            webSocketClient.close();
        } catch (Throwable throwable) {
            throwable.printStackTrace();
            logger.severe("An error occured while closing connection: " + throwable.getMessage());
        }
    }

    public void sendMessage(byte[] message, DataType dataType) {
        try {
            logger.info("WebSocket Client sending '" + dataType + "' message");
            webSocketClient.send(message);
        } catch (Throwable throwable) {
            logger.severe("An error occured while sending message: " + throwable.getMessage());
        }
    }

    public void handleResponse(byte[] message) {
        try {
            DataType dataType = MessageManager.unpackMessage(message);
            logger.info("WebSocket Client received '" + dataType + "' message");
            if(receivedMessageHandler != null) {
                receivedMessageHandler.messageReceived(dataType);
            }
        } catch (Throwable throwable) {
            throwable.printStackTrace();
            logger.severe("An error occured while handling response message: " + throwable.getMessage());
        }
    }

    public void handleResponse(String message) {
        logger.info("Websocket Client received message: " + message);
    }
}