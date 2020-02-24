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
    /// Query Request Ack type Data part of the Message
    /// </summary>
    [MessagePackObject]
    public class QueryRequestAckData : MessageData
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly QueryRequestAckTypeData ackData;

        [Key(2)]
        private readonly string exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRequestAckData"/> class
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The sucess response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        public QueryRequestAckData(StatusCode status, QueryRequestAckTypeData ackData, string exception)
        {
            this.status = status;
            this.ackData = ackData;
            this.exception = exception;
        }

        /// <summary>
        /// The status incorporates a global signal regarding the response.
        /// </summary>
        [IgnoreMember]
        public StatusCode Status => status;

        /// <summary>
        /// The sucess response object belonging to the acknowledgement.
        /// </summary>
        [IgnoreMember]
        public QueryRequestAckTypeData AckData => ackData;

        /// <summary>
        /// The description of an error.
        /// </summary>
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

        public override QueryRequestAckData AsQueryRequestAck()
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

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRequestAckTypeData"/> class
        /// </summary>
        /// <param name="numberOfHits">This many hits have been returned in this page.</param>
        /// <param name="numberOfFilteredHits">This many records have been filtered from the original result by the lists.</param>
        /// <param name="hasMorePage">Its value is true if the result has not fitted into this page, meaning that there are more records.</param>
        /// <param name="queryContextDescriptor">Query status descriptor for querying the next pages.</param>
        /// <param name="fieldDescriptors">The field descriptors.</param>
        /// <param name="records">The field values of the query result in the sort order.</param>
        public QueryRequestAckTypeData(long numberOfHits, long numberOfFilteredHits, bool hasMorePage, QueryContextDescriptor queryContextDescriptor, List<FieldDescriptor> fieldDescriptors, List<List<object>> records)
        {
            this.numberOfHits = numberOfHits;
            this.numberOfFilteredHits = numberOfFilteredHits;
            this.hasMorePage = hasMorePage;
            this.queryContextDescriptor = queryContextDescriptor;
            this.fieldDescriptors = fieldDescriptors;
            this.records = records;
        }

        /// <summary>
        /// This many hits have been returned in this page.
        /// </summary>
        [IgnoreMember]
        public long NumberOfHits => numberOfHits;

        /// <summary>
        /// This many records have been filtered from the original result by the lists. 
        /// </summary>
        [IgnoreMember]
        public long NumberOfFilteredHits => numberOfFilteredHits;

        /// <summary>
        /// Its value is true if the result has not fitted into this page, meaning that there are more records.
        /// </summary>
        [IgnoreMember]
        public bool HasMorePage => hasMorePage;

        /// <summary>
        /// Query status descriptor for querying the next pages.
        /// </summary>
        [IgnoreMember]
        public QueryContextDescriptor QueryContextDescriptor => queryContextDescriptor;

        /// <summary>
        /// The field descriptors.
        /// </summary>
        [IgnoreMember]
        public List<FieldDescriptor> FieldDescriptors => fieldDescriptors;

        /// <summary>
        /// The field values of the query result in the sort order.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryContextDescriptor"/> class
        /// </summary>
        /// <param name="id">Uniquely identifies the query within the entire system.</param>
        /// <param name="query">The original SELECT query sent by the user.</param>
        /// <param name="deliveredNumberOfHits">This many records have been forwarded yet to the requester client.</param>
        /// <param name="queryStartTime">A millisecond based epoch timestamp which is important at NONE consistency.</param>
        /// <param name="consistencyType">The consistency type.</param>
        /// <param name="lastBucketId">The bucket id of the last record sent in the page.</param>
        /// <param name="gdsDescriptor">The descriptor of the GDS Node serving the request.</param>
        /// <param name="fieldValues">The field values (included in the sorting condition) of the last record sent in the page.</param>
        /// <param name="partitionNames">The GDS has not sent data from these partitions yet.</param>
        public QueryContextDescriptor(string id, string query, long deliveredNumberOfHits, long queryStartTime, string consistencyType, string lastBucketId, GDSDescriptor gdsDescriptor, List<object> fieldValues, List<string> partitionNames)
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

        /// <summary>
        /// Uniquely identifies the query within the entire system.
        /// </summary>
        [IgnoreMember]
        public string Id => id;

        /// <summary>
        /// The original SELECT query sent by the user.
        /// </summary>
        [IgnoreMember]
        public string Query => query;

        /// <summary>
        /// This many records have been forwarded yet to the requester client.
        /// </summary>
        [IgnoreMember]
        public long DeliveredNumberOfHits => deliveredNumberOfHits;

        /// <summary>
        /// A millisecond based epoch timestamp which is important at NONE consistency.
        /// </summary>
        [IgnoreMember]
        public long QueryStartTime => queryStartTime;

        /// <summary>
        /// The consistency type.
        /// </summary>
        [IgnoreMember]
        public string ConsistencyType => consistencyType;

        /// <summary>
        /// The bucket id of the last record sent in the page.
        /// </summary>
        [IgnoreMember]
        public string LastBucketId => lastBucketId;

        /// <summary>
        /// The descriptor of the GDS Node serving the request.
        /// </summary>
        [IgnoreMember]
        public GDSDescriptor GdsDescriptor => gdsDescriptor;

        /// <summary>
        /// The field values (included in the sorting condition) of the last record sent in the page.
        /// </summary>
        [IgnoreMember]
        public List<object> FieldValues => fieldValues;

        /// <summary>
        /// The GDS has not sent data from these partitions yet.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="GDSDescriptor"/> class
        /// </summary>
        /// <param name="clusterName">The GDS cluster name.</param>
        /// <param name="nodeName">The GDS cluster node.</param>
        public GDSDescriptor(string clusterName, string nodeName)
        {
            this.clusterName = clusterName;
            this.nodeName = nodeName;
        }

        /// <summary>
        /// The GDS cluster name.
        /// </summary>
        [IgnoreMember]
        public string ClusterName => clusterName;

        /// <summary>
        /// The GDS cluster node.
        /// </summary>
        [IgnoreMember]
        public string NodeName => nodeName;
    }
}
