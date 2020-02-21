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

using gds.message.data;
using gds.message.header;

namespace gds.message
{
    /// <summary>
    /// Message with the Header and Data parts
    /// </summary>
    public class Message
    {
        private readonly Header header;
        private readonly Data data;

        public Message(Header header, Data data)
        {
            this.header = header;
            this.data = data;
        }

        /// <summary>
        /// Header part of the Message
        /// </summary>
        public Header Header => header;

        /// <summary>
        /// Data part of the Message
        /// </summary>
        public Data Data => data;
    }
}
