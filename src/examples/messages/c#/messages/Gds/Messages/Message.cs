/*
 * Copyright 2020 ARH Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Gds.Messages.Data;
using Gds.Messages.Header;

namespace Gds.Messages
{
    /// <summary>
    /// Message with the Header and Data parts
    /// </summary>
    public class Message
    {
        private readonly MessageHeader header;
        private readonly MessageData data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class with the <see cref="MessageHeader"/> and <see cref="MessageData"/> parts
        /// </summary>
        /// <param name="header">The Header part of the Message</param>
        /// <param name="data">The Data part of the Message</param>
        public Message(MessageHeader header, MessageData data)
        {
            this.header = header;
            this.data = data;
        }

        /// <summary>
        /// The Header part of the Message
        /// </summary>
        public MessageHeader Header => header;

        /// <summary>
        /// The Data part of the Message
        /// </summary>
        public MessageData Data => data;
    }
}
