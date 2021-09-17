using System.Collections.Generic;
using SplitTestSuite.TestSuiteTools.Model;

namespace SplitTestSuite.TestSuiteTools.Splitting
{
    internal class NullTestSuiteSplitter : ITestSuiteSplitter
    {
        public TestSuitePartition Split(TestSuite testSuite, int numParts)
        {
            return new TestSuitePartition(new List<TestSuite>(){testSuite});
        }
    }
}