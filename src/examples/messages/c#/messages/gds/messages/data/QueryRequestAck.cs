using gds.messages.data;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class QueryRequestAck : Data
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly QueryRequestAckTypeData ackData;

        [Key(2)]
        private readonly string exception;

        public QueryRequestAck(StatusCode status, QueryRequestAckTypeData ackData, string exception)
        {
            this.status = status;
            this.ackData = ackData;
            this.exception = exception;
        }

        [IgnoreMember]
        public StatusCode Status => status;

        [IgnoreMember]
        public QueryRequestAckTypeData AckData => ackData;

        [IgnoreMember]
        public string Exception => exception;

        public override DataType GetDataType()
        {
            return DataType.QueryRequestAck;
        }

        public override bool IsQueryRequestAck()
        {
            return true;
        }

        public override QueryRequestAck AsQueryRequestAck()
        {
            return this;
        }
    }

    [MessagePackObject]
    public class QueryRequestAckTypeData
    {
        [Key(0)]
        private readonly long numberOfHits;

        [Key(1)]
        private readonly long numberOfFilteredHits;

        [Key(2)]
        private readonly bool hasMorePage;

        [Key(3)]
        private readonly QueryContextDescriptor queryContextDescriptor;

        [Key(4)]
        private readonly List<FieldDescriptor> fieldDescriptors;

        [Key(5)]
        private readonly List<List<object>> records;

        public QueryRequestAckTypeData(long numberOfHits, long numberOfFilteredHits, bool hasMorePage, QueryContextDescriptor queryContextDescriptor, 
            List<FieldDescriptor> fieldDescriptors, List<List<object>> records)
        {
            this.numberOfHits = numberOfHits;
            this.numberOfFilteredHits = numberOfFilteredHits;
            this.hasMorePage = hasMorePage;
            this.queryContextDescriptor = queryContextDescriptor;
            this.fieldDescriptors = fieldDescriptors;
            this.records = records;
        }

        [IgnoreMember]
        public long NumberOfHits => numberOfHits;

        [IgnoreMember]
        public long NumberOfFilteredHits => numberOfFilteredHits;

        [IgnoreMember]
        public bool HasMorePage => hasMorePage;

        [IgnoreMember]
        public QueryContextDescriptor QueryContextDescriptor => queryContextDescriptor;

        [IgnoreMember]
        public List<FieldDescriptor> FieldDescriptors => fieldDescriptors;

        [IgnoreMember]
        public List<List<object>> Records => records;
    }

    [MessagePackObject]
    public class QueryContextDescriptor
    {
        [Key(0)]
        private readonly string id;

        [Key(1)]
        private readonly string query;

        [Key(2)]
        private readonly long deliveredNumberOfHits;

        [Key(3)]
        private readonly long queryStartTime;

        [Key(4)]
        private readonly string consistencyType;

        [Key(5)]
        private readonly string lastBucketId;

        [Key(6)]
        private readonly GDSDescriptor gdsDescriptor;

        [Key(7)]
        private readonly List<object> fieldValues;

        [Key(8)]
        private readonly List<string> partitionNames;

        public QueryContextDescriptor(string id, string query, long deliveredNumberOfHits, long queryStartTime, string consistencyType, string lastBucketId,
            GDSDescriptor gdsDescriptor, List<object> fieldValues, List<string> partitionNames)
        {
            this.id = id;
            this.query = query;
            this.deliveredNumberOfHits = deliveredNumberOfHits;
            this.queryStartTime = queryStartTime;
            this.consistencyType = consistencyType;
            this.lastBucketId = lastBucketId;
            this.gdsDescriptor = gdsDescriptor;
            this.fieldValues = fieldValues;
            this.partitionNames = partitionNames;
        }

        [IgnoreMember]
        public string Id => id;

        [IgnoreMember]
        public string Query => query;

        [IgnoreMember]
        public long DeliveredNumberOfHits => deliveredNumberOfHits;

        [IgnoreMember]
        public long QueryStartTime => queryStartTime;

        [IgnoreMember]
        public string ConsistencyType => consistencyType;

        [IgnoreMember]
        public string LastBucketId => lastBucketId;

        [IgnoreMember]
        public GDSDescriptor GdsDescriptor => gdsDescriptor;

        [IgnoreMember]
        public List<object> FieldValues => fieldValues;

        [IgnoreMember]
        public List<string> PartitionNames => partitionNames;
    }

    [MessagePackObject]
    public class GDSDescriptor
    {
        [Key(0)]
        private readonly string clusterName;

        [Key(1)]
        private readonly string nodeName;

        public GDSDescriptor(string clusterName, string nodeName)
        {
            this.clusterName = clusterName;
            this.nodeName = nodeName;
        }
        
        [IgnoreMember]
        public string ClusterName => clusterName;

        [IgnoreMember]
        public string NodeName => nodeName;
    }
}
