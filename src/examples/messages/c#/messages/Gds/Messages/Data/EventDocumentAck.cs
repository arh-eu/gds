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
using System.Collections.Generic;

namespace Gds.Messages.Data
{
    /// <summary>
    /// Event Document Ack type Data part of the Message
    /// </summary>
    [MessagePackObject]
    public class EventDocumentAck : MessageData
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly List<EventDocumentAckResult> ackData;

        [Key(2)]
        private readonly string exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDocumentAck"/> class
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The sucess response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        public EventDocumentAck(StatusCode status, List<EventDocumentAckResult> ackData, string exception)
        {
            this.status = status;
            this.ackData = ackData;
            this.exception = exception;
        }

        /// <summary>
        /// The status incorporates a global signal regarding the response.
        /// </summary>
        [IgnoreMember]
        public StatusCode Status => status;

        /// <summary>
        /// The sucess response object belonging to the acknowledgement.
        /// </summary>
        [IgnoreMember]
        public List<EventDocumentAckResult> AckData => ackData;

        /// <summary>
        /// The description of an error.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDocumentAckResult"/> class
        /// </summary>
        /// <param name="status"></param>
        /// <param name="notification"></param>
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
            writer.Write((int)value.Status);
            writer.Write(value.Notification);
            writer.WriteMapHeader(0);
        }

        public EventDocumentAckResult Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            reader.ReadArrayHeader();
            StatusCode status = (StatusCode)reader.ReadInt32();
            string notification = reader.ReadString();
            reader.ReadMapHeader();
            return new EventDocumentAckResult(status, notification);
        }
    }
}
