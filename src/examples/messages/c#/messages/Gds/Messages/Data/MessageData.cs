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
using System;

namespace Gds.Messages.Data
{
    public abstract class MessageData
    {
        /// <summary>
        /// Specifies what types of information the data carries.
        /// </summary>
        /// <returns></returns>
        public abstract DataType GetDataType();

        /// <summary>
        /// Returns true if it is a <see cref="DataType.Connection"/> data.
        /// </summary>
        public virtual bool IsConnectionData()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.ConnectionAck"/> data.
        /// </summary>
        public virtual bool IsConnectionAckData()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.Event"/> data.
        /// </summary>
        public virtual bool IsEventData()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.EventAck"/> data.
        /// </summary>
        public virtual bool IsEventAckData()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.AttachmentRequest"/> data.
        /// </summary>
        public virtual bool IsAttachmentRequestData()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.AttachmentRequestAck"/> data.
        /// </summary>
        public virtual bool IsAttachmentRequestAckData()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.AttachmentResponse"/> data.
        /// </summary>
        public virtual bool IsAttachmentResponseData()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.AttachmentResponseAck"/> data.
        /// </summary>
        public virtual bool IsAttachmentResponseAckData()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.EventDocument"/> data.
        /// </summary>
        public virtual bool IsEventDocumentData()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.EventAck"/> data.
        /// </summary>
        public virtual bool IsEventDocumentAckData()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.QueryRequest"/> data.
        /// </summary>
        public virtual bool IsQueryRequest()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.QueryRequestAck"/> data.
        /// </summary>
        public virtual bool IsQueryRequestAck()
        {
            return false;
        }

        /// <summary>
        /// Returns true if it is a <see cref="DataType.NextQueryPageRequest"/> data.
        /// </summary>
        public virtual bool IsNextQueryPageRequest()
        {
            return false;
        }

        public virtual ConnectionData AsConnectionData()
        {
            throw new InvalidCastException();
        }

        public virtual ConnectionAckData AsConnectionAckData()
        {
            throw new InvalidCastException();
        }

        public virtual EventData AsEventData()
        {
            throw new InvalidCastException();
        }

        public virtual EventAckData AsEventAckData()
        {
            throw new InvalidCastException();
        }

        public virtual AttachmentRequestData AsAttachmentRequestData()
        {
            throw new InvalidCastException();
        }

        public virtual AttachmentRequestAckData AsAttachmentRequestAckData()
        {
            throw new InvalidCastException();
        }

        public virtual AttachmentResponseData AsAttachmentResponseData()
        {
            throw new InvalidCastException();
        }

        public virtual AttachmentResponseAckData AsAttachmentResponseAckData()
        {
            throw new InvalidCastException();
        }

        public virtual EventDocument AsEventDocumentData()
        {
            throw new InvalidCastException();
        }

        public virtual EventDocumentAck AsEventDocumentAckData()
        {
            throw new InvalidCastException();
        }

        public virtual QueryRequestData AsQueryRequest()
        {
            throw new InvalidCastException();
        }

        public virtual QueryRequestAckData AsQueryRequestAck()
        {
            throw new InvalidCastException();
        }

        public virtual NextQueryPageRequestData AsNextQueryPageRequest()
        {
            throw new InvalidCastException();
        }

        public override string ToString()
        {
            return (GetDataType()) switch
            {
                DataType.Connection => MessagePackSerializer.SerializeToJson<ConnectionData>(AsConnectionData(), SerializerOptions.AllSerializerOptions),
                DataType.ConnectionAck => MessagePackSerializer.SerializeToJson<ConnectionAckData>(AsConnectionAckData(), SerializerOptions.AllSerializerOptions),
                DataType.Event => MessagePackSerializer.SerializeToJson<EventData>(AsEventData(), SerializerOptions.AllSerializerOptions),
                DataType.EventAck => MessagePackSerializer.SerializeToJson<EventAckData>(AsEventAckData(), SerializerOptions.AllSerializerOptions),
                DataType.AttachmentRequest => MessagePackSerializer.SerializeToJson<AttachmentRequestData>(AsAttachmentRequestData(), SerializerOptions.AllSerializerOptions),
                DataType.AttachmentRequestAck => MessagePackSerializer.SerializeToJson<AttachmentRequestAckData>(AsAttachmentRequestAckData(), SerializerOptions.AllSerializerOptions),
                DataType.AttachmentResponse => MessagePackSerializer.SerializeToJson<AttachmentResponseData>(AsAttachmentResponseData(), SerializerOptions.AllSerializerOptions),
                DataType.AttachmentResponseAck => MessagePackSerializer.SerializeToJson<AttachmentResponseAckData>(AsAttachmentResponseAckData(), SerializerOptions.AllSerializerOptions),
                DataType.EventDocument => MessagePackSerializer.SerializeToJson<EventDocument>(AsEventDocumentData(), SerializerOptions.AllSerializerOptions),
                DataType.EventDocumentAck => MessagePackSerializer.SerializeToJson<EventDocumentAck>(AsEventDocumentAckData(), SerializerOptions.AllSerializerOptions),
                DataType.QueryRequest => MessagePackSerializer.SerializeToJson<QueryRequestData>(AsQueryRequest(), SerializerOptions.AllSerializerOptions),
                DataType.QueryRequestAck => MessagePackSerializer.SerializeToJson<QueryRequestAckData>(AsQueryRequestAck(), SerializerOptions.AllSerializerOptions),
                DataType.NextQueryPageRequest => MessagePackSerializer.SerializeToJson<NextQueryPageRequestData>(AsNextQueryPageRequest(), SerializerOptions.AllSerializerOptions),
                _ => null,
            };
        }
    }
}
