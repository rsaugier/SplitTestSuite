using System;

namespace TestSuiteTools.Model.Builders
{
    public class TestSuiteBuilderAdapter<TItem> : ITestSuiteBuilder
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

        public TestSuitePart Build()
        {
            return this.adapted.Build();
        }
    }
}