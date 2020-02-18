using gds.messages.data;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class EventAck : Data
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly List<OperationResponse> ackData;

        [Key(2)]
        private readonly string exception;

        public EventAck(StatusCode status, List<OperationResponse> ackData, string exception)
        {
            this.status = status;
            this.ackData = ackData;
            this.exception = exception;
        }

        [IgnoreMember]
        public StatusCode Status => status;

        [IgnoreMember]
        public List<OperationResponse> AckData => ackData;

        [IgnoreMember]
        public string Exception => exception;

        public override DataType GetDataType()
        {
            return DataType.EventAck;
        }

        public override bool IsEventAckData()
        {
            return true;
        }

        public override EventAck AsEventAckData()
        {
            return this;
        }
    }

    [MessagePackObject]
    public class OperationResponse
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly string notification;

        [Key(2)]
        private readonly List<FieldDescriptor> fieldDescriptors;

        [Key(3)]
        private readonly List<SubResult> subResults;

        public OperationResponse(StatusCode status, string notification, List<FieldDescriptor> fieldDescriptors, List<SubResult> subResults)
        {
            this.status = status;
            this.notification = notification;
            this.fieldDescriptors = fieldDescriptors;
            this.subResults = subResults;
        }

        [IgnoreMember]
        public StatusCode Status => status;

        [IgnoreMember]
        public string Notification => notification;

        [IgnoreMember]
        public List<FieldDescriptor> FieldDescriptors => fieldDescriptors;

        [IgnoreMember]
        public List<SubResult> SubResults => subResults;
    }

        [MessagePackObject]
    public class FieldDescriptor
    {
        [Key(0)]
        private readonly string fieldName;

        [Key(1)]
        private readonly string fieldType;

        [Key(2)]
        private readonly string mimeType;

        public FieldDescriptor(string fieldName, string fieldType, string mimeType)
        {
            this.fieldName = fieldName;
            this.fieldType = fieldType;
            this.mimeType = mimeType;
        }

        [IgnoreMember]
        public string FieldName => fieldName;

        [IgnoreMember]
        public string FieldType => fieldType;

        [IgnoreMember]
        public string MimeType => mimeType;
    }

    [MessagePackObject]
    public class SubResult
    {
        [Key(0)]
        private readonly StatusCode subStatus;

        [Key(1)]
        private readonly string id;

        [Key(2)]
        private readonly string tableName;

        [Key(3)]
        private readonly bool? created;

        [Key(4)]
        private readonly int? version;

        [Key(5)]
        private readonly List<object> returningRecordValues;

        public SubResult(StatusCode subStatus, string id, string tableName, bool? created, int? version, List<object> returningRecordValues)
        {
            this.subStatus = subStatus;
            this.id = id;
            this.tableName = tableName;
            this.created = created;
            this.version = version;
            this.returningRecordValues = returningRecordValues;
        }

        [IgnoreMember]
        public StatusCode SubStatus => subStatus;

        [IgnoreMember]
        public string Id => id;

        [IgnoreMember]
        public string TableName => tableName;

        [IgnoreMember]
        public bool? Created => created;

        [IgnoreMember]
        public int? Version => version;

        [IgnoreMember]
        public List<object> ReturningRecordValues => returningRecordValues;
    }
}
