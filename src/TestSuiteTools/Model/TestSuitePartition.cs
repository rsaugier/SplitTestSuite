using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestSuitePartition
    {
        public IReadOnlyList<TestSuitePart> Parts { get; }
        public int NumParts => this.Parts.Count;

        public TestSuitePartition(IReadOnlyCollection<TestSuitePart> parts)
        {
            this.Parts = new List<TestSuitePart>(parts);
        }
    }
}