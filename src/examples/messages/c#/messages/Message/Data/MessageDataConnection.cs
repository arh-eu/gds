using System;
using System.Collections.Generic;
using System.Text;
using MsgPack;
using MsgPack.Serialization;

namespace messages
{
    public class MessageDataConnection : MessageData
    {
        private bool serveOnTheSameConnection;
        private int protocolVersionNumber;
        private bool fragmentationSupported;
        private int? fragmentationTransmissionUnit;
        private string[] reservedFields;

        public MessageDataConnection(bool serveOnTheSameConnection, int protocolVersionNumber,
            bool fragmentationSupported, int? fragmentationTransmissionUnit, string password)
        {
            this.serveOnTheSameConnection = serveOnTheSameConnection;
            this.protocolVersionNumber = protocolVersionNumber;
            this.fragmentationSupported = fragmentationSupported;
            this.fragmentationTransmissionUnit = fragmentationTransmissionUnit;
            this.reservedFields = new string[1] { password };
        }

        public MessageDataConnection(bool serveOnTheSameConnection, int protocolVersionNumber,
            bool fragmentationSupported, int? fragmentationTransmissionUnit) 
            : this(serveOnTheSameConnection, protocolVersionNumber, 
                  fragmentationSupported, fragmentationTransmissionUnit, null) { }

        internal MessageDataConnection() {}

        public bool ServeOnTheSameConnection => serveOnTheSameConnection;
        public int ProtocolVersionNumber => protocolVersionNumber;
        public bool FragmentationSupported => fragmentationSupported;
        public int? FragmentationTransmissionUnit => fragmentationTransmissionUnit;
        public String Password => reservedFields?[0];

        public override DataType GetMessageDataType()
        {
            return DataType.Connection;
        }

        public override bool IsConnectionMessageData()
        {
            return true;
        }

        public override MessageDataConnection AsConnectionMessageData()
        {
            return this;
        }

        public override void PackToMessage(Packer packer, PackingOptions options)
        {
            packer.PackArrayHeader(5);
            packer.Pack(serveOnTheSameConnection);
            packer.Pack(protocolVersionNumber);
            packer.Pack(fragmentationSupported);
            packer.Pack(FragmentationTransmissionUnit);
            packer.Pack(reservedFields);
        }

        public override void UnpackFromMessage(Unpacker unpacker)
        {
            unpacker.ReadBoolean(out serveOnTheSameConnection);
            unpacker.ReadInt32(out protocolVersionNumber);
            unpacker.ReadBoolean(out fragmentationSupported);
            unpacker.ReadNullableInt32(out fragmentationTransmissionUnit);
            if(unpacker.Read())
            {
                reservedFields = new string[1];
                unpacker.ReadString(out reservedFields[0]);
            }
        }

        public override string ToString()
        {
            return String.Format("[serveOnThesameConnection={0},protocolVersionNumber={1},fragmentationSupported={2},fragmentationTransmissionUnit={3},reservedFields={4}]",
                serveOnTheSameConnection, protocolVersionNumber, fragmentationSupported, fragmentationTransmissionUnit, reservedFields);
        }
    }
}
