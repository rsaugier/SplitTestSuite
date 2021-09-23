using TestSuiteTools;

namespace SplitTestSuite
{
    public class NullUserOutput : IUserOutput
    {
        public void Say(string message)
        {
        }

        public void Say(params string[] messages)
        {
        }
    }
}