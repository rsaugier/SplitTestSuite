using System.Collections.Generic;
using TestSuiteTools.Model;

namespace TestSuiteTools.Splitting.SplitStrategy
{
    public interface ISplitStrategy
    {
        List<List<ITestSuiteGrain>> Split(IReadOnlyCollection<ITestSuiteGrain> items, int numParts);
    }
}