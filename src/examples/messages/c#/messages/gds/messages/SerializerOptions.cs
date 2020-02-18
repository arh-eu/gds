using MessagePack;
using MessagePack.Formatters;
using gds.messages.data;
using gds.messages.header;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages
{
    static class SerializerOptions
    {
        private static readonly IFormatterResolver PrivateFieldsResolver = MessagePack.Resolvers.StandardResolverAllowPrivate.Instance;
        private static readonly IMessagePackFormatter MessageFormatter = new MessageFormatter();
        private static readonly IMessagePackFormatter HeaderFormatter = new HeaderFormatter();
        private static readonly IMessagePackFormatter ConnectionAckTypeDataFormatter = new ConnectionAckTypeDataFormatter();
        private static readonly IMessagePackFormatter AttachmentRequestDataFormatter = new AttachmentRequestFormatter();
        private static readonly IMessagePackFormatter QueryRequestFormatter = new QueryRequestFormatter();
        private static readonly IMessagePackFormatter EventDocumentAckResultFormatter = new EventDocumentAckResultFormatter();

        public static readonly MessagePackSerializerOptions AllSerializerOptions =
            MessagePackSerializerOptions.Standard.WithResolver(MessagePack.Resolvers.CompositeResolver.Create(
                new IMessagePackFormatter[] { MessageFormatter, HeaderFormatter, ConnectionAckTypeDataFormatter, AttachmentRequestDataFormatter, 
                    QueryRequestFormatter, EventDocumentAckResultFormatter },
                new IFormatterResolver[] { PrivateFieldsResolver }));
    }
}
