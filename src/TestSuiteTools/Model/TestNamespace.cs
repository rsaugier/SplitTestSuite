namespace SplitTestSuite.TestSuiteTools.Model
{
    internal class TestNamespace
    {
        public string Name { get; }
        public TestAssembly Assembly { get; }

        public TestNamespace(string name, TestAssembly testAssembly)
        {
            this.Name = name;
            this.Assembly = testAssembly;
        }
    }
}