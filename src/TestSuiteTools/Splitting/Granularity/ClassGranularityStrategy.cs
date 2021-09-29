using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Splitting.Granularity
{
    public class ClassGranularityStrategy : IGranularityStrategy
    {
        public IReadOnlyList<ITestSuiteGrain> GetItemsFromTestSuite(TestSuite testSuite)
        {
            return testSuite.TestAssemblies.SelectMany(
                a => a.TestNamespaces.SelectMany(
                    ns => ns.TestClasses)).ToList();
        }

        public IPartBuilder CreateBuilder(TestSuite testSuite)
        {
            return new PartBuilderAdapter<TestClassPart>(new ClassWiseTestSuitePartBuilder(testSuite));
        }
    }
}