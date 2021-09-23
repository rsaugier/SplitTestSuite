namespace SplitTestSuite.TestSuiteTools.Model
{
    /// <summary>
    /// An immutable class representing a test method.
    /// </summary>
    internal class TestMethod : ITestSuitePart
    {
        public string Name { get; }
        public TestClass Class { get; }

        public TestMethod(string name, TestClass testClass)
        {
            this.Name = name;
            this.Class = testClass;
        }
    }
}