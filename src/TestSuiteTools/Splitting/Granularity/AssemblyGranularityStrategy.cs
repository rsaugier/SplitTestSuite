using System.Collections.Generic;
using System.Linq;
using SplitTestSuite.TestSuiteTools.Model;
using SplitTestSuite.TestSuiteTools.Model.Builders;

namespace SplitTestSuite.TestSuiteTools.Splitting.Granularity
{
    internal class AssemblyGranularityStrategy : IGranularityStrategy
    {
        public IReadOnlyList<ITestSuitePart> GetItemsFromTestSuite(TestSuite testSuite)
        {
            return testSuite.TestAssemblies.ToList();
        }

        public ITestSuiteBuilder CreateBuilder()
        {
            return new TestSuiteBuilderAdapter<TestAssemblyPart>(new AssemblyWiseTestSuiteBuilder());
        }
    }
}