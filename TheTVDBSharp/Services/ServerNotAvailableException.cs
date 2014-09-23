using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheTVDBSharp.Services
{
    public class ServerNotAvailableException : Exception
    {
        public ServerNotAvailableException(string message = "Server is currently not available. Please try again later", Exception inner = null)
            : base(message, inner)
        {
        }
    }
}
