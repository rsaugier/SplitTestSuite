using System.Collections.Generic;
using System.Linq;

namespace TestSuiteTools.Model
{
    public class TestNamespacePart : ITestNamespacePart
    {
        private readonly Dictionary<string, ITestClassPart> testClassesByName = new();

        public TestNamespace Namespace { get; }
        public string Name => Namespace.Name;
        public IReadOnlyCollection<ITestClassPart> TestClasses => this.testClassesByName.Values;
        public bool IsWhole { get; }
        public TestNamespace Whole => Namespace;

        public TestNamespacePart(TestNamespace testNamespace, IReadOnlyCollection<ITestClassPart> methodLists)
        {
            this.Namespace = testNamespace;
            foreach (var methodList in methodLists)
            {
                this.testClassesByName.Add(methodList.Whole.Name, methodList);
            }

            IsWhole = Namespace.TestClasses.All(ns => this.testClassesByName.ContainsKey(ns.Name));
        }
    }
}