namespace SplitTestSuite.TestSuiteTools.Model
{
    internal class TestClass
    {
        public string Name { get; }
        public TestNamespace Namespace { get; }

        public TestClass(string name, TestNamespace testNamespace)
        {
            this.Name = name;
            this.Namespace = testNamespace;
        }
    }
}