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

using MessagePack;

namespace Gds.Messages.Data
{
    /// <summary>
    /// Attachment Response type Data part of the Message
    /// </summary>
    [MessagePackObject]
    public class AttachmentResponseData : MessageData
    {
        [Key(0)]
        private readonly AttachmentResult result;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentResponseData"/> class
        /// </summary>
        /// <param name="result">The description of the result.</param>
        public AttachmentResponseData(AttachmentResult result)
        {
            this.result = result;
        }

        /// <summary>
        /// The description of the result.
        /// </summary>
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

        public override AttachmentResponseData AsAttachmentResponseData()
        {
            return this;
        }
    }
}
