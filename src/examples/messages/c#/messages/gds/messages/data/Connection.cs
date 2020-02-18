using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    [MessagePackObject]
    public class Connection : Data
    {
        [Key(0)]
        private readonly bool serveOnTheSameConnection;

        [Key(1)]
        private readonly int protocolVersionNumber;

        [Key(2)]
        private readonly bool fragmentationSupported;

        [Key(3)]
        private readonly int? fragmentationTransmissionUnit;

        [Key(4)]
        private readonly object[] reservedFields;

        [SerializationConstructor]
        public Connection(bool serveOnTheSameConnection, int protocolVersionNumber, bool fragmentationSupported,
            int? fragmentationTransmissionUnit, object[] reservedFields)
        {
            this.serveOnTheSameConnection = serveOnTheSameConnection;
            this.protocolVersionNumber = protocolVersionNumber;
            this.fragmentationSupported = fragmentationSupported;
            this.fragmentationTransmissionUnit = fragmentationTransmissionUnit;
            this.reservedFields = reservedFields;
        }

        [IgnoreMember]
        public bool ServeOnTheSameConnection => serveOnTheSameConnection;

        [IgnoreMember]
        public int ProtocolVersionNumber => protocolVersionNumber;

        [IgnoreMember]
        public bool FragmentationSupported => fragmentationSupported;

        [IgnoreMember]
        public int? FragmentationTransmissionUnit => fragmentationTransmissionUnit;

        [IgnoreMember]
        public object[] ReservedFields => reservedFields;

        [IgnoreMember]
        public string Password
        {
            get 
            { 
                if(reservedFields != null && reservedFields.Length >= 1)
                {
                    return (string) reservedFields[0];
                }
                else
                {
                    return null;
                }

            }
        }

        public override DataType GetDataType()
        {
            return DataType.Connection;
        }

        public override bool IsConnectionData()
        {
            return true;
        }

        public override Connection AsConnectionData()
        {
            return this;
        }
    }
}
