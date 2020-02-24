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
using System;
using System.Collections.Generic;

namespace Gds.Messages.Data
{
    /// <summary>
    /// The Connection Ack type Data part of the Message
    /// </summary>
    [MessagePackObject]
    public class ConnectionAckData : MessageData
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly ConnectionAckTypeData ackData;

        [Key(2)]
        private readonly string exception;

        [SerializationConstructor]
        private ConnectionAckData(StatusCode status, ConnectionAckTypeData ackData, string exception)
        {
            this.status = status;
            this.ackData = ackData;
            this.exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionAckData"/> class
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="successAckData">The sucess response object belonging to the acknowledgement.</param>
        public ConnectionAckData(StatusCode status, ConnectionData successAckData)
        {
            this.status = status;
            this.ackData = new ConnectionAckTypeData(successAckData);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionAckData"/> class
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="unauthorizedAckData">The unauthorized response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        public ConnectionAckData(StatusCode status, Dictionary<int, string> unauthorizedAckData, string exception)
        {
            this.status = status;
            this.ackData = new ConnectionAckTypeData(unauthorizedAckData);
            this.exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionAckData"/> class
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="exception">The description of an error.</param>
        public ConnectionAckData(StatusCode status, string exception)
        {
            this.status = status;
            this.ackData = new ConnectionAckTypeData();
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
        public ConnectionData SuccessAckData => ackData.SuccessAckTypeData;

        /// <summary>
        /// The unauthorized response object belonging to the acknowledgement.
        /// </summary>
        [IgnoreMember]
        public Dictionary<int, string> UnauthorizedAckData => ackData.UnauthorizedAckTypeData;

        /// <summary>
        /// The description of an error.
        /// </summary>
        [IgnoreMember]
        public String Exception => exception;

        public override DataType GetDataType()
        {
            return DataType.ConnectionAck;
        }

        public override bool IsConnectionAckData()
        {
            return true;
        }

        public override ConnectionAckData AsConnectionAckData()
        {
            return this;
        }
    }

    public class ConnectionAckTypeData
    {
        private readonly ConnectionData successAckTypeData;
        private readonly Dictionary<int, string> unauthorizedAckTypeData;

        public ConnectionAckTypeData(ConnectionData successAckTypeData)
        {
            this.successAckTypeData = successAckTypeData;
        }

        public ConnectionAckTypeData(Dictionary<int, string> unauthorizedAckTypeData)
        {
            this.unauthorizedAckTypeData = unauthorizedAckTypeData;
        }

        public ConnectionAckTypeData() { }

        public ConnectionData SuccessAckTypeData => successAckTypeData;

        public Dictionary<int, string> UnauthorizedAckTypeData => unauthorizedAckTypeData;
    }

    public class ConnectionAckTypeDataFormatter : IMessagePackFormatter<ConnectionAckTypeData>
    {
        public void Serialize(ref MessagePackWriter writer, ConnectionAckTypeData value, MessagePackSerializerOptions options)
        {
            if (value.SuccessAckTypeData != null)
            {
                MessagePackSerializer.Serialize(ref writer, value.SuccessAckTypeData, options);
            }
            else if (value.UnauthorizedAckTypeData != null)
            {
                MessagePackSerializer.Serialize(ref writer, value.UnauthorizedAckTypeData);
            }
            else
            {
                writer.WriteNil();
            }
        }

        public ConnectionAckTypeData Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.NextMessagePackType.Equals(MessagePackType.Array))
            {
                return new ConnectionAckTypeData(
                    MessagePackSerializer.Deserialize<ConnectionData>(ref reader, options));
            }
            else if (reader.NextMessagePackType.Equals(MessagePackType.Map))
            {
                return new ConnectionAckTypeData(
                    MessagePackSerializer.Deserialize<Dictionary<int, string>>(ref reader));
            }
            else
            {
                reader.ReadNil();
                return null;
            }
        }
    }
}
