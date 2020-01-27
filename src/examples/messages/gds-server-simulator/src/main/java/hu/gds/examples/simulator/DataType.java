/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator;

public enum DataType {

    CONNECTION,
    CONNECTION_ACK,
    EVENT,
    EVENT_ACK,
    ATTACHMENT_REQUEST,
    ATTACHMENT_REQUEST_ACK,
    ATTACHMENT_RESPONSE,
    ATTACHMENT_RESPONSE_ACK,
    EVENT_DOCUMENT,
    EVENT_DOCUMENT_ACK,
    QUERY_REQUEST,
    QUERY_REQUEST_ACK,
    NEXT_QUERY_PAGE_REQUEST;

    public int asInteger() {
        return this.ordinal();
    }

    public static DataType fromInteger(int idx) {
        return values()[idx];
    }
}
