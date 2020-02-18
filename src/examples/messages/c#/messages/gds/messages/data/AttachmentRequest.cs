using gds.messages.data;
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    public class AttachmentRequest : Data
    {
        private readonly string request;

        public AttachmentRequest(string request)
        {
            this.request = request;
        }

        public string Request => request;

        public override DataType GetDataType()
        {
            return DataType.AttachmentRequest;
        }

        public override bool IsAttachmentRequestData()
        {
            return true;
        }

        public override AttachmentRequest AsAttachmentRequestData()
        {
            return this;
        }
    }

    class AttachmentRequestFormatter : IMessagePackFormatter<AttachmentRequest>
    {
        public void Serialize(ref MessagePackWriter writer, AttachmentRequest value, MessagePackSerializerOptions options)
        {
            writer.Write(value.Request);
        }

        public AttachmentRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return new AttachmentRequest(reader.ReadString());
        }
    }
}
