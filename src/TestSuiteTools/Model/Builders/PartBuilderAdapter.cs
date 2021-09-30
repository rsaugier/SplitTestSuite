using System;

namespace TestSuiteTools.Model.Builders
{
    public class PartBuilderAdapter<TGrain> : IPartBuilder
        where TGrain : ITestSuiteGrain
    {
        private readonly IPartBuilder<TGrain> adapted;

        public PartBuilderAdapter(IPartBuilder<TGrain> adapted)
        {
            this.adapted = adapted;
        }

        public void AddItem(ITestSuiteGrain part)
        {
            if (!(part is TGrain concreteItem))
            {
                throw new InvalidOperationException($"expected {typeof(TGrain).Name}");
            }

            this.adapted.AddItem(concreteItem);
        }

        public ITestSuitePart Build()
        {
            return this.adapted.Build();
        }
    }
}