using TestSuiteTools;

namespace SplitTestSuite
{
    public class NullLog : ILog
    {
        public void Error(string message)
        {
        }

        public void Error(params string[] messages)
        {
        }

        public void Warn(string message)
        {
        }

        public void Warn(params string[] messages)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(params string[] messages)
        {
        }

        public void Debug(string message)
        {
        }

        public void Debug(params string[] messages)
        {
        }
    }
}