using System;
using System.Collections.Generic;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;
using TestSuiteTools.Splitting.Granularity;
using TestSuiteTools.Splitting.SplitStrategy;

namespace TestSuiteTools.Splitting
{
    public class TestSuiteSplitter : ITestSuiteSplitter
    {
        private readonly ISplitStrategy splitStrategy;
        private readonly IGranularityStrategy granularityStrategy;

        public TestSuiteSplitter(ISplitStrategy splitStrategy, IGranularityStrategy granularityStrategy)
        {
            this.splitStrategy = splitStrategy ?? throw new ArgumentNullException(nameof(splitStrategy));
            this.granularityStrategy = granularityStrategy ?? throw new ArgumentNullException(nameof(granularityStrategy));
        }

        public TestSuitePartition Split(TestSuite testSuite, int numParts)
        {
            IReadOnlyList<ITestSuiteGrain> items = this.granularityStrategy.GetItemsFromTestSuite(testSuite);
            List<List<ITestSuiteGrain>> parts = this.splitStrategy.Split(items, numParts);
            List<ITestSuitePart> testSuiteParts = new();
            foreach (var partItems in parts)
            {
                IPartBuilder builder = this.granularityStrategy.CreateBuilder(testSuite);
                foreach (var partItem in partItems)
                {
                    builder.AddItem(partItem);
                }
                testSuiteParts.Add(builder.Build());
            }
            return new TestSuitePartition(testSuiteParts);
        }
    }
}