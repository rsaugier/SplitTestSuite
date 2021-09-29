using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestSuitePartition
    {
        public IReadOnlyList<ITestSuitePart> Parts { get; }
        public int NumParts => this.Parts.Count;

        public TestSuitePartition(IReadOnlyCollection<ITestSuitePart> parts)
        {
            this.Parts = new List<ITestSuitePart>(parts);
        }
    }
}