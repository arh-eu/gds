using MsgPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace messages
{
    public class MessageDataConnectionAck : MessageData
    {
        private int status;
        private MessageDataConnection ackData;
        private Dictionary<int, string> unauthorizedAckData;
        private string exception;

        public MessageDataConnectionAck(int status, MessageDataConnection ackData, string exception)
        {
            this.status = status;
            this.ackData = ackData;
            this.exception = exception;
        }

        public MessageDataConnectionAck(int status, Dictionary<int, string> unauthorizedAckData, string exception)
        {
            this.status = status;
            this.unauthorizedAckData = unauthorizedAckData;
            this.exception = exception;
        }

        public MessageDataConnectionAck(int status, string exception)
        {
            this.status = status;
            this.exception = exception;
        }

        internal MessageDataConnectionAck() { }

        public int Status => status;

        public MessageDataConnection AckData => ackData;

        public string Exception => exception;

        public override DataType GetMessageDataType()
        {
            return DataType.ConnectionAck;
        }

        public override bool IsConnectionAckMessageData()
        {
            return true;
        }

        public override MessageDataConnectionAck AsConnectionAckMessageData()
        {
            return this;
        }


        public override void PackToMessage(Packer packer, PackingOptions options)
        {
            packer.PackArrayHeader(3);
            packer.Pack(status);
            if (ackData != null)
            {
                ackData.PackToMessage(packer, null);
            } 
            else if(unauthorizedAckData != null)
            {
                packer.Pack(unauthorizedAckData);
            }
            else
            {
                packer.PackNull();
            }
            packer.Pack(exception);
        }

        public override void UnpackFromMessage(Unpacker unpacker)
        {
            unpacker.ReadInt32(out status);
            unpacker.Read();
            if(unpacker.IsArrayHeader)
            {
                ackData = new MessageDataConnection();
                ackData.UnpackFromMessage(unpacker);
            }
            else if(unpacker.IsMapHeader)
            {
                unauthorizedAckData = new Dictionary<int, string>();
                long mapLength = unpacker.ItemsCount;
                for(int i = 0; i < mapLength; ++i)
                {
                    int key;
                    string value;
                    unpacker.ReadInt32(out key);
                    unpacker.ReadString(out value);
                    unauthorizedAckData.Add(key, value);
                }
            } 
            else
            {
                unpacker.Read();
            }
            unpacker.ReadString(out exception);
        }

        public override string ToString()
        {
            return String.Format("[status={0},ackData={1},unauthorizedAckData={2},exception={3}]", 
                status, ackData == null ? "null" : ackData.ToString(), unauthorizedAckData == null ? "null" : unauthorizedAckData.ToString(), exception == null ? "null" : exception);
        }
    }
}
