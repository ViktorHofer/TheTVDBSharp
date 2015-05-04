using System;

namespace TheTVDBSharp.Models
{
    public class ParseException : Exception
    {
        public ParseException(string message = null, Exception inner = null)
            : base(message, inner)
        {
        }
    }
}
