using MessagePack;
using gds.messages.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.header
{
    /// <summary>
    /// The <c>Header</c> part of the <c>Message</c>
    /// </summary>
    public class Header
    {
        private readonly string userName;
        private readonly string messageId;
        private readonly long createTime;
        private readonly long requestTime;
        private readonly bool isFragmented;
        private readonly bool? firstFragment;
        private readonly bool? lastFragment;
        private readonly int? offset;
        private readonly int? fullDataSize;
        private readonly DataType dataType;

        /// <summary>
        /// This constructor initializes a new Header
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="messageId"></param>
        /// <param name="createTime"></param>
        /// <param name="requestTime"></param>
        /// <param name="isFragmented"></param>
        /// <param name="firstFragment"></param>
        /// <param name="lastFragment"></param>
        /// <param name="offset"></param>
        /// <param name="fullDataSize"></param>
        /// <param name="dataType"></param>
        public Header(string userName, string messageId, long createTime, long requestTime,
            bool isFragmented, bool? firstFragment, bool? lastFragment, int? offset, int? fullDataSize, DataType dataType)
        {
            this.userName = userName;
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

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string UserName => userName;

        /// <summary>
        /// The identifier of the message, with which the request can be associated.
        /// </summary>
        public string MessageId => messageId;

        /// <summary>
        /// The time of creating the message, epoch timestamp.
        /// </summary>
        public long CreateTime => createTime;

        /// <summary>
        /// The time of the request, epoch timestamp.
        /// </summary>
        public long RequestTime => requestTime;

        /// <summary>
        /// Whether the current message is fragmented or not.
        /// </summary>
        public bool IsFragmented => isFragmented;

        /// <summary>
        /// Whether it's the first fragment of a fragmented message.
        /// </summary>
        public bool? FirstFragment => firstFragment;

        /// <summary>
        /// Whether it's the last fragment of a fragmented message.
        /// </summary>
        public bool? LastFragment => lastFragment;

        /// <summary>
        /// If the <c>IsFragmented</c> value of the message is false and this is a chunk acknowledgement, 
        /// it's content determines the last byte of the original, fragmented message the receiver has yet received.
        /// If the <c>IsFragmented</c> is true, then it is a chunk (which can’t be an acknowledgement), 
        /// so it specifies the last byte of the of the original message that has yet been forwarded.
        /// </summary>
        public int? Offset => offset;

        /// <summary>
        /// The size of the full data in bytes.
        /// </summary>
        public int? FullDataSize => fullDataSize;

        /// <summary>
        /// It specifies what types of information the data carries.
        /// </summary>
        public DataType DataType => dataType;
    }
}
