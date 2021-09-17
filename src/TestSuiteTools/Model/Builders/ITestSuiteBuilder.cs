namespace SplitTestSuite.TestSuiteTools.Model.Builders
{
    internal interface ITestSuiteBuilder
    {
        void AddItem(ITestSuitePart part);
        TestSuite Build();
    }

    internal interface ITestSuiteBuilder<in TItem>
    {
        void AddItem(TItem item);
        TestSuite Build();
    }
}