using MsgPack;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace messages
{
    public class Message : IPackable
    {
        private readonly MessageHeader header;
        private readonly MessageData data;

        public Message(MessageHeader header, MessageData data)
        {
            this.header = header;
            this.data = data;
        }

        public MessageHeader Header => header;
        public MessageData Data => data;

        public void PackToMessage(Packer packer, PackingOptions options)
        {
            packer.PackArrayHeader(11);
            header.PackToMessage(packer, null);
            data.PackToMessage(packer, null);
        }
    }
}
