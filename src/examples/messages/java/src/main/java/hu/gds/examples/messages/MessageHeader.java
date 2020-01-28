package hu.gds.examples.messages;

import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;

import static hu.gds.examples.messages.MessagePackUtil.*;

public class MessageHeader {

    /*
        [
            "user", --> 'user name'
            "793ed37a-a30c-44cb-848b-ad30c1c52358", --> 'message id'
            'System.currentTimeMillis()', --> 'create time'
            'System.currentTimeMillis()', --> 'request time'
            false, --> 'is fragmented'
            null, --> 'first fragment'
            null, --> 'last fragment'
            null, --> 'offset'
            null, --> 'full data size'
            '[0..12]', --> 'data type'
            '...' --> 'data'
        ]
     */
    public static void packHeader(MessageBufferPacker packer, int dataType) throws IOException {
        //user name
        packer.packString("user");

        //message id
        packer.packString("793ed37a-a30c-44cb-848b-ad30c1c52358");

        //create time
        packer.packLong(System.currentTimeMillis());

        //request time
        packer.packLong(System.currentTimeMillis());

        //is fragmented
        packer.packBoolean(false);

        //first fragment
        packer.packNil();

        //last fragment
        packer.packNil();

        //offset
        packer.packNil();

        //full data size
        packer.packNil();

        //data type
        packer.packInt(dataType);
    }

    public static int unpackHeader(MessageUnpacker unpacker) throws IOException {
        //user name
        String userName = unpacker.unpackString();

        //message id
        String messageId = unpacker.unpackString();

        //create time
        Long createTime = unpacker.unpackLong();

        //request time
        Long requestTime = unpacker.unpackLong();

        //is fragmented
        Boolean isFragmented = unpacker.unpackBoolean();

        //first fragment
        Boolean firstFragment = unPackBoolean(unpacker);

        //last fragment
        Boolean lastFragment = unPackBoolean(unpacker);

        //offset
        Integer offset = unpackInteger(unpacker);

        //full data size
        Integer fullDataSize = unpackInteger(unpacker);

        //dataType
        Integer dataType = unpacker.unpackInt();

        return dataType;
    }
}
