package hu.gds.examples.clientsimulator;

import hu.gds.examples.messages.DataType;

import java.io.IOException;

public abstract class ReceivedMessageHandler {

    boolean isMessageSendingProcessEnd = false;

    abstract void messageReceived(DataType dataType) throws IOException;
}
