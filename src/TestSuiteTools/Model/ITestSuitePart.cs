using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class ITestSuitePart : ITestSuiteGrain
    {
        public IReadOnlyCollection<ITestAssemblyPart> TestAssemblies { get; }
    }
}