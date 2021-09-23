using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Splitting.Granularity
{
    public class NamespaceGranularityStrategy : IGranularityStrategy
    {
        public IReadOnlyList<ITestSuitePart> GetItemsFromTestSuite(TestSuite testSuite)
        {
            return testSuite.TestAssemblies.SelectMany(a => a.TestNamespaces).ToList();
        }

        public ITestSuiteBuilder CreateBuilder()
        {
            return new TestSuiteBuilderAdapter<TestNamespacePart>(new NamespaceWiseTestSuiteBuilder());
        }
    }
}