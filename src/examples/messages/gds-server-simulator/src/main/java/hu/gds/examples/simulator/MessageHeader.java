/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator;

import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;


public class MessageHeader implements Packable {
    private final String username;
    private final String messageID;
    private final long creationTime;
    private final long requestTime;
    private final boolean isFragmented;
    private final Boolean firstFragment;
    private final Boolean lastFragment;
    private final Integer offset;
    private final Integer fullDataSize;
    private final DataType dataType;

    public MessageHeader(String username, String messageID, long creationTime, long requestTime, boolean isFragmented,
                         Boolean firstFragment, Boolean lastFragment, Integer offset, Integer fullDataSize, DataType dataType) {
        this.username = username;
        this.messageID = messageID;
        this.creationTime = creationTime;
        this.requestTime = requestTime;
        this.isFragmented = isFragmented;
        this.firstFragment = firstFragment;
        this.lastFragment = lastFragment;
        this.offset = offset;
        this.fullDataSize = fullDataSize;
        this.dataType = dataType;
    }

    public MessageHeader(String username, String messageID, long creationTime, long requestTime, boolean isFragmented,
                         Boolean firstFragment, Boolean lastFragment, Integer offset, Integer fullDataSize, int dataType) {
        this(username, messageID, creationTime, requestTime, isFragmented,
                firstFragment, lastFragment, offset, fullDataSize, DataType.fromInteger(dataType));
    }

    @Override
    public void pack(MessageBufferPacker packer) throws IOException {
        MessagePackUtil.packString(packer, username);
        MessagePackUtil.packString(packer, messageID);
        MessagePackUtil.packLong(packer, creationTime);
        MessagePackUtil.packLong(packer, requestTime);
        MessagePackUtil.packBoolean(packer, isFragmented);
        MessagePackUtil.packBoolean(packer, firstFragment);
        MessagePackUtil.packBoolean(packer, lastFragment);
        MessagePackUtil.packInteger(packer, offset);
        MessagePackUtil.packInteger(packer, fullDataSize);
        MessagePackUtil.packInteger(packer, dataType.asInteger());
    }


    public static MessageHeader unpack(MessageUnpacker unpacker) {
        try {
            String userName = unpacker.unpackString();
            String messageId = unpacker.unpackString();
            long createTime = unpacker.unpackLong();
            long requestTime = unpacker.unpackLong();
            boolean isFragmented = unpacker.unpackBoolean();

            Boolean firstFragment = MessagePackUtil.getBoolean(unpacker);
            Boolean lastFragment = MessagePackUtil.getBoolean(unpacker);
            Integer offset = MessagePackUtil.getInteger(unpacker);
            Integer fullDataSize = MessagePackUtil.getInteger(unpacker);

            int dataType = unpacker.unpackInt();

            return new MessageHeader(userName, messageId, createTime, requestTime, isFragmented,
                    firstFragment, lastFragment, offset, fullDataSize, dataType);

        } catch (Throwable t) {
            throw new IllegalStateException("The format of the header is not valid!", t);
        }
    }

    public static MessageHeader create(String username, DataType type) {
        long now = System.currentTimeMillis();
        return new MessageHeader(username, "request-" + now, now, now,
                false, null, null, null, null, type);
    }

    public String getUsername() {
        return username;
    }

    public String getMessageID() {
        return messageID;
    }

    public long getCreationTime() {
        return creationTime;
    }

    public long getRequestTime() {
        return requestTime;
    }

    public boolean isFragmented() {
        return isFragmented;
    }

    public Boolean getFirstFragment() {
        return firstFragment;
    }

    public Boolean getLastFragment() {
        return lastFragment;
    }

    public Integer getOffset() {
        return offset;
    }

    public Integer getFullDataSize() {
        return fullDataSize;
    }

    public DataType getDataType() {
        return dataType;
    }
}
