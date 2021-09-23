using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    public class TestSuitePartition
    {
        public IReadOnlyList<TestSuite> Parts { get; }
        public int NumParts => this.Parts.Count;

        public TestSuitePartition(IReadOnlyCollection<TestSuite> parts)
        {
            this.Parts = new List<TestSuite>(parts);
        }
    }
}