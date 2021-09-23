using TestSuiteTools;

namespace SplitTestSuite
{
    public class NullLog : ILog
    {
        public void Info(string message)
        {
        }

        public void Info(params string[] messages)
        {
        }
    }
}