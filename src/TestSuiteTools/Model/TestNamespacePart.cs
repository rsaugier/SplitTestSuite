using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    /// <summary>
    /// An immutable class representing a test namespace.
    /// (ie a namespace within a test assembly).
    /// </summary>
    public class TestNamespacePart : ITestSuitePart
    {
        private readonly Dictionary<string, TestClassPart> testClassesByName = new();

        public TestNamespace Namespace { get; }
        public string Name => Namespace.Name;
        public IReadOnlyCollection<TestClassPart> TestClasses => this.testClassesByName.Values;

        public TestNamespacePart(TestNamespace testNamespace, IReadOnlyCollection<TestClassPart> methodLists)
        {
            this.Namespace = testNamespace;
            foreach (var methodList in methodLists)
            {
                this.testClassesByName.Add(methodList.Class.Name, methodList);
            }
        }
    }
}