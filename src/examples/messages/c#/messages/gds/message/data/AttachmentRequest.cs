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

namespace gds.message.data
{
    /// <summary>
    /// AttachmentRequest type data part of the Message
    /// </summary>
    public class AttachmentRequest : Data
    {
        private readonly string request;

        public AttachmentRequest(string request)
        {
            this.request = request;
        }

        /// <summary>
        /// The SELECT statement.
        /// </summary>
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
