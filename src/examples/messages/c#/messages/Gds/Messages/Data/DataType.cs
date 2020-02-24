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

namespace Gds.Messages.Data
{
    /// <summary>
    /// Specifies what types of information the data carries.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// The message is for connection establishment.
        /// </summary>
        Connection = 0,

        /// <summary>
        /// An acknowledgement regardingg a connection message.
        /// </summary>
        ConnectionAck = 1,

        /// <summary>
        /// The message is for store or modify events. 
        /// </summary>
        Event = 2,

        /// <summary>
        /// An acknowledgement regardingg an event message.
        /// </summary>
        EventAck = 3,

        /// <summary>
        /// The message is for requesting an attachment.
        /// </summary>
        AttachmentRequest = 4,

        /// <summary>
        /// An acknowledgement regardingg an attachment request message.
        /// </summary>
        AttachmentRequestAck = 5,

        /// <summary>
        /// A response message regardingg an attachment request message.
        /// </summary>
        AttachmentResponse = 6,

        /// <summary>
        /// An acknowledgement regardingg an attachment response message.
        /// </summary>
        AttachmentResponseAck = 7,

        /// <summary>
        /// The message is for sending event.
        /// </summary>
        EventDocument = 8,

        /// <summary>
        /// An acknowledgement regardingg an event document message.
        /// </summary>
        EventDocumentAck = 9,

        /// <summary>
        /// The message is for make a query request.
        /// </summary>
        QueryRequest = 10,

        /// <summary>
        /// An acknowledgement regardingg a query request message.
        /// </summary>
        QueryRequestAck = 11,

        /// <summary>
        /// The message is for make the next query page request.
        /// </summary>
        NextQueryPageRequest = 12
    }
}
