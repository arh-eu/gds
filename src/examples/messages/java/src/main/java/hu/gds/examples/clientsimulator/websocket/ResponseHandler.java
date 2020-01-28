package hu.gds.examples.clientsimulator.websocket;

public interface ResponseHandler {
    void handleResponse(byte[] message);
    void handleResponse(String message);
}
