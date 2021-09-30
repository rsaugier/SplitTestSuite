using System.Collections.Generic;
using System.Linq;

namespace TestSuiteTools.Model
{
    public class TestAssemblyPart : ITestAssemblyPart
    {
        private readonly Dictionary<string, TestNamespacePart> namespacesByName = new();

        public TestAssembly Assembly { get; }
        public string Path => Assembly.Path;
        public IReadOnlyCollection<ITestNamespacePart> TestNamespaces => this.namespacesByName.Values;
        public bool IsWhole { get; }

        public TestAssemblyPart(TestAssembly assembly, IReadOnlyCollection<TestNamespacePart> testNamespaces)
        {
            this.Assembly = assembly;
            foreach (var testNamespace in testNamespaces)
            {
                this.namespacesByName.Add(testNamespace.Namespace.Name, testNamespace);
            }

            IsWhole = Assembly.TestNamespaces.All(ns => this.namespacesByName.ContainsKey(ns.Name));
        }
    }
}