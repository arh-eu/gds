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

namespace Gds.Messages.Header
{
    class MessageHeaderFormatter : IMessagePackFormatter<MessageHeader>
    {
        public void Serialize(ref MessagePackWriter writer, MessageHeader value, MessagePackSerializerOptions options)
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
            writer.Write((int)value.DataType);
        }

        public MessageHeader Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
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

            return new MessageHeader(userName, messageId, createTime, requestTime, isFragmented, firstFragment,
                lastFragment, offset, fullDataSize, (DataType)dataType);
        }
    }
}
