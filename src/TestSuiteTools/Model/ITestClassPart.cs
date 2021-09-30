using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public interface ITestClassPart : ITestSuiteGrain
    {
        public IReadOnlyCollection<TestMethod> TestMethods { get ; }
        public string Name { get; }
        public string QualifiedName { get; }
        public bool IsWhole { get; }
        public TestClass Whole { get; }
    }
}