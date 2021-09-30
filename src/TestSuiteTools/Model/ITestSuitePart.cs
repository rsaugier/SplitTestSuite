using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public interface ITestSuitePart : ITestSuiteGrain
    {
        public IReadOnlyCollection<ITestAssemblyPart> TestAssemblies { get; }
        public TestSuite Whole { get; }
    }
}