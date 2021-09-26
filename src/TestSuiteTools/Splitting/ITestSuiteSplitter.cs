using TestSuiteTools.Model;

namespace TestSuiteTools.Splitting
{
    internal interface ITestSuiteSplitter
    {
        TestSuitePartition Split(TestSuitePart testSuite, int numParts);
    }
}