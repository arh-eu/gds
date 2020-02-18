using gds.messages.data;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class NextQueryPageRequest : Data
    {
        [Key(0)]
        private readonly QueryContextDescriptor queryContextDescriptor;

        [Key(1)]
        private readonly long timeout;

        public NextQueryPageRequest(QueryContextDescriptor queryContextDescriptor, long timeout)
        {
            this.queryContextDescriptor = queryContextDescriptor;
            this.timeout = timeout;
        }

        [IgnoreMember]
        public QueryContextDescriptor QueryContextDescriptor => queryContextDescriptor;

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

        public override NextQueryPageRequest AsNextQueryPageRequest()
        {
            return this;
        }
    }
}
