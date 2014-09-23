using System;

namespace TheTVDBSharp.Services
{
    public class SimpleLogger : ISimpleLogger
    {
        public event EventHandler<LogEventArgs> Logged;

        public void Log(string message, LogLevel level, Exception innerException = null)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException("message", "Log message cannot be null");

            if (this.Logged != null)
            {
                this.Logged(this, new LogEventArgs(message, level, innerException));
            }
        }
    }
}
