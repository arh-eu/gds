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
using System.Collections.Generic;

namespace Gds.Messages.Data
{
    /// <summary>
    /// Attachment Request Ack type Data part of the Message
    /// </summary>
    [MessagePackObject]
    public class AttachmentRequestAckData : MessageData
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly AttachmentRequestAckTypeData ackData;

        [Key(2)]
        private readonly string exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentRequestAckData"/> class
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        public AttachmentRequestAckData(StatusCode status, AttachmentRequestAckTypeData ackData, string exception)
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
        /// The response object belonging to the acknowledgement.
        /// </summary>
        [IgnoreMember]
        public AttachmentRequestAckTypeData AckData => ackData;

        /// <summary>
        /// The description of an error.
        /// </summary>
        [IgnoreMember]
        internal string Exception => exception;

        public override DataType GetDataType()
        {
            return DataType.AttachmentRequestAck;
        }

        public override bool IsAttachmentRequestAckData()
        {
            return true;
        }

        public override AttachmentRequestAckData AsAttachmentRequestAckData()
        {
            return this;
        }
    }

    [MessagePackObject]
    public class AttachmentRequestAckTypeData
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly AttachmentResult result;

        [Key(2)]
        private readonly long? remainedWaitTimeMillis;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentRequestAckTypeData"/> class
        /// </summary>
        /// <param name="status">The sub-type belonging to the global status.</param>
        /// <param name="result">The description of the result.</param>
        /// <param name="remainedWaitTimeMillis">It indicates how many milliseconds are left of the attachment obtainment time if the request cannot be served yet.</param>
        public AttachmentRequestAckTypeData(StatusCode status, AttachmentResult result, long? remainedWaitTimeMillis)
        {
            this.status = status;
            this.result = result;
            this.remainedWaitTimeMillis = remainedWaitTimeMillis;
        }

        /// <summary>
        /// The sub-type belonging to the global status.
        /// </summary>
        [IgnoreMember]
        public StatusCode Status => status;

        /// <summary>
        /// The description of the result.
        /// </summary>
        [IgnoreMember]
        public AttachmentResult Result => result;

        /// <summary>
        /// It indicates how many milliseconds are left of the attachment obtainment time if the request cannot be served yet.
        /// </summary>
        [IgnoreMember]
        public long? RemainedWaitTimeMillis => remainedWaitTimeMillis;
    }

    [MessagePackObject(keyAsPropertyName: true)]
    public class AttachmentResult
    {
        private readonly List<string> requestids;
        private readonly string ownertable;
        private readonly string attachmentid;
        private readonly List<string> ownerids;
        private readonly string meta;
        private readonly long? ttl;
        private readonly long? to_valid;
        private readonly byte[] attachment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentResult"/> class
        /// </summary>
        /// <param name="requestids">Contains the request message identifier(s).</param>
        /// <param name="ownertable">The table name of the event record that owns the attachment.</param>
        /// <param name="attachmentid">The unique identifier of the attachment.</param>
        /// <param name="ownerids">The identifier(s) of the event(s) owning the attachment.</param>
        /// <param name="meta">The meta descriptor of the attachment in JSON form.</param>
        /// <param name="ttl">The preservation time of the attachment in milliseconds, relative to the date of storing.</param>
        /// <param name="to_valid">The preservation time of the attachment as a millisecond based epoch timestamp.</param>
        /// <param name="attachment">The attachment in binary format. </param>
        public AttachmentResult(List<string> requestids, string ownertable, string attachmentid, List<string> ownerids, string meta, long? ttl, long? to_valid, byte[] attachment)
        {
            this.requestids = requestids;
            this.ownertable = ownertable;
            this.attachmentid = attachmentid;
            this.ownerids = ownerids;
            this.meta = meta;
            this.ttl = ttl;
            this.to_valid = to_valid;
            this.attachment = attachment;
        }

        /// <summary>
        /// Contains the request message identifier(s).
        /// </summary>
        [IgnoreMember]
        public List<string> RequestIds => requestids;

        /// <summary>
        /// The table name of the event record that owns the attachment.
        /// </summary>
        [IgnoreMember]
        public string OwnerTable => ownertable;

        /// <summary>
        /// The unique identifier of the attachment.
        /// </summary>
        [IgnoreMember]
        public string AttachmentId => attachmentid;

        /// <summary>
        /// The identifier(s) of the event(s) owning the attachment.
        /// </summary>
        [IgnoreMember]
        public List<string> OwnerIds => ownerids;

        /// <summary>
        /// The meta descriptor of the attachment in JSON form.
        /// </summary>
        [IgnoreMember]
        public string Meta => meta;

        /// <summary>
        /// The preservation time of the attachment in milliseconds, relative to the date of storing.
        /// </summary>
        [IgnoreMember]
        public long? Ttl => ttl;

        /// <summary>
        /// The preservation time of the attachment as a millisecond based epoch timestamp.
        /// </summary>
        [IgnoreMember]
        public long? ToValid => to_valid;

        /// <summary>
        /// The attachment in binary format. 
        /// </summary>
        [IgnoreMember]
        public byte[] Attachment => attachment;
    }
}
