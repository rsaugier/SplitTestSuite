using System.Collections.Generic;
using SplitTestSuite.TestSuiteTools.Model;

namespace SplitTestSuite.TestSuiteTools.Splitting.SplitStrategy
{
    internal interface ISplitStrategy
    {
        List<List<ITestSuitePart>> Split(IReadOnlyCollection<ITestSuitePart> items, int numParts);
    }
}