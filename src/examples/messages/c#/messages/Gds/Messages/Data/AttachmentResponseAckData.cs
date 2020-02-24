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
    /// Attachment Response Ack type Data part of the Message
    /// </summary>
    [MessagePackObject]
    public class AttachmentResponseAckData : MessageData
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly AttachmentResponseAckTypeData ackData;

        [Key(2)]
        private readonly string exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentResponseAckData"/> class
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        public AttachmentResponseAckData(StatusCode status, AttachmentResponseAckTypeData ackData, string exception)
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
        public AttachmentResponseAckTypeData AckData => ackData;

        /// <summary>
        /// The description of an error.
        /// </summary>
        [IgnoreMember]
        public string Exception => exception;

        public override DataType GetDataType()
        {
            return DataType.AttachmentResponseAck;
        }

        public override bool IsAttachmentResponseAckData()
        {
            return true;
        }

        public override AttachmentResponseAckData AsAttachmentResponseAckData()
        {
            return this;
        }
    }

    [MessagePackObject]
    public class AttachmentResponseAckTypeData
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly AttachmentResponseAckResult result;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentResponseAckTypeData"/> class
        /// </summary>
        /// <param name="status">The sub-type belonging to the global status.</param>
        /// <param name="result">The acknowledgement descriptor.</param>
        public AttachmentResponseAckTypeData(StatusCode status, AttachmentResponseAckResult result)
        {
            this.status = status;
            this.result = result;
        }

        /// <summary>
        /// The sub-type belonging to the global status.
        /// </summary>
        [IgnoreMember]
        public StatusCode Status => status;

        /// <summary>
        /// The acknowledgement descriptor.
        /// </summary>
        [IgnoreMember]
        public AttachmentResponseAckResult Result => result;
    }

    [MessagePackObject(keyAsPropertyName: true)]
    public class AttachmentResponseAckResult
    {
        private readonly List<string> requestids;
        private readonly string ownertable;
        private readonly string attachmentid;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentResponseAckResult"/> class
        /// </summary>
        /// <param name="requestids">Contains the request message identifier(s).</param>
        /// <param name="ownertable">The table name of the event record that owns the attachment.</param>
        /// <param name="attachmentid">The unique identifier of the attachment.</param>
        public AttachmentResponseAckResult(List<string> requestids, string ownertable, string attachmentid)
        {
            this.requestids = requestids;
            this.ownertable = ownertable;
            this.attachmentid = attachmentid;
        }

        /// <summary>
        /// Contains the request message identifier(s).
        /// </summary>
        [IgnoreMember]
        public List<string> Requestids => requestids;

        /// <summary>
        /// The table name of the event record that owns the attachment.
        /// </summary>
        [IgnoreMember]
        public string Ownertable => ownertable;

        /// <summary>
        /// The unique identifier of the attachment.
        /// </summary>
        [IgnoreMember]
        public string Attachmentid => attachmentid;
    }
}
