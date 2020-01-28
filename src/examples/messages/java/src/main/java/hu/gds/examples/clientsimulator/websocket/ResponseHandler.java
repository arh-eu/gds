package hu.gds.examples.clientsimulator.websocket;

import java.io.IOException;

public interface ResponseHandler {
    void handleResponse(byte[] message) throws IOException;
    void handleResponse(String message);
}
