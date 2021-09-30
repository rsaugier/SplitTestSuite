using System.Collections.Generic;
using System.Linq;

namespace TestSuiteTools.Model
{
    public class TestAssemblyPart : ITestAssemblyPart
    {
        private readonly Dictionary<string, ITestNamespacePart> namespacesByName = new();

        public TestAssembly Assembly { get; }
        public string Path => Assembly.Path;
        public IReadOnlyCollection<ITestNamespacePart> TestNamespaces => this.namespacesByName.Values;
        public bool IsWhole { get; }
        public TestAssembly Whole => Assembly;

        public TestAssemblyPart(TestAssembly assembly, IReadOnlyCollection<ITestNamespacePart> testNamespaces)
        {
            this.Assembly = assembly;
            foreach (var testNamespace in testNamespaces)
            {
                this.namespacesByName.Add(testNamespace.Whole.Name, testNamespace);
            }

            IsWhole = Assembly.TestNamespaces.All(ns => this.namespacesByName.ContainsKey(ns.Name));
        }
    }
}