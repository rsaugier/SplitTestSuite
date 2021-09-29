using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model;

namespace TestSuiteTools.Splitting.SplitStrategy
{
    public class RoundRobinSplitStrategy : ISplitStrategy
    {
        public List<List<ITestSuiteGrain>> Split(IReadOnlyCollection<ITestSuiteGrain> items, int numParts)
        {
            var parts = new List<ITestSuiteGrain>[numParts];
            for (int i = 0; i < numParts; ++i)
            {
                parts[i] = new List<ITestSuiteGrain>();
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