using System;
using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Splitting.Granularity
{
    public class MethodGranularityStrategy : IGranularityStrategy
    {
        public IReadOnlyList<ITestSuiteGrain> GetItemsFromTestSuite(TestSuite testSuite)
        {
            return testSuite.TestAssemblies.SelectMany(
                a => a.TestNamespaces.SelectMany(
                    ns => ns.TestClasses.SelectMany(c => c.TestMethods))).ToList();
        }

        public IPartBuilder CreateBuilder(TestSuite testSuite)
        {
            throw new NotImplementedException();
            //return new PartBuilderAdapter<TestMethod>(new TestSuiteBuilder());
        }
    }
}