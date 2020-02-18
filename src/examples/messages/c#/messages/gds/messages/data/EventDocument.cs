using gds.messages.data;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class EventDocument : Data
    {
        [Key(0)]
        private readonly string tableName;

        [Key(1)]
        private readonly List<FieldDescriptor> fieldDescriptors;

        [Key(2)]
        private readonly List<List<object>> records;

        [Key(3)]
        private readonly Dictionary<int, string[]> returningOptions = new Dictionary<int, string[]>();

        public EventDocument(string tableName, List<FieldDescriptor> fieldDescriptors, List<List<object>> records)
        {
            this.tableName = tableName;
            this.fieldDescriptors = fieldDescriptors;
            this.records = records;
        }

        [IgnoreMember]
        public string TableName => tableName;

        [IgnoreMember]
        public List<FieldDescriptor> FieldDescriptors => fieldDescriptors;

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
