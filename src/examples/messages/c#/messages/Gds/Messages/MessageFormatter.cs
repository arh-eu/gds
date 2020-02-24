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

using MessagePack;
using MessagePack.Formatters;
using Gds.Messages.Data;
using Gds.Messages.Header;
using System;

namespace Gds.Messages
{
    class MessageFormatter : IMessagePackFormatter<Message>
    {
        public void Serialize(ref MessagePackWriter writer, Message value, MessagePackSerializerOptions options)
        {
            writer.WriteArrayHeader(11);
            MessagePackSerializer.Serialize(ref writer, value.Header, options);
            switch (value.Data.GetDataType())
            {
                case DataType.Connection:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsConnectionData(), options);
                    break;
                case DataType.ConnectionAck:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsConnectionAckData(), options);
                    break;
                case DataType.Event:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsEventData(), options);
                    break;
                case DataType.EventAck:
                    throw new InvalidOperationException("'EventAck' Message Data Type cannot be serlaized by client");
                case DataType.AttachmentRequest:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsAttachmentRequestData(), options);
                    break;
                case DataType.AttachmentRequestAck:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsAttachmentRequestAckData(), options);
                    break;
                case DataType.AttachmentResponse:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsAttachmentResponseData(), options);
                    break;
                case DataType.AttachmentResponseAck:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsAttachmentResponseAckData(), options);
                    break;
                case DataType.EventDocument:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsEventDocumentData(), options);
                    break;
                case DataType.EventDocumentAck:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsEventDocumentAckData(), options);
                    break;
                case DataType.QueryRequest:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsQueryRequest(), options);
                    break;
                case DataType.QueryRequestAck:
                    throw new InvalidOperationException("'QueryRequestAck' Message Data Type cannot be serlaized by client");
                case DataType.NextQueryPageRequest:
                    MessagePackSerializer.Serialize(ref writer, value.Data.AsNextQueryPageRequest(), options);
                    break;
                default:
                    throw new InvalidOperationException("Unknown Data Type: " + value.Data.GetDataType());
            }
        }

        public Message Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            reader.ReadArrayHeader();
            MessageHeader header = MessagePackSerializer.Deserialize<MessageHeader>(ref reader, options);
            MessageData data = header.DataType switch
            {
                DataType.Connection => MessagePackSerializer.Deserialize<ConnectionData>(ref reader, options),
                DataType.ConnectionAck => MessagePackSerializer.Deserialize<ConnectionAckData>(ref reader, options),
                DataType.Event => throw new InvalidOperationException("'Event' Message Data Type cannot be deserlaized by client"),
                DataType.EventAck => MessagePackSerializer.Deserialize<EventAckData>(ref reader, options),
                DataType.AttachmentRequest => MessagePackSerializer.Deserialize<AttachmentRequestData>(ref reader, options),
                DataType.AttachmentRequestAck => MessagePackSerializer.Deserialize<AttachmentRequestAckData>(ref reader, options),
                DataType.AttachmentResponse => MessagePackSerializer.Deserialize<AttachmentResponseData>(ref reader, options),
                DataType.AttachmentResponseAck => MessagePackSerializer.Deserialize<AttachmentResponseAckData>(ref reader, options),
                DataType.EventDocument => MessagePackSerializer.Deserialize<EventDocument>(ref reader, options),
                DataType.EventDocumentAck => MessagePackSerializer.Deserialize<EventDocumentAck>(ref reader, options),
                DataType.QueryRequest => throw new InvalidOperationException("'QueryRequest' Message Data Type cannot be deserlaized by client"),
                DataType.QueryRequestAck => MessagePackSerializer.Deserialize<QueryRequestAckData>(ref reader, options),
                DataType.NextQueryPageRequest => throw new InvalidOperationException("'NextQueryPageRequest' Message Data Type cannot be deserlaized by client"),
                _ => throw new InvalidOperationException("Unknown Data Type: " + header.DataType),
            };
            return new Message(header, data);
        }
    }
}
