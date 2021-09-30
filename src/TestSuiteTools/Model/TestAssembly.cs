using System;
using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestAssembly : ITestAssemblyPart
    {
        private readonly Dictionary<string, TestNamespace> namespacesByName = new();

        public string Path { get; }
        public TestSuite TestSuite { get; private set; }
        public IReadOnlyCollection<TestNamespace> TestNamespaces => this.namespacesByName.Values;
        public IReadOnlyDictionary<string, TestNamespace> TestNamespacesByName => this.namespacesByName;
        IReadOnlyCollection<ITestNamespacePart> ITestAssemblyPart.TestNamespaces => this.namespacesByName.Values;
        public bool IsWhole => true;
        public TestAssembly Whole => this;

        public TestAssembly(string path, IReadOnlyCollection<TestNamespace> testNamespaces)
        {
            Path = path;
            foreach (var testNamespace in testNamespaces)
            {
                this.namespacesByName.Add(testNamespace.Name, testNamespace);
            }
        }

        internal static class Setter
        {
            public static void SetParent(TestAssembly self, TestSuite parent)
            {
                if (self.TestSuite != null)
                {
                    throw new InvalidOperationException();
                }
                self.TestSuite = parent;
            }
        }
    }
}