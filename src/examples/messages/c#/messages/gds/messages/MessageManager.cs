using gds.messages.data;
using gds.messages.header;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages
{
    public class MessageManager
    {
        public static byte[] GetBinaryFromMessage(Message message)
        {
            return MessagePackSerializer.Serialize(message, SerializerOptions.AllSerializerOptions);
        }

        public static Message GetMessageFromBinary(byte[] binary)
        {
            return MessagePackSerializer.Deserialize<Message>(binary, SerializerOptions.AllSerializerOptions);
        }

        public static Message GetMessage(Header header, Data data)
        {
            return new Message(header, data);
        }

        public static Header GetHeader(string userName, string messageId, long createTime,
            long requestTime, bool isFragmented, bool? firstFragment, bool? lastFragment, int? offset, 
            int? fullDataSize, DataType dataType)
        {
            return new Header(userName, messageId, createTime, requestTime, isFragmented, firstFragment,
                lastFragment, offset, fullDataSize, dataType);
        }

        public static Connection GetConnectionData(bool serveOnTheSameConnection,
            int protocolVersionNumber, bool fragmentationSupported, int? fragmentationTransmissionUnit, string password)
        {
            return new Connection(serveOnTheSameConnection, protocolVersionNumber, fragmentationSupported, fragmentationTransmissionUnit, new object[] { password });
        }

        public static Connection GetConnectionData(bool serveOnTheSameConnection,
            int protocolVersionNumber, bool fragmentationSupported, int? fragmentationTransmissionUnit)
        {
            return new Connection(serveOnTheSameConnection, protocolVersionNumber, fragmentationSupported, fragmentationTransmissionUnit, new object[] { null });
        }

        public static ConnectionAck GetConnectionAckData(StatusCode status, Connection successAckData, string exception)
        {
            return new ConnectionAck(status, successAckData, exception);
        }

        public static ConnectionAck GetConnectionAckData(StatusCode status, Dictionary<int, string> unauthorizedAckData, string exception)
        {
            return new ConnectionAck(status, unauthorizedAckData, exception);
        }

        public static ConnectionAck GetConnectionAckData(StatusCode status, string exception)
        {
            return new ConnectionAck(status, exception);
        }

        public static Event GetEventData(string operationsStringBlock, Dictionary<string, byte[]> binaryContentsMapping, List<List<Dictionary<int, bool>>> executionPriorityStructure)
        {
            return new Event(operationsStringBlock, binaryContentsMapping, executionPriorityStructure);
        }

        public static Event GetEventData(string operationsStringBlock, Dictionary<string, byte[]> binaryContentsMapping)
        {
            return new Event(operationsStringBlock, binaryContentsMapping, new List<List<Dictionary<int, bool>>>());
        }

        public static Event GetEventData(string operationsStringBlock, List<List<Dictionary<int, bool>>> executionPriorityStructure)
        {
            return new Event(operationsStringBlock, new Dictionary<string, byte[]>(), executionPriorityStructure);
        }

        public static Event GetEventData(string operationsStringBlock)
        {
            return new Event(operationsStringBlock, new Dictionary<string, byte[]>(), new List<List<Dictionary<int, bool>>>());
        }

        public static AttachmentRequest GetAttachmentRequestData(string request)
        {
            return new AttachmentRequest(request);
        }

        public static AttachmentRequestAck GetAttachmentRequestAckData(StatusCode status, AttachmentRequestAckTypeData ackData, string exception)
        {
            return new AttachmentRequestAck(status, ackData, exception);
        }

        public static AttachmentRequestAck GetAttachmentRequestAckData(StatusCode status, string exception)
        {
            return new AttachmentRequestAck(status, null, exception);
        }

        public static AttachmentResponse GetAttachmentResponseData(AttachmentResult result)
        {
            return new AttachmentResponse(result);
        }

        public static AttachmentResponseAck GetAttachmentResponseAckData(StatusCode status, AttachmentResponseAckTypeData ackData, string exception)
        {
            return new AttachmentResponseAck(status, ackData, exception);
        }

        public static AttachmentResponseAck GetAttachmentResponseAckData(StatusCode status, string exception)
        {
            return new AttachmentResponseAck(status, null, exception);
        }

        public static EventDocument GetEventDocumentData(string tableName, List<FieldDescriptor> fieldDescriptors, List<List<object>> records)
        {
            return new EventDocument(tableName, fieldDescriptors, records);
        }

        public static EventDocumentAck GetEventDocumentAckData(StatusCode status, List<EventDocumentAckResult> ackData, string exception)
        {
            return new EventDocumentAck(status, ackData, exception);
        }

        public static EventDocumentAck GetEventDocumentAckData(StatusCode status, string exception)
        {
            return new EventDocumentAck(status, null, exception);
        }

        public static QueryRequest GetQueryRequest(string selectStringBlock, string consistencyType, long timeout, int queryPageSize, int queryType)
        {
            return new QueryRequest(selectStringBlock, consistencyType, timeout, queryPageSize, queryType);
        }

        public static QueryRequest GetQueryRequest(string selectStringBlock, string consistencyType, long timeout)
        {
            return new QueryRequest(selectStringBlock, consistencyType, timeout);
        }

        public static QueryRequestAck GetQueryRequestAck(StatusCode status, QueryRequestAckTypeData ackData, string exception)
        {
            return new QueryRequestAck(status, ackData, exception);
        }

        public static QueryRequestAck GetQueryRequestAck(StatusCode status, string exception)
        {
            return new QueryRequestAck(status, null, exception);
        }

        public static NextQueryPageRequest GetNextQueryPageRequest(QueryContextDescriptor queryContextDescriptor, long timeout)
        {
            return new NextQueryPageRequest(queryContextDescriptor, timeout);
        }
    }
}
