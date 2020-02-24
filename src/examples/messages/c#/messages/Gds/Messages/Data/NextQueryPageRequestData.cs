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
    /// Next Query Page Request type Data part of the Message
    /// </summary>
    [MessagePackObject]
    public class NextQueryPageRequestData : MessageData
    {
        [Key(0)]
        private readonly QueryContextDescriptor queryContextDescriptor;

        [Key(1)]
        private readonly long timeout;

        /// <summary>
        /// Initializes a new instance of the <see cref="NextQueryPageRequestData"/> class
        /// </summary>
        /// <param name="queryContextDescriptor">Query status descriptor for querying the next pages.</param>
        /// <param name="timeout">The timeout value in milliseconds.</param>
        public NextQueryPageRequestData(QueryContextDescriptor queryContextDescriptor, long timeout)
        {
            this.queryContextDescriptor = queryContextDescriptor;
            this.timeout = timeout;
        }

        /// <summary>
        /// Query status descriptor for querying the next pages.
        /// </summary>
        [IgnoreMember]
        public QueryContextDescriptor QueryContextDescriptor => queryContextDescriptor;

        /// <summary>
        /// The timeout value in milliseconds.
        /// </summary>
        [IgnoreMember]
        public long Timeout => timeout;

        public override DataType GetDataType()
        {
            return DataType.NextQueryPageRequest;
        }

        public override bool IsNextQueryPageRequest()
        {
            return true;
        }

        public override NextQueryPageRequestData AsNextQueryPageRequest()
        {
            return this;
        }
    }
}
