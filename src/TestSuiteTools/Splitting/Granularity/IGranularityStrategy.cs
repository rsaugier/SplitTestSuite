using System.Collections.Generic;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Splitting.Granularity
{
    public interface IGranularityStrategy
    {
        IReadOnlyList<ITestSuitePart> GetItemsFromTestSuite(TestSuitePart testSuite);
        ITestSuiteBuilder CreateBuilder();
    }
}