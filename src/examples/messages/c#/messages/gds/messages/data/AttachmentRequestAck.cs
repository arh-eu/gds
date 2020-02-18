using gds.messages.data;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class AttachmentRequestAck : Data
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly AttachmentRequestAckTypeData ackData;

        [Key(2)]
        private readonly string exception;

        public AttachmentRequestAck(StatusCode status, AttachmentRequestAckTypeData ackData, string exception)
        {
            this.status = status;
            this.ackData = ackData;
            this.exception = exception;
        }

        [IgnoreMember]
        public StatusCode Status => status;

        [IgnoreMember]
        public AttachmentRequestAckTypeData AckData => ackData;

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

        public override AttachmentRequestAck AsAttachmentRequestAckData()
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

        public AttachmentRequestAckTypeData(StatusCode status, AttachmentResult result, long? remainedWaitTimeMillis)
        {
            this.status = status;
            this.result = result;
            this.remainedWaitTimeMillis = remainedWaitTimeMillis;
        }

        [IgnoreMember]
        public StatusCode Status => status;

        [IgnoreMember]
        public AttachmentResult Result => result;

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

        [IgnoreMember]
        public List<string> RequestIds => requestids;

        [IgnoreMember]
        public string OwnerTable => ownertable;

        [IgnoreMember]
        public string AttachmentId => attachmentid;

        [IgnoreMember]
        public List<string> OwnerIds => ownerids;

        [IgnoreMember]
        public string Meta => meta;

        [IgnoreMember]
        public long? Ttl => ttl;

        [IgnoreMember]
        public long? ToValid => to_valid;

        [IgnoreMember]
        public byte[] Attachment => attachment;
    }
}
