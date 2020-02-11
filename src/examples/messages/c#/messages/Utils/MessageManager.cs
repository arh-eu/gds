using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace messages
{
    public class MessageManager
    {
        public static MemoryStream GetBinaryMessage(Message message)
        {
            MemoryStream stream = new MemoryStream();
            SerializationContext context = new SerializationContext();
            context.Serializers.RegisterOverride(new MessageSerializer(context));
            MessagePackSerializer serializer = MessagePackSerializer.Get<Message>(context);
            serializer.Pack(stream, message);
            return stream;
        }

        public static Message GetMessageFromBinary(MemoryStream stream)
        {
            SerializationContext context = new SerializationContext();
            context.Serializers.RegisterOverride(new MessageSerializer(context));
            MessagePackSerializer serializer = MessagePackSerializer.Get<Message>(context);
            return (Message)serializer.Unpack(stream);
        }

        public static Message GetMessage(MessageHeader header, MessageData data)
        {
            return new Message(header, data);
        }

        public static MessageHeader GetMessageHeader(string user, string messageId, long createTime, long requestTime,
            bool isFragmented, bool? firstFragment, bool? lastFragment, int? offset,
            int? fullDataSize, int dataType)
        {
            return new MessageHeader(user, messageId, createTime, requestTime,
                isFragmented, firstFragment, lastFragment, offset, fullDataSize, dataType);
        }

        public static MessageDataConnection GetConnectionMessageData(bool serveOnTheSameConnection,
            int protocolVersionNumber, bool fragmentationSupported, int? fragmentationTransmissionUnit, string password)
        {
            return new MessageDataConnection(serveOnTheSameConnection, protocolVersionNumber, fragmentationSupported,
                fragmentationTransmissionUnit, password);
        }

        public static MessageDataConnection GetConnectionMessageData(bool serveOnTheSameConnection,
            int protocolVersionNumber, bool fragmentationSupported, int? fragmentationTransmissionUnit)
        {
            return new MessageDataConnection(serveOnTheSameConnection, protocolVersionNumber, fragmentationSupported,
                fragmentationTransmissionUnit);
        }

        public static MessageDataConnectionAck GetConnectionAckMessageData(int status, MessageDataConnection ackData, string exception)
        {
            return new MessageDataConnectionAck(status, ackData, exception);
        }

        public static MessageDataConnectionAck GetConnectionAckMessageData(int status, Dictionary<int, string> unauthorizedAckData, string exception)
        {
            return new MessageDataConnectionAck(status, unauthorizedAckData, exception);
        }

        public static MessageDataConnectionAck GetConnectionAckMessageData(int status, string exception)
        {
            return new MessageDataConnectionAck(status, exception);
        }
    }
}
