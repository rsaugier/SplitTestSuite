using System.Collections.Generic;
using System.Linq;
using SplitTestSuite.TestSuiteTools.Model;
using SplitTestSuite.TestSuiteTools.Model.Builders;

namespace SplitTestSuite.TestSuiteTools.Splitting.Granularity
{
    internal class MethodGranularityStrategy : IGranularityStrategy
    {
        public IReadOnlyList<ITestSuitePart> GetItemsFromTestSuite(TestSuite testSuite)
        {
            return testSuite.TestAssemblies.SelectMany(
                a => a.TestNamespaces.SelectMany(
                    ns => ns.TestClasses.SelectMany(c => c.TestMethods))).ToList();
        }

        public ITestSuiteBuilder CreateBuilder()
        {
            return new TestSuiteBuilderAdapter<TestMethod>(new MethodWiseTestSuiteBuilder());
        }
    }
}