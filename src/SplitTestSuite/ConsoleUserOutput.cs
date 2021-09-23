using System;
using TestSuiteTools;

namespace SplitTestSuite
{
    public class ConsoleUserOutput : IUserOutput
    {
        public ConsoleUserOutput()
        {
        }

        public void Say(string message)
        {
            Console.WriteLine(message);
        }

        public void Say(params string[] messages)
        {
            foreach (var message in messages)
            {
                this.Say(message);
            }
        }
    }
}