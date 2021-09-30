using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Splitting.Granularity
{
    public class NamespaceGranularityStrategy : IGranularityStrategy
    {
        public IReadOnlyList<ITestSuiteGrain> GetItemsFromTestSuite(TestSuite testSuite)
        {
            return testSuite.TestAssemblies.SelectMany(a => a.TestNamespaces).ToList();
        }

        public IPartBuilder CreateBuilder(TestSuite testSuite)
        {
            return new PartBuilderAdapter<ITestNamespacePart>(new NamespaceWiseTestSuitePartBuilder(testSuite));
        }
    }
}