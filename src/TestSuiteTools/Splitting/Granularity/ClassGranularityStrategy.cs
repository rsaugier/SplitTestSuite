using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Splitting.Granularity
{
    public class ClassGranularityStrategy : IGranularityStrategy
    {
        public IReadOnlyList<ITestSuitePart> GetItemsFromTestSuite(TestSuite testSuite)
        {
            return testSuite.TestAssemblies.SelectMany(
                a => a.TestNamespaces.SelectMany(
                    ns => ns.TestClasses)).ToList();
        }

        public ITestSuiteBuilder CreateBuilder()
        {
            return new TestSuiteBuilderAdapter<TestClassPart>(new ClassWiseTestSuiteBuilder());
        }
    }
}