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
using System;
using System.Collections.Generic;

namespace Gds.Messages.Data
{
    /// <summary>
    /// Event type Data part of the Message
    /// </summary>
    [MessagePackObject]
    public class EventData : MessageData
    {
        [Key(0)]
        private readonly string operationsStringBlock;

        [Key(1)]
        private readonly Dictionary<string, byte[]> binaryContentsMapping;

        [Key(2)]
        private readonly List<List<Dictionary<int, bool>>> executionPriorityStructure;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventData"/> class
        /// </summary>
        /// <param name="operationsStringBlock">The operations in standard SQL statements, separated with ';' characters.</param>
        /// <param name="binaryContentsMapping">The mapping of the binary contents.</param>
        /// <param name="executionPriorityStructure">The execution priority structure.</param>
        public EventData(string operationsStringBlock, Dictionary<string, byte[]> binaryContentsMapping, List<List<Dictionary<int, bool>>> executionPriorityStructure)
        {
            this.operationsStringBlock = operationsStringBlock;
            this.binaryContentsMapping = binaryContentsMapping;
            this.executionPriorityStructure = executionPriorityStructure;
        }

        /// <summary>
        /// The operations in standard SQL statements, separated with ';' characters.
        /// </summary>
        [IgnoreMember]
        public String OperationsStringBlock => operationsStringBlock;

        /// <summary>
        /// The mapping of the binary contents.
        /// </summary>
        [IgnoreMember]
        public Dictionary<string, byte[]> BinaryContentsMapping => binaryContentsMapping;

        /// <summary>
        /// The execution priority structure.
        /// </summary>
        [IgnoreMember]
        public List<List<Dictionary<int, bool>>> ExecutionPriorityStructure => executionPriorityStructure;

        public override DataType GetDataType()
        {
            return DataType.Event;
        }

        public override bool IsEventData()
        {
            return true;
        }

        public override EventData AsEventData()
        {
            return this;
        }
    }
}
