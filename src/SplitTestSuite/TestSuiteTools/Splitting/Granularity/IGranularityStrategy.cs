using System.Collections.Generic;
using SplitTestSuite.TestSuiteTools.Model;
using SplitTestSuite.TestSuiteTools.Model.Builders;

namespace SplitTestSuite.TestSuiteTools.Splitting.Granularity
{
    internal interface IGranularityStrategy
    {
        IReadOnlyList<ITestSuitePart> GetItemsFromTestSuite(TestSuite testSuite);
        ITestSuiteBuilder CreateBuilder();
    }
}