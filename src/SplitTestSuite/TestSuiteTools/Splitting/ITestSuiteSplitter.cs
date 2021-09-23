using SplitTestSuite.TestSuiteTools.Model;

namespace SplitTestSuite.TestSuiteTools.Splitting
{
    internal interface ITestSuiteSplitter
    {
        TestSuitePartition Split(TestSuite testSuite, int numParts);
    }
}