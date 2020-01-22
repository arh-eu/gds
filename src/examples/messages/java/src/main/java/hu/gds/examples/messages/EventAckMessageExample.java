package hu.gds.examples.messages;

import org.msgpack.core.MessagePack;
import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.Value;

import java.io.IOException;
import java.util.List;

public class EventAckMessageExample {

    public static void unpackMessage(byte[] message) throws IOException {
        MessageUnpacker unpacker = MessagePack.newDefaultUnpacker(message);

        //Wrapper array
        unpacker.unpackArrayHeader();

        //HEADER
        Utils.unpackHeader(unpacker);

        //DATA
        unpacker.unpackArrayHeader();

        //status
        int status = unpacker.unpackInt();

        //ack type data object
        if(!unpacker.getNextFormat().getValueType().isNilType()) {
            int ackTypeDataArraySize = unpacker.unpackArrayHeader();
            for (int i = 0; i < ackTypeDataArraySize; ++i) {
                unpacker.unpackArrayHeader();

                //local status
                int localStatus = unpacker.unpackInt();

                //notification
                String notification = Utils.unpackString(unpacker);

                //field descriptors
                int fieldDescriptorsArraySize = unpacker.unpackArrayHeader();
                for (int j = 0; j < fieldDescriptorsArraySize; ++j) {
                    unpacker.unpackArrayHeader();

                    //field name
                    String fieldName = unpacker.unpackString();

                    //field type
                    String fieldType = unpacker.unpackString();

                    //mime type
                    String mimeType = unpacker.unpackString();
                }

                //sub results
                int subResultsArraySize = unpacker.unpackArrayHeader();
                for (int k = 0; k < subResultsArraySize; ++k) {
                    //sub result
                    unpacker.unpackArrayHeader();

                    //sub status
                    Integer subStatus = unpacker.unpackInt();

                    //id
                    String id = Utils.unpackString(unpacker);

                    //table name
                    String tableName = Utils.unpackString(unpacker);

                    //created
                    Boolean created = Utils.unPackBoolean(unpacker);

                    //version
                    Long version = Utils.unpackLong(unpacker);

                    //returning record values
                    if(!unpacker.getNextFormat().getValueType().isNilType()) {
                        List<Value> returningRecordValues = unpacker.unpackValue().asArrayValue().list();
                    }
                }
            }
        } else {
            unpacker.unpackNil();
        }

        //exception
        String exception = Utils.unpackString(unpacker);
    }
}
