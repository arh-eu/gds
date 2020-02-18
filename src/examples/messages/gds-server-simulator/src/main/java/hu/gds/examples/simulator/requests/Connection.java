/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gds-server-simulator project.
 * Budapest, 2020/01/27
 */

package hu.gds.examples.simulator.requests;

import hu.gds.examples.simulator.MessagePackUtil;
import hu.gds.examples.simulator.Packable;
import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;

public class Connection implements Packable {

    private final boolean sotsc;
    private final int protocol;
    private final boolean fragSupp;
    private final Integer fragTU;
    private final String[] reserved;


    @SuppressWarnings("ConstantConditions") //value unboxing is handled in the MessagePackUtil class.
    public Connection(MessageUnpacker unpacker) throws IOException {

        /*int arraySize = unpacker.unpackArrayHeader();
        if (arraySize < 5 || arraySize > 6) {
            throw new IllegalStateException("The Body of Connection be an array of size 5 or 6, found "
                    + arraySize + " elements instead!");
        }


        if (arraySize == 6) {
            clusterName = MessagePackUtil.getString(unpacker);
        } else {
            clusterName = null;
        }

        sotsc = MessagePackUtil.getBoolean(unpacker, false);
        protocol = MessagePackUtil.getInteger(unpacker, false);
        fragSupp = MessagePackUtil.getBoolean(unpacker, false);
        fragTU = MessagePackUtil.getInteger(unpacker);

        int reservedSize = unpacker.unpackArrayHeader();
        reserved = new String[reservedSize];

        for (int i = 0; i < reserved.length; ++i) {
            reserved[i] = MessagePackUtil.getString(unpacker);
        }*/

        //DATA
        int arraySize = unpacker.unpackArrayHeader();
        if (arraySize < 4 || arraySize > 5) {
            throw new IllegalStateException("The Body of Connection be an array of size 4 or 5, found "
                    + arraySize + " elements instead!");
        }

        //serve on the same connection
        sotsc = unpacker.unpackBoolean();

        //protocol version number
        protocol = unpacker.unpackInt();

        //fragmentation supported
        fragSupp = unpacker.unpackBoolean();

        //fragmentation transmission unit
        fragTU = MessagePackUtil.getInteger(unpacker);

        if (unpacker.hasNext()) {
            if (!unpacker.getNextFormat().getValueType().isNilType()) {
                //reserved fields
                unpacker.unpackArrayHeader();
                //password
                String password = MessagePackUtil.getString(unpacker);
                reserved = new String[]{password};
            } else {
                reserved = null;
            }
        } else {
            reserved = null;
        }
    }


    @Override
    public void pack(MessageBufferPacker packer) throws IOException {

        packer.packArrayHeader(null == reserved ? 4 : 5);

        packer.packBoolean(sotsc);
        packer.packInt(protocol);
        packer.packBoolean(fragSupp);
        if (fragTU == null) {
            packer.packNil();
        } else {
            packer.packInt(fragTU);
        }

        if(reserved != null) {
            packer.packArrayHeader(reserved.length);
            for (String s : reserved) {
                MessagePackUtil.packString(packer, s);
            }
        }
    }
}
