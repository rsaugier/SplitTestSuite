namespace TestSuiteTools.Model.Builders
{
    public interface ITestSuiteBuilder
    {
        void AddItem(ITestSuitePart part);
        TestSuitePart Build();
    }

    public interface ITestSuiteBuilder<in TItem>
    {
        void AddItem(TItem item);
        TestSuitePart Build();
    }
}