using System;
using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestClass : ITestClassPart
    {
        private readonly Dictionary<string, TestMethod> testMethodsByName = new();

        public string Name { get; }
        public string QualifiedName => $"{TestNamespace.Name}.{Name}";
        public TestNamespace TestNamespace { get; private set; }
        public IReadOnlyCollection<TestMethod> TestMethods => this.testMethodsByName.Values;
        public IReadOnlyDictionary<string, TestMethod> TestMethodsByName => this.testMethodsByName;
        public bool IsWhole => true;
        public TestClass Whole => this;

        public TestClass(string name, IReadOnlyCollection<TestMethod> testMethods)
        {
            Name = name;
            foreach (var testMethod in testMethods)
            {
                this.testMethodsByName.Add(testMethod.Name, testMethod);
            }
        }

        internal static class Setter
        {
            public static void SetParent(TestClass self, TestNamespace parent)
            {
                if (self.TestNamespace != null)
                {
                    throw new InvalidOperationException();
                }
                self.TestNamespace = parent;
            }
        }
    }
}