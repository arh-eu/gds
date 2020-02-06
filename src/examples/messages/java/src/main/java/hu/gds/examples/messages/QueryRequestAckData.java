package hu.gds.examples.messages;

import org.msgpack.core.MessageUnpacker;
import org.msgpack.value.Value;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import static hu.gds.examples.messages.MessagePackUtil.unpackString;

public class QueryRequestAckData {

    public static void unpackData(MessageUnpacker unpacker) throws IOException {
        //DATA
        unpacker.unpackArrayHeader();

        //status
        int status = unpacker.unpackInt();

        //ack type data array
        if (unpacker.getNextFormat().getValueType().isArrayType()) { //SUCCESS ACK
            unpacker.unpackArrayHeader();

            //number of hits
            Long numberOfHits = unpacker.unpackLong();

            //number of filtered hits
            Long numberOfFilteredHits = unpacker.unpackLong();

            //has more page
            boolean hasMorePage = unpacker.unpackBoolean();

            //query context descriptor
            unpacker.unpackArrayHeader();

            //scroll id
            String scrollId = unpacker.unpackString();

            //query
            String query = unpacker.unpackString();

            //delivered number of hits
            long deliveredNumberOfHits = unpacker.unpackLong();

            //query start time
            long queryStartTime = unpacker.unpackLong();

            //consistency type
            String consistencyType = unpacker.unpackString();

            //last bucket id
            String lastBucketId = unpacker.unpackString();

            //gds descriptor
            unpacker.unpackArrayHeader();

            //cluster name
            String clusterName = unpacker.unpackString();

            //gds node
            String gdsNode = unpacker.unpackString();

            //field values
            List<Value> fieldValues = unpacker.unpackValue().asArrayValue().list();

            //partition names
            List<String> partitionNames = new ArrayList<>();
            List<Value> partitionNamesTemp = unpacker.unpackValue().asArrayValue().list();
            for(Value partitionNameValue: partitionNamesTemp) {
                partitionNames.add(partitionNameValue.asStringValue().asString());
            }

            //field descriptors
            int fieldDescriptorsArraySize = unpacker.unpackArrayHeader();
            for(int i = 0; i < fieldDescriptorsArraySize; ++i) {
                unpacker.unpackArrayHeader();

                //field name
                String fieldName = unpacker.unpackString();

                //field type
                String fieldType = unpacker.unpackString();

                //mime type
                String mimeType = unpacker.unpackString();
            }

            //hits
            int hitsArraySize = unpacker.unpackArrayHeader();
            List<List<Value>> hits = new ArrayList<>();
            for(int j = 0; j < hitsArraySize; ++j) {
                hits.add(unpacker.unpackValue().asArrayValue().list());
            }
        } else { //UNSUCCESS ACK
            unpacker.unpackNil();
        }

        //exception
        String exception = unpackString(unpacker);
    }
}
