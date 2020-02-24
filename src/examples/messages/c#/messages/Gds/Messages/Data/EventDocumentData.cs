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
using System.Collections.Generic;

namespace Gds.Messages.Data
{
    /// <summary>
    /// Event Document type Data part of the Message
    /// </summary>
    [MessagePackObject]
    public class EventDocument : MessageData
    {
        [Key(0)]
        private readonly string tableName;

        [Key(1)]
        private readonly List<FieldDescriptor> fieldDescriptors;

        [Key(2)]
        private readonly List<List<object>> records;

        [Key(3)]
        private readonly Dictionary<int, string[]> returningOptions = new Dictionary<int, string[]>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDocument"/> class
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="fieldDescriptors">The field descriptors.</param>
        /// <param name="records">The records.</param>
        public EventDocument(string tableName, List<FieldDescriptor> fieldDescriptors, List<List<object>> records)
        {
            this.tableName = tableName;
            this.fieldDescriptors = fieldDescriptors;
            this.records = records;
        }

        /// <summary>
        /// The name of the table.
        /// </summary>
        [IgnoreMember]
        public string TableName => tableName;

        /// <summary>
        /// The field descriptors.
        /// </summary>
        [IgnoreMember]
        public List<FieldDescriptor> FieldDescriptors => fieldDescriptors;

        /// <summary>
        /// The records.
        /// </summary>
        [IgnoreMember]
        public List<List<object>> Records => records;

        public override DataType GetDataType()
        {
            return DataType.EventDocument;
        }

        public override bool IsEventDocumentData()
        {
            return true;
        }

        public override EventDocument AsEventDocumentData()
        {
            return this;
        }
    }
}
