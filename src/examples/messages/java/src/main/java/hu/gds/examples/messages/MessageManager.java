package hu.gds.examples.messages;

import org.msgpack.core.MessageBufferPacker;
import org.msgpack.core.MessagePack;
import org.msgpack.core.MessageUnpacker;

import java.io.IOException;

public class MessageManager {

    public static byte[] packMessage(int dataType) throws IOException {
        MessageBufferPacker packer = MessagePack.newDefaultBufferPacker();

        //Wrapper array
        packer.packArrayHeader(11);

        //HEADER
        MessageHeader.packHeader(packer, dataType);

        switch (dataType) {
            case 0:
                ConnectionData.packData(packer);
                break;
            case 1:
                ConnectionAckData.packData(packer);
                break;
            default:
                //TODO: hiba
                break;
        }

        return packer.toByteArray();
    }

    public static int unpackMessage(byte[] message) throws IOException {
        MessageUnpacker unPacker = MessagePack.newDefaultUnpacker(message);

        //Wrapper array
        unPacker.unpackArrayHeader();

        //HEADER
        int dataType = MessageHeader.unpackHeader(unPacker);

        switch (dataType) {
            case 0:
                ConnectionData.unpackData(unPacker);
                break;
            case 1:
                ConnectionAckData.unpackData(unPacker);
                break;
            default:
                //TODO: hiba
                break;
        }

        return dataType;
    }
}
