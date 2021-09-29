using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestClassPart : ITestClassPart
    {
        private readonly Dictionary<string, TestMethod> testMethodsByName = new();

        public TestClass Class { get; }
        public string Name => Class.Name;
        public IReadOnlyCollection<TestMethod> TestMethods => this.testMethodsByName.Values;

        public TestClassPart(TestClass testClass, IReadOnlyCollection<TestMethod> testMethods)
        {
            this.Class = testClass;
            foreach (var testMethod in testMethods)
            {
                this.testMethodsByName.Add(testMethod.Name, testMethod);
            }
        }
    }
}