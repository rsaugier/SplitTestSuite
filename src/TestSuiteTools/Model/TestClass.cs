using System;
using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestClass : ITestClassPart
    {
        private readonly Dictionary<string, TestMethod> testMethodsByName = new();

        public string Name { get; }
        public TestNamespace Namespace { get; private set; }
        public IReadOnlyCollection<TestMethod> TestMethods => this.testMethodsByName.Values;

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
                if (self.Namespace != null)
                {
                    throw new InvalidOperationException();
                }
                self.Namespace = parent;
            }
        }
    }
}