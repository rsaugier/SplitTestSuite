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

        public TestSuitePartition Split(TestSuitePart testSuite, int numParts)
        {
            IReadOnlyList<ITestSuitePart> items = this.granularityStrategy.GetItemsFromTestSuite(testSuite);
            List<List<ITestSuitePart>> parts = this.splitStrategy.Split(items, numParts);
            List<TestSuitePart> testSuiteParts = new();
            foreach (var partItems in parts)
            {
                ITestSuiteBuilder builder = this.granularityStrategy.CreateBuilder();
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