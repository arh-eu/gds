package hu.gds.examples.messages;

public enum DataType {
    CONNECTION(0),
    CONNECTION_ACK(1),
    EVENT(2),
    EVENT_ACK(3),
    ATTACHMENT_REQUEST(4),
    ATTACHMENT_REQUEST_ACK(5),
    ATTACHMENT_RESPONSE(6),
    ATTACHMENT_RESPONSE_ACK(7),
    EVENT_DOCUMENT(8),
    EVENT_DOCUMENT_ACK(9),
    QUERY_REQUEST(10),
    QUERY_REQUEST_ACK(11),
    NEXT_QUERY_PAGE_REQUEST(12);

    private int value;

    DataType(int value) {
        this.value = value;
    }

    public int getValue() {
        return this.value;
    }
}
