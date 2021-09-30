using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestSuitePart : ITestSuitePart
    {
        private readonly Dictionary<string, ITestAssemblyPart> testAssemblies = new();

        public TestSuite TestSuite { get; }
        public IReadOnlyCollection<ITestAssemblyPart> TestAssemblies => this.testAssemblies.Values;
        public TestSuite Whole => TestSuite;

        public TestSuitePart(TestSuite testSuite, IReadOnlyCollection<ITestAssemblyPart> testAssemblyParts)
        {
            TestSuite = testSuite;
            foreach (var testAssemblyPart in testAssemblyParts)
            {
                this.testAssemblies.Add(testAssemblyPart.Whole.Path, testAssemblyPart);
            }
        }
    }
}