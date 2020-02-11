using MsgPack;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace messages
{
    class MessageSerializer : MessagePackSerializer<Message>
    {
        public MessageSerializer(SerializationContext ownerContext) : base(ownerContext) { }

        protected override void PackToCore(Packer packer, Message objectTree)
        {
            objectTree.PackToMessage(packer, null);
        }

        protected override Message UnpackFromCore(Unpacker unpacker)
        {
            MessageHeader header = new MessageHeader();
            header.UnpackFromMessage(unpacker);
            MessageData data;
            
            switch(header.DataType)
            {
                case 0:
                    data = new MessageDataConnection();
                    break;
                case 1:
                    data = new MessageDataConnectionAck();
                    break;
                default:
                    data = null;
                    break;
            }

            unpacker.Read();
            data.UnpackFromMessage(unpacker);
            return new Message(header, data);
        }
    }
}
