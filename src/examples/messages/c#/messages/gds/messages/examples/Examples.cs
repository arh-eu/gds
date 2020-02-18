using gds.messages;
using gds.messages.data;
using gds.messages.header;
using messages_api.gds.websocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace gds.messages.examples
{
    public class Examples
    {
        private readonly static string URI = "ws://127.0.0.1:8080/websocket";

        static void Main(string[] args)
        {
            //GDSWebSocketClient client = new GDSWebSocketClient(URI);
            /*
            byte[] connection = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.Connection), GetExampleConnectionData()));
            byte[] connectionAck = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.ConnectionAck), GetExampleConnectionAckData()));
            byte[] eventM = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.Event), GetExampleEventData()));
            byte[] attachmentRequest = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.AttachmentRequest), GetExampleAttachmentRequestData()));
            byte[] attachmentRequestAck = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.AttachmentRequestAck), GetExampleAttachmentRequestAckData()));
            byte[] attachmentResponse = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.AttachmentResponse), GetExampleAttachmentResponseData()));
            byte[] attachmentResponseAck = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.AttachmentResponseAck), GetExampleAttachmentResponseAckData()));
            byte[] eventDocument = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.EventDocument), GetExampleEventDocumentData()));
            byte[] eventDocumentAck = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.EventDocumentAck), GetExampleEventDocumentAckData()));
            byte[] queryRequest = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.QueryRequest), GetExampleQueryRequestData()));
            //byte[] queryRequestAck = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.QueryRequestAck), GetExampleQueryRequestAckData()));
            byte[] nextQueryRequestPage = MessageManager.GetBinaryFromMessage(MessageManager.GetMessage(GetExampleMessageHeader(DataType.NextQueryPageRequest), GetExampleNextQueryPageRequestData()));

            File.WriteAllBytes("connection", connection);
            File.WriteAllBytes("connectionAck", connectionAck);
            File.WriteAllBytes("eventM", eventM);
            File.WriteAllBytes("attachmentRequest", attachmentRequest);
            File.WriteAllBytes("attachmentRequestAck", attachmentRequestAck);
            File.WriteAllBytes("attachmentResponse", attachmentResponse);
            File.WriteAllBytes("attachmentResponseAck", attachmentResponseAck);
            File.WriteAllBytes("eventDocument", eventDocument);
            File.WriteAllBytes("eventDocumentAck", eventDocumentAck);
            File.WriteAllBytes("queryRequest", queryRequest);
            //File.WriteAllBytes("queryRequestAck", queryRequestAck);
            File.WriteAllBytes("nextQueryRequestPage", nextQueryRequestPage);
            */

        }

        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        public static Header GetExampleMessageHeader(DataType dataType)
        {
            return MessageManager.GetHeader("user", "870da92f-7fff-48af-825e-05351ef97acd", CurrentTimeMillis(), CurrentTimeMillis(), false, null, null, null, null, dataType);
        }

        public static Connection GetExampleConnectionData()
        {
            return MessageManager.GetConnectionData(false, 1, false, null, "pass");
        }

        public static ConnectionAck GetExampleConnectionAckData()
        {
            return MessageManager.GetConnectionAckData(StatusCode.OK, GetExampleConnectionData(), null);
        }

        public static Event GetExampleEventData()
        {
            string operationsStringBlock = "INSERT INTO events (id, some_field, images) VALUES('EVNT202000000000000000', 'some_text', array('ATID202000000000000000));" +
                "INSERT INTO events - @attachment(id, meta, image) VALUES('ATID201811071434257890', 'some_meta', 0x62696e6172795f6964315f6578616d706c65)";

            Dictionary<string, byte[]> binaryContentsMapping = new Dictionary<string, byte[]>();
            binaryContentsMapping.Add("62696e6172795f69645f6578616d706c65", new byte[] { 1, 2, 3 });

            List<List<Dictionary<int, bool>>> executionPrioryStructure = new List<List<Dictionary<int, bool>>>();
            List<Dictionary<int, bool>> priorityLevels = new List<Dictionary<int, bool>>();
            Dictionary<int, bool> priorityLevel1 = new Dictionary<int, bool>();
            priorityLevel1.Add(1, true);
            Dictionary<int, bool> priorityLevel2 = new Dictionary<int, bool>();
            priorityLevel2.Add(1, true);
            priorityLevels.Add(priorityLevel1);
            priorityLevels.Add(priorityLevel2);
            executionPrioryStructure.Add(priorityLevels);

            return MessageManager.GetEventData(operationsStringBlock, binaryContentsMapping, executionPrioryStructure);
        }

        public static AttachmentRequest GetExampleAttachmentRequestData()
        {
            return MessageManager.GetAttachmentRequestData("SELECT meta, data, \"@to_valid\" FROM \"events - @attachment\" " +
                "WHERE id = ’ATID202000000000000000’ and ownerid = ’EVNT202000000000000000’ FOR UPDATE WAIT 86400");
        }

        public static AttachmentRequestAck GetExampleAttachmentRequestAckData()
        {
            AttachmentResult result = new AttachmentResult(new List<string> { "870da92f-7fff-48af-825e-05351ef97acd" }, "events", 
                "ATID202000000000000000", new List<string> { "EVNT202000000000000000" }, "some_meta", 86_400_000, null, new byte[] { 1, 2, 3 });
            AttachmentRequestAckTypeData ackData = new AttachmentRequestAckTypeData(StatusCode.Created, result, null);
            return MessageManager.GetAttachmentRequestAckData(StatusCode.OK, ackData, null);
        }

        public static AttachmentResponse GetExampleAttachmentResponseData()
        {
            AttachmentResult result = new AttachmentResult(new List<string> { "870da92f-7fff-48af-825e-05351ef97acd" }, "events",
                "ATID202000000000000000", new List<string> { "EVNT202000000000000000" }, "some_meta", 86_400_000, null, new byte[] { 1, 2, 3 });
            return MessageManager.GetAttachmentResponseData(result);
        }

        public static AttachmentResponseAck GetExampleAttachmentResponseAckData()
        {
            AttachmentResponseAckResult result = new AttachmentResponseAckResult(new List<string> { "870da92f-7fff-48af-825e-05351ef97acd" }, "events", "ATID202000000000000000");
            AttachmentResponseAckTypeData ackData = new AttachmentResponseAckTypeData(StatusCode.Created, result);
            return MessageManager.GetAttachmentResponseAckData(StatusCode.OK, ackData, null);
        }

        public static EventDocument GetExampleEventDocumentData()
        {
            List<FieldDescriptor> fieldDescriptors = new List<FieldDescriptor>();
            fieldDescriptors.Add(new FieldDescriptor("id", "KEYWORD", ""));
            fieldDescriptors.Add(new FieldDescriptor("some_field", "TEXT", ""));
            fieldDescriptors.Add(new FieldDescriptor("images", "TEXT_ARRAY", "image/jpeg"));

            List<List<object>> records = new List<List<object>>();
            List<object> record = new List<object>();
            record.Add("EVNT202000000000000000");
            record.Add("some_text");
            record.Add(new string[] { "ATID202000000000000000" });
            records.Add(record);

            return MessageManager.GetEventDocumentData("events", fieldDescriptors, records);
        }

        public static EventDocumentAck GetExampleEventDocumentAckData()
        {
            List<EventDocumentAckResult> ackData = new List<EventDocumentAckResult>();
            ackData.Add(new EventDocumentAckResult(StatusCode.Created, null));
            return MessageManager.GetEventDocumentAckData(StatusCode.OK, ackData, null);
        }

        public static QueryRequest GetExampleQueryRequestData()
        {
            return MessageManager.GetQueryRequest("SELECT * FROM events", "NONE", 10_000, 0, 0);
        }

        public static QueryRequestAck GetExampleQueryRequestAckData()
        {
            QueryContextDescriptor queryContextDescriptor = new QueryContextDescriptor("074f7219-5903-44b6-8806-1a106b01704b", "SELECT * FROM events", 0, CurrentTimeMillis(),
                "NONE", "63c2c394-a243-4f2b-af78-95cb31d8bf96", new GDSDescriptor("cluster_name", "node_name"), new List<object>() { "EVNT202000000000000000", "some_text",
                    new string[] { "ATID202000000000000000" } }, new List<string>());

            List<FieldDescriptor> fieldDescriptors = new List<FieldDescriptor>();
            fieldDescriptors.Add(new FieldDescriptor("id", "KEYWORD", ""));
            fieldDescriptors.Add(new FieldDescriptor("some_field", "TEXT", ""));
            fieldDescriptors.Add(new FieldDescriptor("images", "TEXT_ARRAY", "image/jpeg"));

            List<List<object>> records = new List<List<object>>();
            List<object> record = new List<object>();
            record.Add("EVNT202000000000000000");
            record.Add("some_text");
            record.Add(new string[] { "ATID202000000000000000" });
            records.Add(record);

            QueryRequestAckTypeData ackData = new QueryRequestAckTypeData(1, 0, false, queryContextDescriptor, fieldDescriptors, records);

            return MessageManager.GetQueryRequestAck(StatusCode.OK, ackData, null);
        }

        public static NextQueryPageRequest GetExampleNextQueryPageRequestData()
        {
            QueryContextDescriptor queryContextDescriptor = new QueryContextDescriptor("074f7219-5903-44b6-8806-1a106b01704b", "SELECT * FROM events", 0, CurrentTimeMillis(),
                "NONE", "63c2c394-a243-4f2b-af78-95cb31d8bf96", new GDSDescriptor("cluster_name", "node_name"), new List<object>() { "EVNT202000000000000000", "some_text",
                    new string[] { "ATID202000000000000000" } }, new List<string>());

            return MessageManager.GetNextQueryPageRequest(queryContextDescriptor, 10_000);
        }
    }
}
