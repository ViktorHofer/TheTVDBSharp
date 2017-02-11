using System;
using System.Net;

namespace TheTVDBSharp.Models
{
    public class BadResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public BadResponseException(HttpStatusCode statusCode, string message = "Bad response", Exception inner = null)
            : base(message, inner)
        {
            StatusCode = statusCode;
        }
    }
}
