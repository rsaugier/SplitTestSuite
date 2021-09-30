using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestSuite : ITestSuitePart
    {
        private readonly Dictionary<string, TestAssembly> testAssemblies = new();

        public IReadOnlyCollection<TestAssembly> TestAssemblies => this.testAssemblies.Values;
        IReadOnlyCollection<ITestAssemblyPart> ITestSuitePart.TestAssemblies => this.testAssemblies.Values;

        public TestSuite(IReadOnlyCollection<TestAssembly> testAssemblies)
        {
            foreach (var testAssembly in testAssemblies)
            {
                this.testAssemblies.Add(testAssembly.Path, testAssembly);
            }
        }
    }
}