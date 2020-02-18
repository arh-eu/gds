using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages.data
{
    public enum StatusCode
    {
        OK = 200,
        Created = 201,
        Accepted = 202,
        NotAccepted = 304,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        NotAcceptable = 406,
        RequestTimeout = 408,
        Conflict = 409,
        PreconditionFailed = 412,
        TooManyRequests = 429,
        InternalServerError = 500,
        LimitExceeded = 509,
        NotExtended = 510
    }
}
