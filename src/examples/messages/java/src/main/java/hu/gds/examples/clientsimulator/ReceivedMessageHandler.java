package hu.gds.examples.clientsimulator;

import hu.gds.examples.messages.DataType;

import java.io.IOException;

public interface ReceivedMessageHandler {
    void messageReceived(DataType dataType) throws IOException;
}
