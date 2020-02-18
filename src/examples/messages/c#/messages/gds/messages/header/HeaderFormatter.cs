using MessagePack;
using MessagePack.Formatters;
using gds.messages.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.header
{
    class HeaderFormatter : IMessagePackFormatter<Header>
    {
        public void Serialize(ref MessagePackWriter writer, Header value, MessagePackSerializerOptions options)
        {
            writer.Write(value.UserName);
            writer.Write(value.MessageId);
            writer.Write(value.CreateTime);
            writer.Write(value.RequestTime);
            writer.Write(value.IsFragmented);
            MsgPackUtils.WriteNullableBool(value.FirstFragment, ref writer);
            MsgPackUtils.WriteNullableBool(value.LastFragment, ref writer);
            MsgPackUtils.WriteNullableInt(value.Offset, ref writer);
            MsgPackUtils.WriteNullableInt(value.FullDataSize, ref writer);
            writer.Write((int) value.DataType);
        }

        public Header Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            string userName = reader.ReadString();
            string messageId = reader.ReadString();
            long createTime = reader.ReadInt64();
            long requestTime = reader.ReadInt64();
            bool isFragmented = reader.ReadBoolean();
            bool? firstFragment = MsgPackUtils.ReadNullableBool(ref reader);
            bool? lastFragment = MsgPackUtils.ReadNullableBool(ref reader);
            int? offset = MsgPackUtils.ReadNullableInt(ref reader);
            int? fullDataSize = MsgPackUtils.ReadNullableInt(ref reader);
            int dataType = reader.ReadInt32();

            return new Header(userName, messageId, createTime, requestTime, isFragmented, firstFragment,
                lastFragment, offset, fullDataSize, (DataType) dataType);
        }
    }
}
