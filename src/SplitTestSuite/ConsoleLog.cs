using System;
using TestSuiteTools;

namespace SplitTestSuite
{
    public class ConsoleLog : ILog
    {
        public ConsoleLog()
        {
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Info(params string[] messages)
        {
            foreach (var message in messages)
            {
                this.Info(message);
            }
        }
    }
}