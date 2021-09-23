using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Splitting.Granularity
{
    public class AssemblyGranularityStrategy : IGranularityStrategy
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