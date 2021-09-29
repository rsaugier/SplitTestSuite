using System;

namespace TestSuiteTools.Model.Builders
{
    public class PartBuilderAdapter<TItem> : IPartBuilder
        where TItem : class
    {
        private readonly IPartBuilder<TItem> adapted;

        public PartBuilderAdapter(IPartBuilder<TItem> adapted)
        {
            this.adapted = adapted;
        }

        public void AddItem(ITestSuiteGrain part)
        {
            if (!(part is TItem concreteItem))
            {
                throw new InvalidOperationException($"expected {nameof(TItem)}");
            }

            this.adapted.AddItem(concreteItem);
        }

        public ITestSuitePart Build()
        {
            return this.adapted.Build();
        }
    }
}