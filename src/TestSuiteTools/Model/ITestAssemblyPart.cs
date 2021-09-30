using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public interface ITestAssemblyPart : ITestSuiteGrain
    {
        public IReadOnlyCollection<ITestNamespacePart> TestNamespaces { get; }
        public string Path { get; }
        public bool IsWhole { get; }
    }
}