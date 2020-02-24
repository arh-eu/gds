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
    /// Event Ack type Data part of the Message
    /// </summary>
    [MessagePackObject]
    public class EventAckData : MessageData
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly List<OperationResponse> ackData;

        [Key(2)]
        private readonly string exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventAckData"/> class
        /// </summary>
        /// <param name="status">The status incorporates a global signal regarding the response.</param>
        /// <param name="ackData">The sucess response object belonging to the acknowledgement.</param>
        /// <param name="exception">The description of an error.</param>
        public EventAckData(StatusCode status, List<OperationResponse> ackData, string exception)
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
        public List<OperationResponse> AckData => ackData;

        /// <summary>
        /// The description of an error.
        /// </summary>
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

        public override EventAckData AsEventAckData()
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

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResponse"/> class
        /// </summary>
        /// <param name="status">The status of the operation.</param>
        /// <param name="notification">The description of the error in case of one.</param>
        /// <param name="fieldDescriptors">The field descriptors if the requester demanded a response with specifying the RETURNING clause.</param>
        /// <param name="subResults">Contains the results of each sub-operation.</param>
        public OperationResponse(StatusCode status, string notification, List<FieldDescriptor> fieldDescriptors, List<SubResult> subResults)
        {
            this.status = status;
            this.notification = notification;
            this.fieldDescriptors = fieldDescriptors;
            this.subResults = subResults;
        }

        /// <summary>
        /// The status of the operation.
        /// </summary>
        [IgnoreMember]
        public StatusCode Status => status;

        /// <summary>
        /// The description of the error in case of one.
        /// </summary>
        [IgnoreMember]
        public string Notification => notification;

        /// <summary>
        /// The field descriptors if the requester demanded a response with specifying the RETURNING clause.
        /// </summary>
        [IgnoreMember]
        public List<FieldDescriptor> FieldDescriptors => fieldDescriptors;

        /// <summary>
        /// Contains the results of each sub-operation.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDescriptor"/> class
        /// </summary>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="fieldType">The type of the field.</param>
        /// <param name="mimeType">The mime type of the field.</param>
        public FieldDescriptor(string fieldName, string fieldType, string mimeType)
        {
            this.fieldName = fieldName;
            this.fieldType = fieldType;
            this.mimeType = mimeType;
        }

        /// <summary>
        /// The name of the field.
        /// </summary>
        [IgnoreMember]
        public string FieldName => fieldName;

        /// <summary>
        /// The type of the field.
        /// </summary>
        [IgnoreMember]
        public string FieldType => fieldType;

        /// <summary>
        /// The mime type of the field.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SubResult"/> class
        /// </summary>
        /// <param name="subStatus">The status code of the sub-operation.</param>
        /// <param name="id">The unique identifier of the record.</param>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="created">Specifies whether the record was created at database level during the operation.</param>
        /// <param name="version">The version number of the record. </param>
        /// <param name="returningRecordValues">The block containing the results requested in the RETURNING clause.</param>
        public SubResult(StatusCode subStatus, string id, string tableName, bool? created, int? version, List<object> returningRecordValues)
        {
            this.subStatus = subStatus;
            this.id = id;
            this.tableName = tableName;
            this.created = created;
            this.version = version;
            this.returningRecordValues = returningRecordValues;
        }

        /// <summary>
        /// The status code of the sub-operation.
        /// </summary>
        [IgnoreMember]
        public StatusCode SubStatus => subStatus;

        /// <summary>
        /// The unique identifier of the record.
        /// </summary>
        [IgnoreMember]
        public string Id => id;

        /// <summary>
        /// The name of the table.
        /// </summary>
        [IgnoreMember]
        public string TableName => tableName;

        /// <summary>
        /// Specifies whether the record was created at database level during the operation.
        /// </summary>
        [IgnoreMember]
        public bool? Created => created;

        /// <summary>
        /// The version number of the record. 
        /// </summary>
        [IgnoreMember]
        public int? Version => version;

        /// <summary>
        ///  The block containing the results requested in the RETURNING clause.
        /// </summary>
        [IgnoreMember]
        public List<object> ReturningRecordValues => returningRecordValues;
    }
}
