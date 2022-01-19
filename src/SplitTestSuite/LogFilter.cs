using System;
using TestSuiteTools;

namespace SplitTestSuite
{
    public class LogFilter: ILog
    {
        private readonly ILog wrappedLog;
        private readonly LogLevel maxLogLevel;

        public LogFilter(ILog wrappedLog, LogLevel maxLogLevel)
        {
            this.wrappedLog = wrappedLog ?? throw new ArgumentNullException(nameof(wrappedLog));
            this.maxLogLevel = maxLogLevel;
        }

        public void Error(string message)
        {
            if (this.maxLogLevel >= LogLevel.Error)
            {
                this.wrappedLog.Error(message);
            }
        }

        public void Error(params string[] messages)
        {
            if (this.maxLogLevel >= LogLevel.Error)
            {
                this.wrappedLog.Error(messages);
            }
        }

        public void Warn(string message)
        {
            if (this.maxLogLevel >= LogLevel.Warn)
            {
                this.wrappedLog.Warn(message);
            }
        }

        public void Warn(params string[] messages)
        {
            if (this.maxLogLevel >= LogLevel.Warn)
            {
                this.wrappedLog.Warn(messages);
            }
        }

        public void Info(string message)
        {
            if (this.maxLogLevel >= LogLevel.Info)
            {
                this.wrappedLog.Info(message);
            }
        }

        public void Info(params string[] messages)
        {
            if (this.maxLogLevel >= LogLevel.Info)
            {
                this.wrappedLog.Info(messages);
            }
        }

        public void Debug(string message)
        {
            if (this.maxLogLevel >= LogLevel.Debug)
            {
                this.wrappedLog.Debug(message);
            }
        }

        public void Debug(params string[] messages)
        {
            if (this.maxLogLevel >= LogLevel.Debug)
            {
                this.wrappedLog.Debug(messages);
            }
        }
    }
}