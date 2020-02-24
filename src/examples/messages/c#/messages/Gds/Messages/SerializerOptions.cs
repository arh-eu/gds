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
using Gds.Messages.Data;
using Gds.Messages.Header;

namespace Gds.Messages
{
    static class SerializerOptions
    {
        private static readonly IFormatterResolver PrivateFieldsResolver = MessagePack.Resolvers.StandardResolverAllowPrivate.Instance;
        private static readonly IMessagePackFormatter MessageFormatter = new MessageFormatter();
        private static readonly IMessagePackFormatter MessageHeaderFormatter = new MessageHeaderFormatter();
        private static readonly IMessagePackFormatter ConnectionAckTypeDataFormatter = new ConnectionAckTypeDataFormatter();
        private static readonly IMessagePackFormatter AttachmentRequestDataFormatter = new AttachmentRequestFormatter();
        private static readonly IMessagePackFormatter QueryRequestFormatter = new QueryRequestFormatter();
        private static readonly IMessagePackFormatter EventDocumentAckResultFormatter = new EventDocumentAckResultFormatter();

        public static readonly MessagePackSerializerOptions AllSerializerOptions =
            MessagePackSerializerOptions.Standard.WithResolver(MessagePack.Resolvers.CompositeResolver.Create(
                new IMessagePackFormatter[] { MessageFormatter, MessageHeaderFormatter, ConnectionAckTypeDataFormatter, AttachmentRequestDataFormatter,
                    QueryRequestFormatter, EventDocumentAckResultFormatter },
                new IFormatterResolver[] { PrivateFieldsResolver }));
    }
}
