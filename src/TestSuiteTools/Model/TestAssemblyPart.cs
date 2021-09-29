using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestAssemblyPart : ITestAssemblyPart
    {
        private readonly Dictionary<string, TestNamespacePart> namespacesByName = new();

        public TestAssembly Assembly { get; }
        public string Path => Assembly.Path;
        public IReadOnlyCollection<ITestNamespacePart> TestNamespaces => this.namespacesByName.Values;

        public TestAssemblyPart(TestAssembly assembly, IReadOnlyCollection<TestNamespacePart> testClassLists)
        {
            this.Assembly = assembly;
            foreach (var testClassList in testClassLists)
            {
                this.namespacesByName.Add(testClassList.Namespace.Name, testClassList);
            }
        }
    }
}