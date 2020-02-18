using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    public enum DataType
    {
        Connection = 0,
        ConnectionAck = 1,
        Event = 2,
        EventAck = 3,
        AttachmentRequest = 4,
        AttachmentRequestAck = 5,
        AttachmentResponse = 6,
        AttachmentResponseAck = 7,
        EventDocument = 8,
        EventDocumentAck = 9,
        QueryRequest = 10,
        QueryRequestAck = 11,
        NextQueryPageRequest = 12
    }
}
