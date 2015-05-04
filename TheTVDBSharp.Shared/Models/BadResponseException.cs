using System;
#if PORTABLE
using System.Net;
#elif WINDOWS_UAP
using Windows.Web.Http;
#endif

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
