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
    /// ACK status codes
    /// </summary>
    public enum StatusCode
    {
        /// <summary>
        /// Everything is alright
        /// </summary>
        OK = 200,

        /// <summary>
        /// Created
        /// </summary>
        Created = 201,

        /// <summary>
        /// Accepted, performed
        /// </summary>
        Accepted = 202,

        /// <summary>
        /// No modifications were done
        /// </summary>
        NotAccepted = 304,

        /// <summary>
        /// Bad request
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// Unauthorized
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// Forbidden
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// Failed to serve the request
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// Unexpected error
        /// </summary>
        NotAcceptable = 406,

        /// <summary>
        /// Timeout
        /// </summary>
        RequestTimeout = 408,

        /// <summary>
        /// Version conflict
        /// </summary>
        Conflict = 409,

        /// <summary>
        /// Unsuccessful request
        /// </summary>
        PreconditionFailed = 412,

        /// <summary>
        /// Too many requests
        /// </summary>
        TooManyRequests = 429,

        /// <summary>
        /// Unexpected error
        /// </summary>
        InternalServerError = 500,

        /// <summary>
        /// Bandwidth limit exceeded
        /// </summary>
        LimitExceeded = 509,

        /// <summary>
        /// Missing update permission
        /// </summary>
        NotExtended = 510
    }
}
