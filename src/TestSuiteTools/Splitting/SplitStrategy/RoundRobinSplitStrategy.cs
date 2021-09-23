using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model;

namespace TestSuiteTools.Splitting.SplitStrategy
{
    public class RoundRobinSplitStrategy : ISplitStrategy
    {
        public List<List<ITestSuitePart>> Split(IReadOnlyCollection<ITestSuitePart> items, int numParts)
        {
            var parts = new List<ITestSuitePart>[numParts];
            for (int i = 0; i < numParts; ++i)
            {
                parts[i] = new List<ITestSuitePart>();
            }

            int itemIndex = 0;
            foreach (var item in items)
            {
                parts[itemIndex++ % numParts].Add(item);
            }
            return parts.ToList();
        }
    }
}