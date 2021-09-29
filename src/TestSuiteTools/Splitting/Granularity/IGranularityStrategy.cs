using System.Collections.Generic;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Splitting.Granularity
{
    public interface IGranularityStrategy
    {
        IReadOnlyList<ITestSuiteGrain> GetItemsFromTestSuite(TestSuite testSuite);
        IPartBuilder CreateBuilder(TestSuite testSuite);
    }
}