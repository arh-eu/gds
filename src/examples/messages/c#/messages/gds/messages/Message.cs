using MessagePack;
using gds.messages.data;
using gds.messages.header;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages
{
    /// <summary>
    /// Class <c>Message</c> with the <c>Header</c> and <c>Data</c> parts
    /// </summary>
    public class Message
    {
        private readonly Header header;
        private readonly Data data;

        /// <summary>
        /// This constructor initializes a new Message with the <paramref name="header"/> and <paramref name="data"/> parts.
        /// </summary>
        /// <param name="header">Header part of the Message</param>
        /// <param name="data">Data part of the Message</param>
        public Message(Header header, Data data)
        {
            this.header = header;
            this.data = data;
        }

        /// <summary>
        /// <c>Header</c> part of the Message
        /// </summary>
        public Header Header => header;

        /// <summary>
        /// <c>Data</c> part of the Message
        /// </summary>
        public Data Data => data;
    }
}
