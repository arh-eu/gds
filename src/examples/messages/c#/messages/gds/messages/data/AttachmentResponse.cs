using gds.messages.data;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class AttachmentResponse : Data
    {
        [Key(0)]
        private readonly AttachmentResult result;

        public AttachmentResponse(AttachmentResult result)
        {
            this.result = result;
        }

        [IgnoreMember]
        public AttachmentResult Result => result;

        public override DataType GetDataType()
        {
            return DataType.AttachmentResponse;
        }

        public override bool IsAttachmentResponseData()
        {
            return true;
        }

        public override AttachmentResponse AsAttachmentResponseData()
        {
            return this;
        }
    }
}
