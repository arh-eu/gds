using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class ConnectionAck : Data
    {
        [Key(0)]
        private readonly StatusCode status;

        [Key(1)]
        private readonly ConnectionAckTypeData ackData;

        [Key(2)]
        private readonly string exception;

        [SerializationConstructor]
        private ConnectionAck(StatusCode status, ConnectionAckTypeData ackData, string exception)
        {
            this.status = status;
            this.ackData = ackData;
            this.exception = exception;
        }

        public ConnectionAck(StatusCode status, Connection successAckData, string exception)
        {
            this.status = status;
            this.ackData = new ConnectionAckTypeData(successAckData);
            this.exception = exception;
        }

        public ConnectionAck(StatusCode status, Dictionary<int, string> unauthorizedAckData, string exception)
        {
            this.status = status;
            this.ackData = new ConnectionAckTypeData(unauthorizedAckData);
            this.exception = exception;
        }

        public ConnectionAck(StatusCode status, string exception)
        {
            this.status = status;
            this.ackData = new ConnectionAckTypeData();
            this.exception = exception;
        }

        [IgnoreMember]
        public StatusCode Status => status;

        [IgnoreMember]
        public String Exception => exception;

        [IgnoreMember]
        public Connection SuccessAckData => ackData.SuccessAckTypeData;

        [IgnoreMember]
        public Dictionary<int, string> UnauthorizedAckData => ackData.UnauthorizedAckTypeData;

        public override DataType GetDataType()
        {
            return DataType.ConnectionAck;
        }

        public override bool IsConnectionAckData()
        {
            return true;
        }

        public override ConnectionAck AsConnectionAckData()
        {
            return this;
        }
    }

    public class ConnectionAckTypeData
    {
        private readonly Connection successAckTypeData;
        private readonly Dictionary<int, string> unauthorizedAckTypeData;

        public ConnectionAckTypeData(Connection successAckTypeData)
        {
            this.successAckTypeData = successAckTypeData;
        }

        public ConnectionAckTypeData(Dictionary<int, string> unauthorizedAckTypeData)
        {
            this.unauthorizedAckTypeData = unauthorizedAckTypeData;
        }

        public ConnectionAckTypeData() { }

        public Connection SuccessAckTypeData => successAckTypeData;

        public Dictionary<int, string> UnauthorizedAckTypeData => unauthorizedAckTypeData;
    }

    public class ConnectionAckTypeDataFormatter : IMessagePackFormatter<ConnectionAckTypeData>
    {
        public void Serialize(ref MessagePackWriter writer, ConnectionAckTypeData value, MessagePackSerializerOptions options)
        {
            if(value.SuccessAckTypeData != null)
            {
                MessagePackSerializer.Serialize(ref writer, value.SuccessAckTypeData, options);
            } 
            else if(value.UnauthorizedAckTypeData != null)
            {
                MessagePackSerializer.Serialize(ref writer, value.UnauthorizedAckTypeData);
            } 
            else
            {
                writer.WriteNil();
            }
        }

        public ConnectionAckTypeData Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if(reader.NextMessagePackType.Equals(MessagePackType.Array))
            {
                return new ConnectionAckTypeData(
                    MessagePackSerializer.Deserialize<Connection>(ref reader, options));
            } 
            else if(reader.NextMessagePackType.Equals(MessagePackType.Map))
            {
                return new ConnectionAckTypeData(
                    MessagePackSerializer.Deserialize<Dictionary<int, string>>(ref reader));
            } 
            else
            {
                reader.ReadNil();
                return null;
            }
        }
    }
}
