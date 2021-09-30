using System;
using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestNamespace : ITestNamespacePart
    {
        private readonly Dictionary<string, TestClass> testClassesByName = new();

        public string Name { get; }
        public TestAssembly Assembly { get; private set; }
        public IReadOnlyCollection<TestClass> TestClasses => this.testClassesByName.Values;
        public IReadOnlyDictionary<string, TestClass> TestClassesByName => this.testClassesByName;
        IReadOnlyCollection<ITestClassPart> ITestNamespacePart.TestClasses => this.testClassesByName.Values;
        public bool IsWhole => true;
        public TestNamespace Whole => this;

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