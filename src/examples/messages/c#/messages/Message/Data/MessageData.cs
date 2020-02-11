using MsgPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace messages
{
    public enum DataType
    {
        Connection = 0,
        ConnectionAck = 1,
        Event = 2,
        EventAck = 3,
        AttachmentRequest = 4,
        AttachmentRequestAck = 5,
        AttachmentResponse = 6,
        AttachmentResponseAck = 7,
        EventDocument = 8,
        EventDocumentAck = 9,
        QueryRequest = 10,
        QueryRequestAck = 11,
        NextQueryPageRequest = 12
    }

    public abstract class MessageData : IPackable, IUnpackable
    {
        public abstract DataType GetMessageDataType();

        public virtual bool IsConnectionMessageData()
        {
            return false;
        }

        public virtual bool IsConnectionAckMessageData()
        {
            return false;
        }

        public virtual MessageDataConnection AsConnectionMessageData()
        {
            throw new InvalidCastException();
        }

        public virtual MessageDataConnectionAck AsConnectionAckMessageData()
        {
            throw new InvalidCastException();
        }

        public abstract void PackToMessage(Packer packer, PackingOptions options);

        public abstract void UnpackFromMessage(Unpacker unpacker);
    }
}
