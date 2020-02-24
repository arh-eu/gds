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

namespace Gds.Messages.Data
{
    /// <summary>
    /// The Connection type Data part of the Message.
    /// </summary>
    [MessagePackObject]
    public class ConnectionData : MessageData
    {
        [Key(0)]
        private readonly bool serveOnTheSameConnection;

        [Key(1)]
        private readonly int protocolVersionNumber;

        [Key(2)]
        private readonly bool fragmentationSupported;

        [Key(3)]
        private readonly int? fragmentationTransmissionUnit;

        [Key(4)]
        private readonly object[] reservedFields;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionData"/> class
        /// </summary>
        /// <param name="serveOnTheSameConnection">If true, the clients only accepts the response on the same connection the message was sent (on the connection it established).</param>
        /// <param name="protocolVersionNumber">The version number of the protocol, with which the connected client communicates.</param>
        /// <param name="fragmentationSupported">If true, the client indicates that it accepts messages on this connection fragmented too.</param>
        /// <param name="fragmentationTransmissionUnit">If fragmentation is supported, it determines the size of chunks the other party should fragment the data part of the message.</param>
        /// <param name="reservedFields"></param>
        public ConnectionData(bool serveOnTheSameConnection, int protocolVersionNumber, bool fragmentationSupported, int? fragmentationTransmissionUnit, object[] reservedFields)
        {
            this.serveOnTheSameConnection = serveOnTheSameConnection;
            this.protocolVersionNumber = protocolVersionNumber;
            this.fragmentationSupported = fragmentationSupported;
            this.fragmentationTransmissionUnit = fragmentationTransmissionUnit;
            this.reservedFields = reservedFields;
        }

        /// <summary>
        /// If true, the clients only accepts the response on the same connection the message was sent (on the connection it established).
        /// </summary>
        [IgnoreMember]
        public bool ServeOnTheSameConnection => serveOnTheSameConnection;

        /// <summary>
        /// The version number of the protocol, with which the connected client communicates.
        /// </summary>
        [IgnoreMember]
        public int ProtocolVersionNumber => protocolVersionNumber;

        /// <summary>
        /// If true, the client indicates that it accepts messages on this connection fragmented too.
        /// </summary>
        [IgnoreMember]
        public bool FragmentationSupported => fragmentationSupported;

        /// <summary>
        /// If fragmentation is supported, it determines the size of chunks the other party should fragment the data part of the message.
        /// </summary>
        [IgnoreMember]
        public int? FragmentationTransmissionUnit => fragmentationTransmissionUnit;

        [IgnoreMember]
        public object[] ReservedFields => reservedFields;

        /// <summary>
        /// For a password based authentication.
        /// </summary>
        [IgnoreMember]
        public string Password
        {
            get
            {
                if (reservedFields != null && reservedFields.Length >= 1)
                {
                    return (string)reservedFields[0];
                }
                else
                {
                    return null;
                }

            }
        }

        public override DataType GetDataType()
        {
            return DataType.Connection;
        }

        public override bool IsConnectionData()
        {
            return true;
        }

        public override ConnectionData AsConnectionData()
        {
            return this;
        }
    }
}
