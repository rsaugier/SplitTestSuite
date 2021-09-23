using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    /// <summary>
    /// An immutable class representing a test assembly.
    /// </summary>
    public class TestAssemblyPart : ITestSuitePart
    {
        private readonly Dictionary<string, TestNamespacePart> namespacesByName = new();

        public TestAssembly Assembly { get; }
        public string Path => Assembly.Path;
        public IReadOnlyCollection<TestNamespacePart> TestNamespaces => this.namespacesByName.Values;

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