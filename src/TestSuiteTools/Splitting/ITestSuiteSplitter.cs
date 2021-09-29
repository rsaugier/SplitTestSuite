using TestSuiteTools.Model;

namespace TestSuiteTools.Splitting
{
    internal interface ITestSuiteSplitter
    {
        TestSuitePartition Split(TestSuite testSuite, int numParts);
    }
}