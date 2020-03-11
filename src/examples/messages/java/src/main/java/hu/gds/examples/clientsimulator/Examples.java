package hu.gds.examples.clientsimulator;

import hu.gds.examples.messages.DataType;
import hu.gds.examples.messages.MessageManager;

import java.io.IOException;

public class Examples {

    private static ClientSimulator simulator;

    public static void main(String args[]) throws IOException {
        /*simulator = new ClientSimulator("ws://127.0.0.1:8080/websocket", new ReceivedMessageHandler() {
            @Override
            public void messageReceived(DataType dataType) throws IOException {
                switch (dataType) {
                    case CONNECTION_ACK:
                        sendEventMessage();
                        break;
                    case EVENT_ACK:
                        sendAttachmentRequestMessage();
                        break;
                    case ATTACHMENT_REQUEST_ACK:
                        sendAttachmentResponseMessage();
                        break;
                    case ATTACHMENT_RESPONSE_ACK:
                        sendEventDocumentMessage();
                        break;
                    case EVENT_DOCUMENT_ACK:
                        sendQueryRequestMessage();
                        break;
                    case QUERY_REQUEST_ACK:
                        if (!isMessageSendingProcessEnd) {
                            sendNextQueryPageMessage();

                            //sendAttachmentRequestAckMessage();
                            //sendAttachmentResponseAckMessage();
                            isMessageSendingProcessEnd = true;
                        } else {
                            simulator.close();
                        }
                        break;
                }
            }
        });*/

        simulator = new ClientSimulator("ws://127.0.0.1:8080/websocket", new ReceivedMessageHandler() {
            @Override
            public void messageReceived(DataType dataType) throws IOException {

            }
        });

        simulator.connect();
        sendConnectionMessage();

    }

    private static void sendConnectionMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.CONNECTION), DataType.CONNECTION);
    }

    public static void sendConnectionAckMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.CONNECTION_ACK), DataType.CONNECTION_ACK);
    }

    private static void sendEventMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.EVENT), DataType.EVENT);
    }

    private static void sendAttachmentRequestMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.ATTACHMENT_REQUEST), DataType.ATTACHMENT_REQUEST);
    }

    private static void sendAttachmentRequestAckMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.ATTACHMENT_REQUEST_ACK), DataType.ATTACHMENT_REQUEST_ACK);
    }

    private static void sendAttachmentResponseMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.ATTACHMENT_RESPONSE), DataType.ATTACHMENT_RESPONSE);
    }

    private static void sendAttachmentResponseAckMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.ATTACHMENT_RESPONSE_ACK), DataType.ATTACHMENT_RESPONSE_ACK);
    }

    private static void sendEventDocumentMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.EVENT_DOCUMENT), DataType.EVENT_DOCUMENT);
    }

    private static void sendEventDocumentAckMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.EVENT_DOCUMENT_ACK), DataType.EVENT_DOCUMENT_ACK);
    }

    private static void sendQueryRequestMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.QUERY_REQUEST), DataType.QUERY_REQUEST);
    }

    private static void sendNextQueryPageMessage() throws IOException {
        simulator.sendMessage(MessageManager.packMessage(DataType.NEXT_QUERY_PAGE_REQUEST), DataType.NEXT_QUERY_PAGE_REQUEST);
    }
}
