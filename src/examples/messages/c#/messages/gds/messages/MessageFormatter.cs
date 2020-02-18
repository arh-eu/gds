using MessagePack;
using MessagePack.Formatters;
using gds.messages.data;
using gds.messages.header;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages
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
            Header header = MessagePackSerializer.Deserialize<Header>(ref reader, options);
            Data data = header.DataType switch
            {
                DataType.Connection => MessagePackSerializer.Deserialize<Connection>(ref reader, options),
                DataType.ConnectionAck => MessagePackSerializer.Deserialize<ConnectionAck>(ref reader, options),
                DataType.Event => throw new InvalidOperationException("'Event' Message Data Type cannot be deserlaized by client"),
                DataType.EventAck => MessagePackSerializer.Deserialize<EventAck>(ref reader, options),
                DataType.AttachmentRequest => MessagePackSerializer.Deserialize<AttachmentRequest>(ref reader, options),
                DataType.AttachmentRequestAck => MessagePackSerializer.Deserialize<AttachmentRequestAck>(ref reader, options),
                DataType.AttachmentResponse => MessagePackSerializer.Deserialize<AttachmentResponse>(ref reader, options),
                DataType.AttachmentResponseAck => MessagePackSerializer.Deserialize<AttachmentResponseAck>(ref reader, options),
                DataType.EventDocument => MessagePackSerializer.Deserialize<EventDocument>(ref reader, options),
                DataType.EventDocumentAck => MessagePackSerializer.Deserialize<EventDocumentAck>(ref reader, options),
                DataType.QueryRequest => throw new InvalidOperationException("'QueryRequest' Message Data Type cannot be deserlaized by client"),
                DataType.QueryRequestAck => MessagePackSerializer.Deserialize<QueryRequestAck>(ref reader, options),
                DataType.NextQueryPageRequest => throw new InvalidOperationException("'NextQueryPageRequest' Message Data Type cannot be deserlaized by client"),
                _ => throw new InvalidOperationException("Unknown Data Type: " + header.DataType),
            };
            return new Message(header, data);
        }
    }
}
