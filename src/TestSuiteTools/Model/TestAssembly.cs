using System;
using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestAssembly : ITestAssemblyPart
    {
        private readonly Dictionary<string, TestNamespace> namespacesByName = new();

        public string Path { get; }
        public TestSuite Suite { get; private set; }
        public IReadOnlyCollection<ITestNamespacePart> TestNamespaces => this.namespacesByName.Values;

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
                if (self.Suite != null)
                {
                    throw new InvalidOperationException();
                }
                self.Suite = parent;
            }
        }
    }
}