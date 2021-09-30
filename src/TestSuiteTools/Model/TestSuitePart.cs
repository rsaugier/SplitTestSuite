using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestSuitePart : ITestSuitePart
    {
        private readonly Dictionary<string, TestAssemblyPart> testAssemblies = new();

        public TestSuite TestSuite { get; }
        public IReadOnlyCollection<ITestAssemblyPart> TestAssemblies => this.testAssemblies.Values;

        public TestSuitePart(TestSuite testSuite, IReadOnlyCollection<TestAssemblyPart> testNamespaceLists)
        {
            TestSuite = testSuite;
            foreach (var testNamespaceList in testNamespaceLists)
            {
                this.testAssemblies.Add(testNamespaceList.Assembly.Path, testNamespaceList);
            }
        }
    }
}