using System.Collections.Generic;
using TestSuiteTools.Model;

namespace TestSuiteTools.Splitting
{
    internal class NullTestSuiteSplitter : ITestSuiteSplitter
    {
        public TestSuitePartition Split(TestSuitePart testSuite, int numParts)
        {
            return new TestSuitePartition(new List<TestSuitePart>(){testSuite});
        }
    }
}