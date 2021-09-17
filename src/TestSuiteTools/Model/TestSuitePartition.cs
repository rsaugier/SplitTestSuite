using System.Collections.Generic;

namespace SplitTestSuite.TestSuiteTools.Model
{
    internal class TestSuitePartition
    {
        public IReadOnlyList<TestSuite> Parts { get; }
        public int NumParts => this.Parts.Count;

        public TestSuitePartition(IReadOnlyCollection<TestSuite> parts)
        {
            this.Parts = new List<TestSuite>(parts);
        }
    }
}