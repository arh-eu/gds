using gds.messages.data;
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class EventDocumentAck : Data
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly List<EventDocumentAckResult> ackData;

        [Key(2)]
        private readonly string exception;

        public EventDocumentAck(StatusCode status, List<EventDocumentAckResult> ackData, string exception)
        {
            this.status = status;
            this.ackData = ackData;
            this.exception = exception;
        }

        [IgnoreMember]
        public StatusCode Status => status;

        [IgnoreMember]
        public List<EventDocumentAckResult> AckData => ackData;

        [IgnoreMember]
        public string Exception => exception;

        public override DataType GetDataType()
        {
            return DataType.EventDocumentAck;
        }

        public override bool IsEventDocumentAckData()
        {
            return true;
        }

        public override EventDocumentAck AsEventDocumentAckData()
        {
            return this;
        }
    }

    public class EventDocumentAckResult
    {
        private readonly StatusCode status;
        private readonly string notification;

        public EventDocumentAckResult(StatusCode status, string notification)
        {
            this.status = status;
            this.notification = notification;
        }

        public StatusCode Status => status;

        public string Notification => notification;
    }

    public class EventDocumentAckResultFormatter : IMessagePackFormatter<EventDocumentAckResult>
    {
        public void Serialize(ref MessagePackWriter writer, EventDocumentAckResult value, MessagePackSerializerOptions options)
        {
            writer.WriteArrayHeader(3);
            writer.Write((int) value.Status);
            writer.Write(value.Notification);
            writer.WriteMapHeader(0);
        }

        public EventDocumentAckResult Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            reader.ReadArrayHeader();
            StatusCode status = (StatusCode) reader.ReadInt32();
            string notification = reader.ReadString();
            reader.ReadMapHeader();
            return new EventDocumentAckResult(status, notification);
        }
    }
}
