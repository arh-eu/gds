/*
 * Copyright 2020 ARH Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Gds.Messages.Data;
using Gds.Messages.Header;
using MessagePack;
using System.Collections.Generic;

namespace Gds.Messages
{
    /// <summary>
    /// Helper class for create, pack and unpack messages
    /// </summary>
    public class MessageManager
    {
        /// <summary>
        /// Pack the specified Message object. 
        /// </summary>
        /// <param name="message">The Message object. See <see cref="GetMessage"/>.</param>
        /// <returns>The packed message in byte array format.</returns>
        /// <exception cref="MessagePackSerializationException"></exception>
        public static byte[] GetBinaryFromMessage(Message message)
        {
            return MessagePackSerializer.Serialize(message, SerializerOptions.AllSerializerOptions);
        }

        /// <summary>
        /// Unpack the specified binary message.
        /// </summary>
        /// <param name="message">The message in byte array format.</param>
        /// <returns>The unpacked Message object.</returns>
        /// /// <exception cref="MessagePackSerializationException"></exception>
        public static Message GetMessageFromBinary(byte[] message)
        {
            return MessagePackSerializer.Deserialize<Message>(message, SerializerOptions.AllSerializerOptions);
        }

        /// <summary>
        /// Create a Message object with the speficed Header and Data parts.
        /// </summary>
        /// <param name="header">The Header part of the message. See <see cref="GetHeader"/>.</param>
        /// <param name="data">The Data part of the message. See the MessageManager.GetXXXData() methods.</param>
        /// <returns>The created Message object.</returns>
        public static Message GetMessage(MessageHeader header, MessageData data)
        {
            return new Message(header, data);
        }

        /// <summary>
        /// Create the Header part of the Message.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="messageId">The identifier of the message, with which the request can be associated.</param>
        /// <param name="createTime">The time of creating the message, epoch timestamp.</param>
        /// <param name="requestTime">The time of the request, epoch timestamp.</param>
        /// <param name="isFragmented">Whether the current message is fragmented or not.</param>
        /// <param name="firstFragment">Whether it's the first fragment of a fragmented message</param>
        /// <param name="lastFragment">Whether it's the last fragment of a fragmented message.</param>
        /// <param name="offset">The last byte of the original, fragmented message the receiver has yet received or the last byte of the original message that has yet been forwarded.</param>
        /// <param name="fullDataSize">The size of the full data in bytes.</param>
        /// <param name="dataType">It specifies what types of information the data carries.</param>
        /// <returns>The created MessageHeader object.</returns>
        public static MessageHeader GetHeader(string userName, string messageId, long createTime, long requestTime, bool isFragmented, bool? firstFragment, bool? lastFragment, int? offset, int? fullDataSize, DataType dataType)
        {
            return new MessageHeader(userName, messageId, createTime, requestTime, isFragmented, firstFragment,
                lastFragment, offset, fullDataSize, dataType);
        }

        /// <summary>
        /// Create a Connection type data part.
        /// </summary>
        /// <param name="serveOnTheSameConnection">If true, the clients only accepts the response on the same connection the message was sent (on the connection it established).</param>
        /// <param name="protocolVersionNumber">The version number of the protocol, with which the connected client communicates.</param>
        /// <param name="fragmentationSupported">If true, the client indicates that it accepts messages on this connection fragmented too.</param>
        /// <param name="fragmentationTransmissionUnit">If fragmentation is supported, it determines the size of chunks the other party should fragment the data part of the message.</param>
        /// <param name="password">For a password based authentication.</param>
        /// <returns>The created ConnectionData object.</returns>
        public static ConnectionData GetConnectionData(bool serveOnTheSameConnection, int protocolVersionNumber, bool fragmentationSupported, int? fragmentationTransmissionUnit, string password)
        {
            return new ConnectionData(serveOnTheSameConnection, protocolVersionNumber, fragmentationSupported, fragmentationTransmissionUnit, new object[] { password });
        }

        /// <summary>
        /// Create a Connection type data part.
        /// </summary>
        /// <param name="serveOnTheSameConnection">If true, the clients only accepts the response on the same connection the message was sent (on the connection it established).</param>
        /// <param name="protocolVersionNumber">The version number of the protocol, with which the connected client communicates.</param>
        /// <param name="fragmentationSupported">If true, the client indicates that it accepts messages on this connection fragmented too.</param>
        /// <param name="fragmentationTransmissionUnit">If fragmentation is supported, it determines the size of chunks the other party should fragment the data part of the message.</param>
        /// <returns>The created ConnectionData object.</returns>
        public static ConnectionData GetConnectionData(bool serveOnTheSameConnection, int protocolVersionNumber, bool fragmentationSupported, int? fragmentationTransmissionUnit)
        {
            return new ConnectionData(serveOnTheSameConnection, protocolVersionNumber, fragmentationSupported, fragmentationTransmissionUnit, new object[] { null });
        }

        /// <summary>
        /// Create a ConnectionAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="successAckData">The sucess response object belonging to the acknowledgement.</param>
        /// <returns>The created ConnectionAckData object.</returns>
        public static ConnectionAckData GetConnectionAckData(StatusCode status, ConnectionData successAckData)
        {
            return new ConnectionAckData(status, successAckData);
        }

        /// <summary>
        /// Create a ConnectionAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="unauthorizedAckData">The unauthorized response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        /// <returns>The created ConnectionAckData object.</returns>
        public static ConnectionAckData GetConnectionAckData(StatusCode status, Dictionary<int, string> unauthorizedAckData, string exception)
        {
            return new ConnectionAckData(status, unauthorizedAckData, exception);
        }

        /// <summary>
        /// Create a ConnectionAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regardingg the response.</param>
        /// <param name="exception">The description of an error.</param>
        /// <returns>The created ConnectionAckData object.</returns>
        public static ConnectionAckData GetConnectionAckData(StatusCode status, string exception)
        {
            return new ConnectionAckData(status, exception);
        }

        /// <summary>
        /// Create an Event type data part.
        /// </summary>
        /// <param name="operationsStringBlock">The operations in standard SQL statements, separated with ';' characters.</param>
        /// <param name="binaryContentsMapping">The mapping of the binary contents.</param>
        /// <param name="executionPriorityStructure">The execution priority structure.</param>
        /// <returns>The created EventData object.</returns>
        public static EventData GetEventData(string operationsStringBlock, Dictionary<string, byte[]> binaryContentsMapping, List<List<Dictionary<int, bool>>> executionPriorityStructure)
        {
            return new EventData(operationsStringBlock, binaryContentsMapping, executionPriorityStructure);
        }

        /// <summary>
        /// Create an Event type data part.
        /// </summary>
        /// <param name="operationsStringBlock">The operations in standard SQL statements, separated with ';' characters.</param>
        /// <param name="binaryContentsMapping">The mapping of the binary contents.</param>
        /// <returns>The created EventData object.</returns>
        public static EventData GetEventData(string operationsStringBlock, Dictionary<string, byte[]> binaryContentsMapping)
        {
            return new EventData(operationsStringBlock, binaryContentsMapping, new List<List<Dictionary<int, bool>>>());
        }

        /// <summary>
        /// Create an Event type data part.
        /// </summary>
        /// <param name="operationsStringBlock">The operations in standard SQL statements, separated with ';' characters.</param>
        /// <param name="executionPriorityStructure">The execution priority structure.</param>
        /// <returns>The created EventData object.</returns>
        public static EventData GetEventData(string operationsStringBlock, List<List<Dictionary<int, bool>>> executionPriorityStructure)
        {
            return new EventData(operationsStringBlock, new Dictionary<string, byte[]>(), executionPriorityStructure);
        }

        /// <summary>
        /// Create an Event type data part.
        /// </summary>
        /// <param name="operationsStringBlock"></param>
        /// <returns>The created EventData object.</returns>
        public static EventData GetEventData(string operationsStringBlock)
        {
            return new EventData(operationsStringBlock, new Dictionary<string, byte[]>(), new List<List<Dictionary<int, bool>>>());
        }

        /// <summary>
        /// Create an AttachmentRequest type data part.
        /// </summary>
        /// <param name="request">The SELECT statement.</param>
        /// <returns>The created AttachmentRequestData object.</returns>
        public static AttachmentRequestData GetAttachmentRequestData(string request)
        {
            return new AttachmentRequestData(request);
        }

        /// <summary>
        /// Create an AttachmentRequestAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The response object belonging to the acknowledgement.</param>
        /// <returns>The created AttachmentRequestAckData object.</returns>
        public static AttachmentRequestAckData GetAttachmentRequestAckData(StatusCode status, AttachmentRequestAckTypeData ackData)
        {
            return new AttachmentRequestAckData(status, ackData, null);
        }

        /// <summary>
        /// Create an AttachmentRequestAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        /// <returns>The created AttachmentRequestAckData object.</returns>
        public static AttachmentRequestAckData GetAttachmentRequestAckData(StatusCode status, AttachmentRequestAckTypeData ackData, string exception)
        {
            return new AttachmentRequestAckData(status, ackData, exception);
        }

        /// <summary>
        /// Create an AttachmentRequestAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="exception">The description of an error.</param>
        /// <returns>The created AttachmentRequestAckData object.</returns>
        public static AttachmentRequestAckData GetAttachmentRequestAckData(StatusCode status, string exception)
        {
            return new AttachmentRequestAckData(status, null, exception);
        }

        /// <summary>
        /// Create an AttachmentResponse type data part.
        /// </summary>
        /// <param name="result">The description of the result.</param>
        /// <returns>The created AttachmentResponseData object.</returns>
        public static AttachmentResponseData GetAttachmentResponseData(AttachmentResult result)
        {
            return new AttachmentResponseData(result);
        }

        /// <summary>
        /// Create an AttachmentResponseAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The response object belonging to the acknowledgement.</param>
        /// <returns>The created AttachmentResponseAckData object.</returns>
        public static AttachmentResponseAckData GetAttachmentResponseAckData(StatusCode status, AttachmentResponseAckTypeData ackData)
        {
            return new AttachmentResponseAckData(status, ackData, null);
        }

        /// <summary>
        /// Create an AttachmentResponseAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        /// <returns>The created AttachmentResponseAckData object.</returns>
        public static AttachmentResponseAckData GetAttachmentResponseAckData(StatusCode status, AttachmentResponseAckTypeData ackData, string exception)
        {
            return new AttachmentResponseAckData(status, ackData, exception);
        }

        /// <summary>
        /// Create an AttachmentResponseAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="exception">The description of an error.</param>
        /// <returns>The created AttachmentResponseAckData object.</returns>
        public static AttachmentResponseAckData GetAttachmentResponseAckData(StatusCode status, string exception)
        {
            return new AttachmentResponseAckData(status, null, exception);
        }

        /// <summary>
        /// Create an EventDocument type data part.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="fieldDescriptors">The field descriptors.</param>
        /// <param name="records">The records.</param>
        /// <returns>The created EventDocumentData object.</returns>
        public static EventDocument GetEventDocumentData(string tableName, List<FieldDescriptor> fieldDescriptors, List<List<object>> records)
        {
            return new EventDocument(tableName, fieldDescriptors, records);
        }

        /// <summary>
        /// Create an EventDocumentAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The sucess response object belonging to the acknowledgement.</param>
        /// <returns>The created EventDocumentAckData object.</returns>
        public static EventDocumentAck GetEventDocumentAckData(StatusCode status, List<EventDocumentAckResult> ackData)
        {
            return new EventDocumentAck(status, ackData, null);
        }

        /// <summary>
        /// Create an EventDocumentAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The sucess response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        /// <returns>The created EventDocumentAckData object.</returns>
        public static EventDocumentAck GetEventDocumentAckData(StatusCode status, List<EventDocumentAckResult> ackData, string exception)
        {
            return new EventDocumentAck(status, ackData, exception);
        }

        /// <summary>
        /// Create an EventDocumentAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="exception">The description of an error.</param>
        /// <returns>The created EventDocumentAckData object.</returns>
        public static EventDocumentAck GetEventDocumentAckData(StatusCode status, string exception)
        {
            return new EventDocumentAck(status, null, exception);
        }

        /// <summary>
        /// Create a QueryRequest type data part.
        /// </summary>
        /// <param name="selectStringBlock">The SELECT statement.</param>
        /// <param name="consistencyType">The consistency type used for the query.</param>
        /// <param name="timeout">The timeout value in milliseconds.</param>
        /// <param name="queryPageSize">The number of records per page.</param>
        /// <param name="queryType">The query type</param>
        /// <returns>The created QueryRequestData object.</returns>
        public static QueryRequestData GetQueryRequest(string selectStringBlock, ConsistencyType consistencyType, long timeout, int queryPageSize, QueryType queryType)
        {
            return new QueryRequestData(selectStringBlock, consistencyType, timeout, queryPageSize, queryType);
        }

        /// <summary>
        /// Create a QueryRequest type data part.
        /// </summary>
        /// <param name="selectStringBlock">The SELECT statement.</param>
        /// <param name="consistencyType">The consistency type used for the query.</param>
        /// <param name="timeout">The timeout value in milliseconds.</param>
        /// <returns>The created QueryRequestData object.</returns>
        public static QueryRequestData GetQueryRequest(string selectStringBlock, ConsistencyType consistencyType, long timeout)
        {
            return new QueryRequestData(selectStringBlock, consistencyType, timeout);
        }

        /// <summary>
        /// Create a QueryRequestAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The sucess response object belonging to the acknowledgement.</param>
        /// <returns>The created QueryRequestAckData object.</returns>
        public static QueryRequestAckData GetQueryRequestAck(StatusCode status, QueryRequestAckTypeData ackData)
        {
            return new QueryRequestAckData(status, ackData, null);
        }

        /// <summary>
        /// Create a QueryRequestAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The sucess response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        /// <returns>The created QueryRequestAckData object.</returns>
        public static QueryRequestAckData GetQueryRequestAck(StatusCode status, QueryRequestAckTypeData ackData, string exception)
        {
            return new QueryRequestAckData(status, ackData, exception);
        }

        /// <summary>
        /// Create a QueryRequestAck type data part.
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="exception">The description of an error.</param>
        /// <returns>The created QueryRequestAckData object.</returns>
        public static QueryRequestAckData GetQueryRequestAck(StatusCode status, string exception)
        {
            return new QueryRequestAckData(status, null, exception);
        }

        /// <summary>
        /// Create a NextQueryPageRequest type data part.
        /// </summary>
        /// <param name="queryContextDescriptor">Query status descriptor for querying the next pages.</param>
        /// <param name="timeout">The timeout value in milliseconds.</param>
        /// <returns>The created NextQueryPageRequestData object.</returns>
        public static NextQueryPageRequestData GetNextQueryPageRequest(QueryContextDescriptor queryContextDescriptor, long timeout)
        {
            return new NextQueryPageRequestData(queryContextDescriptor, timeout);
        }
    }
}
