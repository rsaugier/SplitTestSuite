using System.Collections.Generic;
using TestSuiteTools.Model;

namespace TestSuiteTools.Splitting.SplitStrategy
{
    public interface ISplitStrategy
    {
        List<List<ITestSuitePart>> Split(IReadOnlyCollection<ITestSuitePart> items, int numParts);
    }
}