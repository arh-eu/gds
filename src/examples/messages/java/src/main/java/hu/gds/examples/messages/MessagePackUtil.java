package hu.gds.examples.messages;

import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.ImmutableValue;

import java.io.IOException;

public class MessagePackUtil {

    public static String unpackString(MessageUnpacker unpacker) throws IOException {
        ImmutableValue value = unpacker.unpackValue();
        if (value.isNilValue()) {
            return null;
        } else {
            return value.asStringValue().asString();
        }
    }

    public static Long unpackLong(MessageUnpacker unpacker) throws IOException {
        ImmutableValue value = unpacker.unpackValue();
        if (value.isNilValue()) {
            return null;
        } else {
            return value.asNumberValue().toLong();
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
