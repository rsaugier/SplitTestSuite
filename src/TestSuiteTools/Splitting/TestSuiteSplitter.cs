using System;
using System.Collections.Generic;
using SplitTestSuite.TestSuiteTools.Model;
using SplitTestSuite.TestSuiteTools.Model.Builders;
using SplitTestSuite.TestSuiteTools.Splitting.Granularity;
using SplitTestSuite.TestSuiteTools.Splitting.SplitStrategy;

namespace SplitTestSuite.TestSuiteTools.Splitting
{
    internal class TestSuiteSplitter : ITestSuiteSplitter
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
            IReadOnlyList<ITestSuitePart> items = this.granularityStrategy.GetItemsFromTestSuite(testSuite);
            List<List<ITestSuitePart>> parts = this.splitStrategy.Split(items, numParts);
            List<TestSuite> testSuiteParts = new();
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