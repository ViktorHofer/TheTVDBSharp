using System;

namespace TheTVDBSharp.Services
{
    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    public class LogEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public LogLevel Level { get; private set; }

        public Exception InnerException { get; private set; }

        public LogEventArgs(string message, LogLevel level, Exception innerException = null)
        {
            this.Message = message;
            this.Level = level;
            this.InnerException = innerException;
        }
    }

    public interface ISimpleLogger
    {
        event EventHandler<LogEventArgs> Logged;

        void Log(string message, LogLevel level, Exception innerException = null);
    }
}
