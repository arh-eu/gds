using gds.messages.data;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class Event : Data
    {
        [Key(0)]
        private readonly string operationsStringBlock;

        [Key(1)]
        private readonly Dictionary<string, byte[]> binaryContentsMapping;

        [Key(2)]
        private readonly List<List<Dictionary<int, bool>>> executionPriorityStructure;

        public Event(string operationsStringBlock, Dictionary<string, byte[]> binaryContentsMapping, List<List<Dictionary<int, bool>>> executionPriorityStructure)
        {
            this.operationsStringBlock = operationsStringBlock;
            this.binaryContentsMapping = binaryContentsMapping;
            this.executionPriorityStructure = executionPriorityStructure;
        }

        [IgnoreMember]
        public String OperationsStringBlock => operationsStringBlock;

        [IgnoreMember]
        public Dictionary<string, byte[]> BinaryContentsMapping => binaryContentsMapping;

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

        public override Event AsEventData()
        {
            return this;
        }
    }
}
