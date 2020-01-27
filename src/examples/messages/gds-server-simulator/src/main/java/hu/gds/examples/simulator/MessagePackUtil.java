/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator;

import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.ImmutableValue;

import java.io.IOException;


public class MessagePackUtil {

    public static Boolean getBoolean(MessageUnpacker unpacker) throws IOException {
        return getBoolean(unpacker, true);
    }

    public static Boolean getBoolean(MessageUnpacker unpacker, boolean allowNull) throws IOException {
        ImmutableValue value = unpacker.unpackValue();
        if (value.isNilValue() && !allowNull) {
            throw new IllegalStateException("Found a 'null' value where it is not allowed!");
        }
        return value.isNilValue() ? null : value.asBooleanValue().getBoolean();
    }

    public static void packBoolean(MessageBufferPacker packer, Boolean value) throws IOException {
        if (value == null) {
            packer.packNil();
        } else {
            packer.packBoolean(value);
        }
    }

    public static Integer getInteger(MessageUnpacker unpacker) throws IOException {
        return getInteger(unpacker, true);
    }

    public static Integer getInteger(MessageUnpacker unpacker, boolean allowNull) throws IOException {
        ImmutableValue value = unpacker.unpackValue();
        if (value.isNilValue() && !allowNull) {
            throw new IllegalStateException("Found a 'null' value where it is not allowed!");
        }
        return value.isNilValue() ? null : value.asIntegerValue().toInt();
    }

    public static void packInteger(MessageBufferPacker packer, Integer value) throws IOException {
        if (value == null) {
            packer.packNil();
        } else {
            packer.packInt(value);
        }
    }

    public static Long getLong(MessageUnpacker unpacker) throws IOException {
        return getLong(unpacker, true);
    }

    public static Long getLong(MessageUnpacker unpacker, boolean allowNull) throws IOException {
        ImmutableValue value = unpacker.unpackValue();
        if (value.isNilValue() && !allowNull) {
            throw new IllegalStateException("Found a 'null' value where it is not allowed!");
        }
        return value.isNilValue() ? null : value.asNumberValue().toLong();
    }

    public static void packLong(MessageBufferPacker packer, Long value) throws IOException {
        if (value == null) {
            packer.packNil();
        } else {
            packer.packLong(value);
        }
    }

    public static String getString(MessageUnpacker unpacker) throws IOException {
        return getString(unpacker, true);
    }

    public static String getString(MessageUnpacker unpacker, boolean allowNull) throws IOException {
        ImmutableValue value = unpacker.unpackValue();
        if (value.isNilValue() && !allowNull) {
            throw new IllegalStateException("Found a 'null' value where it is not allowed!");
        }
        return value.isNilValue() ? null : value.asStringValue().asString();
    }

    public static void packString(MessageBufferPacker packer, String value) throws IOException {
        if (value == null) {
            packer.packNil();
        } else {
            packer.packString(value);
        }
    }
}
