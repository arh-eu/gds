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

namespace gds.message.data
{
    public abstract class Data
    {
        /// <summary>
        /// It specifies what types of information the data carries.
        /// </summary>
        /// <returns></returns>
        public abstract DataType GetDataType();

        public virtual bool IsConnectionData()
        {
            return false;
        }

        public virtual bool IsConnectionAckData()
        {
            return false;
        }

        public virtual bool IsEventData()
        {
            return false;
        }

        public virtual bool IsEventAckData()
        {
            return false;
        }

        public virtual bool IsAttachmentRequestData()
        {
            return false;
        }

        public virtual bool IsAttachmentRequestAckData()
        {
            return false;
        }

        public virtual bool IsAttachmentResponseData()
        {
            return false;
        }

        public virtual bool IsAttachmentResponseAckData()
        {
            return false;
        }

        public virtual bool IsEventDocumentData()
        {
            return false;
        }

        public virtual bool IsEventDocumentAckData()
        {
            return false;
        }

        public virtual bool IsQueryRequest()
        {
            return false;
        }

        public virtual bool IsQueryRequestAck()
        {
            return false;
        }

        public virtual bool IsNextQueryPageRequest()
        {
            return false;
        }

        public virtual Connection AsConnectionData()
        {
            throw new InvalidCastException();
        }

        public virtual ConnectionAck AsConnectionAckData()
        {
            throw new InvalidCastException();
        }

        public virtual Event AsEventData()
        {
            throw new InvalidCastException();
        }

        public virtual EventAck AsEventAckData()
        {
            throw new InvalidCastException();
        }

        public virtual AttachmentRequest AsAttachmentRequestData()
        {
            throw new InvalidCastException();
        }

        public virtual AttachmentRequestAck AsAttachmentRequestAckData()
        {
            throw new InvalidCastException();
        }

        public virtual AttachmentResponse AsAttachmentResponseData()
        {
            throw new InvalidCastException();
        }

        public virtual AttachmentResponseAck AsAttachmentResponseAckData()
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

        public virtual QueryRequest AsQueryRequest()
        {
            throw new InvalidCastException();
        }

        public virtual QueryRequestAck AsQueryRequestAck()
        {
            throw new InvalidCastException();
        }

        public virtual NextQueryPageRequest AsNextQueryPageRequest()
        {
            throw new InvalidCastException();
        }

        public override string ToString()
        {
            return (GetDataType()) switch
            {
                DataType.Connection => MessagePackSerializer.SerializeToJson<Connection>(AsConnectionData(), SerializerOptions.AllSerializerOptions),
                DataType.ConnectionAck => MessagePackSerializer.SerializeToJson<ConnectionAck>(AsConnectionAckData(), SerializerOptions.AllSerializerOptions),
                DataType.Event => MessagePackSerializer.SerializeToJson<Event>(AsEventData(), SerializerOptions.AllSerializerOptions),
                DataType.EventAck => MessagePackSerializer.SerializeToJson<EventAck>(AsEventAckData(), SerializerOptions.AllSerializerOptions),
                DataType.AttachmentRequest => MessagePackSerializer.SerializeToJson<AttachmentRequest>(AsAttachmentRequestData(), SerializerOptions.AllSerializerOptions),
                DataType.AttachmentRequestAck => MessagePackSerializer.SerializeToJson<AttachmentRequestAck>(AsAttachmentRequestAckData(), SerializerOptions.AllSerializerOptions),
                DataType.AttachmentResponse => MessagePackSerializer.SerializeToJson<AttachmentResponse>(AsAttachmentResponseData(), SerializerOptions.AllSerializerOptions),
                DataType.AttachmentResponseAck => MessagePackSerializer.SerializeToJson<AttachmentResponseAck>(AsAttachmentResponseAckData(), SerializerOptions.AllSerializerOptions),
                DataType.EventDocument => MessagePackSerializer.SerializeToJson<EventDocument>(AsEventDocumentData(), SerializerOptions.AllSerializerOptions),
                DataType.EventDocumentAck => MessagePackSerializer.SerializeToJson<EventDocumentAck>(AsEventDocumentAckData(), SerializerOptions.AllSerializerOptions),
                DataType.QueryRequest => MessagePackSerializer.SerializeToJson<QueryRequest>(AsQueryRequest(), SerializerOptions.AllSerializerOptions),
                DataType.QueryRequestAck => MessagePackSerializer.SerializeToJson<QueryRequestAck>(AsQueryRequestAck(), SerializerOptions.AllSerializerOptions),
                DataType.NextQueryPageRequest => MessagePackSerializer.SerializeToJson<NextQueryPageRequest>(AsNextQueryPageRequest(), SerializerOptions.AllSerializerOptions),
                _ => null,
            };
        }
    }
}
