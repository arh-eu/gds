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

namespace Gds.Messages.Data
{
    /// <summary>
    /// Attachment Request type Data part of the Message
    /// </summary>
    public class AttachmentRequestData : MessageData
    {
        private readonly string request;

        /// <summary>
        /// nitializes a new instance of the <see cref="AttachmentRequestData"/> class
        /// </summary>
        /// <param name="request">The SELECT statement.</param>
        public AttachmentRequestData(string request)
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

        public override AttachmentRequestData AsAttachmentRequestData()
        {
            return this;
        }
    }

    class AttachmentRequestFormatter : IMessagePackFormatter<AttachmentRequestData>
    {
        public void Serialize(ref MessagePackWriter writer, AttachmentRequestData value, MessagePackSerializerOptions options)
        {
            writer.Write(value.Request);
        }

        public AttachmentRequestData Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return new AttachmentRequestData(reader.ReadString());
        }
    }
}
