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
using MessagePack.Formatters;
using System;

namespace gds.message.data
{
    /// <summary>
    /// QueryRequest type data part of the Message
    /// </summary>
    public class QueryRequest : Data
    {
        private readonly string selectStringBlock;
        private readonly ConsistencyType consistencyType;
        private readonly long timeout;
        private readonly int? queryPageSize;
        private readonly QueryType? queryType;

        public QueryRequest(string selectStringBlock, ConsistencyType consistencyType, long timeout, int queryPageSize, QueryType? queryType)
        {
            this.selectStringBlock = selectStringBlock;
            this.consistencyType = consistencyType;
            this.timeout = timeout;
            this.queryPageSize = queryPageSize;
            this.queryType = queryType;
        }

        public QueryRequest(string selectStringBlock, ConsistencyType consistencyType, long timeout)
        {
            this.selectStringBlock = selectStringBlock;
            this.consistencyType = consistencyType;
            this.timeout = timeout;
            this.queryPageSize = null;
            this.queryType = null;
        }

        /// <summary>
        /// The SELECT statement.
        /// </summary>
        public string SelectStringBlock => selectStringBlock;

        /// <summary>
        /// The consistency type used for the query.
        /// </summary>
        public ConsistencyType ConsistencyType => consistencyType;

        /// <summary>
        /// The timeout value in milliseconds.
        /// </summary>
        public long Timeout => timeout;

        /// <summary>
        /// The number of records per page.
        /// </summary>
        public int? QueryPageSize => queryPageSize;

        /// <summary>
        /// The query type
        /// </summary>
        public QueryType? QueryType => queryType;

        public override DataType GetDataType()
        {
            return DataType.QueryRequest;
        }

        public override bool IsQueryRequest()
        {
            return true;
        }

        public override QueryRequest AsQueryRequest()
        {
            return this;
        }
    }

    public enum ConsistencyType
    {
        PAGE,
        PAGES,
        NONE
    }

    public enum QueryType
    {
        PAGE = 0,
        SCROLL = 1
    }

    public class QueryRequestFormatter : IMessagePackFormatter<QueryRequest>
    {
        public void Serialize(ref MessagePackWriter writer, QueryRequest value, MessagePackSerializerOptions options)
        {
            if (value.QueryPageSize != null && value.QueryType != null)
            {
                writer.WriteArrayHeader(5);
            }
            else
            {
                writer.WriteArrayHeader(3);
            }
            writer.Write(value.SelectStringBlock);
            writer.Write(value.ConsistencyType.ToString());
            writer.WriteInt64(value.Timeout);
            if (value.QueryPageSize != null)
            {
                writer.Write(value.QueryPageSize.Value);
            }
            if (value.QueryType != null)
            {
                writer.Write((int)value.QueryType.Value);
            }
        }

        public QueryRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
