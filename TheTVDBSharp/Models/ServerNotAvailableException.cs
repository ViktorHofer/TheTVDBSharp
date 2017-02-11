using System;

namespace TheTVDBSharp.Models
{
    public class ServerNotAvailableException : Exception
    {
        public ServerNotAvailableException(string message = "Server is currently not available. Please try again later", Exception inner = null)
            : base(message, inner)
        {
        }
    }
}
