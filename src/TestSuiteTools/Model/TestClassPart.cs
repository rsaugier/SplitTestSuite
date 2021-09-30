using System.Collections.Generic;
using System.Linq;

namespace TestSuiteTools.Model
{
    public class TestClassPart : ITestClassPart
    {
        private readonly Dictionary<string, TestMethod> testMethodsByName = new();

        public TestClass Class { get; }
        public string Name => Class.Name;
        public string QualifiedName => $"{Class.Namespace.Name}.{Name}";
        public IReadOnlyCollection<TestMethod> TestMethods => this.testMethodsByName.Values;
        public bool IsWhole { get; }

        public TestClassPart(TestClass testClass, IReadOnlyCollection<TestMethod> testMethods)
        {
            this.Class = testClass;
            foreach (var testMethod in testMethods)
            {
                this.testMethodsByName.Add(testMethod.Name, testMethod);
            }
            IsWhole = Class.TestMethods.All(ns => this.testMethodsByName.ContainsKey(ns.Name));
        }
    }
}