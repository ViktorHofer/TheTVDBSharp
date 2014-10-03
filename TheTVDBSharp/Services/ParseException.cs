using System;

namespace TheTVDBSharp.Services
{
    public class ParseException : Exception
    {
        public ParseException(string message = null, Exception inner = null)
            : base(message, inner)
        {
        }
    }
}
