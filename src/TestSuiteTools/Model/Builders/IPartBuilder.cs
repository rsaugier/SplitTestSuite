namespace TestSuiteTools.Model.Builders
{
    public interface IPartBuilder
    {
        void AddItem(ITestSuiteGrain part);
        ITestSuitePart Build();
    }

    public interface IPartBuilder<in TItem>
    {
        void AddItem(TItem item);
        ITestSuitePart Build();
    }
}