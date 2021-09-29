using System;
using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestNamespace : ITestNamespacePart
    {
        private readonly Dictionary<string, TestClass> testClassesByName = new();

        public string Name { get; }
        public TestAssembly Assembly { get; private set; }
        public IReadOnlyCollection<ITestClassPart> TestClasses => this.testClassesByName.Values;

        internal static class Setter
        {
            public static void SetParent(TestNamespace self, TestAssembly parent)
            {
                if (self.Assembly != null)
                {
                    throw new InvalidOperationException();
                }
                self.Assembly = parent;
            }
        }

        public TestNamespace(string name, IReadOnlyCollection<TestClass> testClasses)
        {
            this.Name = name;
            foreach (var testClass in testClasses)
            {
                this.testClassesByName.Add(testClass.Name, testClass);
            }
        }
    }
}