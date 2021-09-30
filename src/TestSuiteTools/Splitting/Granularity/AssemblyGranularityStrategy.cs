using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Splitting.Granularity
{
    public class AssemblyGranularityStrategy : IGranularityStrategy
    {
        public IReadOnlyList<ITestSuiteGrain> GetItemsFromTestSuite(TestSuite testSuite)
        {
            return testSuite.TestAssemblies.ToList();
        }

        public IPartBuilder CreateBuilder(TestSuite testSuite)
        {
            return new PartBuilderAdapter<ITestAssemblyPart>(new AssemblyWiseTestSuitePartBuilder(testSuite));
        }
    }
}