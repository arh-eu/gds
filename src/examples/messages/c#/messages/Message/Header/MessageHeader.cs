using MsgPack;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace messages
{
    public class MessageHeader : IPackable, IUnpackable
    {
        private string user;
        private string messageId;
        private long createTime;
        private long requestTime;
        private bool isFragmented;
        private bool? firstFragment;
        private bool? lastFragment;
        private int? offset;
        private int? fullDataSize;
        private int dataType;

        public MessageHeader(string user, string messageId, long createTime, long requestTime,
            bool isFragmented, bool? firstFragment, bool? lastFragment, int? offset,
            int? fullDataSize, int dataType)
        {
            this.user = user;
            this.messageId = messageId;
            this.createTime = createTime;
            this.requestTime = requestTime;
            this.isFragmented = isFragmented;
            this.firstFragment = firstFragment;
            this.lastFragment = lastFragment;
            this.offset = offset;
            this.fullDataSize = fullDataSize;
            this.dataType = dataType;
        }

        internal MessageHeader() {}

        public string User => user;
        public string MessageId => messageId;
        public long CreateTime => createTime;
        public long RequestTime => requestTime;
        public bool IsFragmented => isFragmented;
        public bool? FirstFragment => firstFragment;
        public bool? LastFragment => lastFragment;
        public int? Offset => offset;
        public int? FullDataSize => fullDataSize;
        public int DataType => dataType;

        public void PackToMessage(Packer packer, PackingOptions options)
        {
            packer.Pack(user);
            packer.Pack(messageId);
            packer.Pack(createTime);
            packer.Pack(requestTime);
            packer.Pack(isFragmented);
            packer.Pack(firstFragment);
            packer.Pack(lastFragment);
            packer.Pack(offset);
            packer.Pack(fullDataSize);
            packer.Pack(dataType);
        }

        public void UnpackFromMessage(Unpacker unpacker)
        {
            unpacker.ReadString(out user);
            unpacker.ReadString(out messageId);
            unpacker.ReadInt64(out createTime);
            unpacker.ReadInt64(out requestTime);
            unpacker.ReadBoolean(out isFragmented);
            unpacker.ReadNullableBoolean(out firstFragment);
            unpacker.ReadNullableBoolean(out lastFragment);
            unpacker.ReadNullableInt32(out offset);
            unpacker.ReadNullableInt32(out fullDataSize);
            unpacker.ReadInt32(out dataType);
        }
    }
}
