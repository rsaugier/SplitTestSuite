using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public interface ITestNamespacePart : ITestSuiteGrain
    {
        public IReadOnlyCollection<ITestClassPart> TestClasses { get; }
        public string Name { get; }
        public bool IsWhole { get; }
    }
}