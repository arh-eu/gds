package hu.gds.examples.messages;

import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.Value;

import java.io.IOException;
import java.util.List;

import static hu.gds.examples.messages.MessagePackUtil.*;

/*
        GDS --> Client
 */
public class EventAckData {

    public static void unpackData(MessageUnpacker unpacker) throws IOException {
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
                String notification = unpackString(unpacker);

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
                    String id = unpackString(unpacker);

                    //table name
                    String tableName = unpackString(unpacker);

                    //created
                    Boolean created = unPackBoolean(unpacker);

                    //version
                    Long version = unpackLong(unpacker);

                    //returning record values
                    if(!unpacker.getNextFormat().getValueType().isNilType()) {
                        List<Value> returningRecordValues = unpacker.unpackValue().asArrayValue().list();
                    } else {
                        unpacker.unpackNil();
                    }
                }
            }
        } else {
            unpacker.unpackNil();
        }

        //exception
        String exception = unpackString(unpacker);
    }
}
