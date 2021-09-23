namespace TestSuiteTools.Model.Builders
{
    public interface ITestSuiteBuilder
    {
        void AddItem(ITestSuitePart part);
        TestSuite Build();
    }

    public interface ITestSuiteBuilder<in TItem>
    {
        void AddItem(TItem item);
        TestSuite Build();
    }
}