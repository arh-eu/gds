package hu.gds.examples.messages;

import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.ImmutableValue;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;

public class Utils {

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

    public static void unpackHeader(MessageUnpacker unpacker) throws IOException {
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
    }

    public static byte[] binaryFromFile(String file) throws IOException {
        return Files.readAllBytes(new File(file).toPath());
    }

    public static void binaryToFile(String file, byte[] binary) throws IOException {
        try (FileOutputStream fos = new FileOutputStream(file)) {
            fos.write(binary);
        }
    }

    public static String unpackString(MessageUnpacker unpacker) throws IOException {
        ImmutableValue value = unpacker.unpackValue();
        if (value.isNilValue()) {
            return null;
        } else {
            return value.asStringValue().asString();
        }
    }

    public static Integer unpackInteger(MessageUnpacker unpacker) throws IOException {
        ImmutableValue value = unpacker.unpackValue();
        if (value.isNilValue()) {
            return null;
        } else {
            return value.asIntegerValue().asInt();
        }
    }

    public static Boolean unPackBoolean(MessageUnpacker unpacker) throws IOException {
        ImmutableValue value = unpacker.unpackValue();
        if (value.isNilValue()) {
            return null;
        } else {
            return value.asBooleanValue().getBoolean();
        }
    }
}
