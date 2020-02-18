using gds.messages.data;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class AttachmentResponseAck : Data
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly AttachmentResponseAckTypeData ackData;

        [Key(2)]
        private readonly string exception;

        public AttachmentResponseAck(StatusCode status, AttachmentResponseAckTypeData ackData, string exception)
        {
            this.status = status;
            this.ackData = ackData;
            this.exception = exception;
        }

        [IgnoreMember]
        public StatusCode Status => status;

        [IgnoreMember]
        public AttachmentResponseAckTypeData AckData => ackData;

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

        public override AttachmentResponseAck AsAttachmentResponseAckData()
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

        public AttachmentResponseAckTypeData(StatusCode status, AttachmentResponseAckResult result)
        {
            this.status = status;
            this.result = result;
        }

        [IgnoreMember]
        public StatusCode Status => status;

        [IgnoreMember]
        public AttachmentResponseAckResult Result => result;
    }

    [MessagePackObject(keyAsPropertyName: true)]
    public class AttachmentResponseAckResult
    {
        private readonly List<string> requestids;
        private readonly string ownertable;
        private readonly string attachmentid;

        public AttachmentResponseAckResult(List<string> requestids, string ownertable, string attachmentid)
        {
            this.requestids = requestids;
            this.ownertable = ownertable;
            this.attachmentid = attachmentid;
        }

        [IgnoreMember]
        public List<string> Requestids => requestids;

        [IgnoreMember]
        public string Ownertable => ownertable;

        [IgnoreMember]
        public string Attachmentid => attachmentid;
    }
}
