namespace TestSuiteTools.Model
{
    public class TestClass
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