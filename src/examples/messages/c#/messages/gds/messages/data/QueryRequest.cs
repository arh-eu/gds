using gds.messages.data;
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    public class QueryRequest : Data
    {
        private readonly string selectStringBlock;
        private readonly string consistencyType;
        private readonly long timeout;
        private readonly int? queryPageSize;
        private readonly int? queryType;

        public QueryRequest(string selectStringBlock, string consistencyType, long timeout, int queryPageSize, int queryType)
        {
            this.selectStringBlock = selectStringBlock;
            this.consistencyType = consistencyType;
            this.timeout = timeout;
            this.queryPageSize = queryPageSize;
            this.queryType = queryType;
        }

        public QueryRequest(string selectStringBlock, string consistencyType, long timeout)
        {
            this.selectStringBlock = selectStringBlock;
            this.consistencyType = consistencyType;
            this.timeout = timeout;
            this.queryPageSize = null;
            this.queryType = null;
        }

        public string SelectStringBlock => selectStringBlock;

        public string ConsistencyType => consistencyType;

        public long Timeout => timeout;

        public int? QueryPageSize => queryPageSize;

        public int? QueryType => queryType;

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

    public class QueryRequestFormatter : IMessagePackFormatter<QueryRequest>
    {
        public void Serialize(ref MessagePackWriter writer, QueryRequest value, MessagePackSerializerOptions options)
        {
            if(value.QueryPageSize != null && value.QueryType != null)
            {
                writer.WriteArrayHeader(5);
            } 
            else
            {
                writer.WriteArrayHeader(3);
            }
            writer.Write(value.SelectStringBlock);
            writer.Write(value.ConsistencyType);
            writer.WriteInt64(value.Timeout);
            if(value.QueryPageSize != null)
            {
                writer.Write(value.QueryPageSize.Value);
            }
            if(value.QueryType != null)
            {
                writer.Write(value.QueryType.Value);
            }
        }

        public QueryRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
