using System;

namespace SplitTestSuite.TestSuiteTools.Model.Builders
{
    internal class TestSuiteBuilderAdapter<TItem> : ITestSuiteBuilder
        where TItem : class
    {
        private readonly ITestSuiteBuilder<TItem> adapted;

        public TestSuiteBuilderAdapter(ITestSuiteBuilder<TItem> adapted)
        {
            this.adapted = adapted;
        }

        public void AddItem(ITestSuitePart part)
        {
            if (!(part is TItem concreteItem))
            {
                throw new InvalidOperationException($"expected {nameof(TItem)}");
            }

            this.adapted.AddItem(concreteItem);
        }

        public TestSuite Build()
        {
            return this.adapted.Build();
        }
    }
}