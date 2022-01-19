namespace TestSuiteTools.UnitTests
{
using System;
using TestSuiteTools;

namespace SplitTestSuite
{
    public class TestsConsoleLog : ILog
    {
        public TestsConsoleLog()
        {
        }

        public void Error(string message)
        {
            Console.WriteLine($"ERROR: {message}");
        }

        public void Error(params string[] messages)
        {
            foreach (var message in messages)
            {
                this.Error(message);
            }
        }

        public void Warn(string message)
        {
            Console.WriteLine($"WARNING: {message}");
        }

        public void Warn(params string[] messages)
        {
            foreach (var message in messages)
            {
                this.Warn(message);
            }
        }

        public void Info(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }

        public void Info(params string[] messages)
        {
            foreach (var message in messages)
            {
                this.Info(message);
            }
        }

        public void Debug(string message)
        {
            Console.WriteLine($"DEBUG: {message}");
        }

        public void Debug(params string[] messages)
        {
            foreach (var message in messages)
            {
                this.Debug(message);
            }
        }
    }
}
}