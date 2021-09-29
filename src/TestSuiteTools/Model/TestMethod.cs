using System;

namespace TestSuiteTools.Model
{
    public class TestMethod : ITestSuiteGrain
    {
        public string Name { get; }
        public TestClass Class { get; private set; }

        public TestMethod(string name)
        {
            this.Name = name;
        }

        internal static class Setter
        {
            public static void SetParent(TestMethod self, TestClass parent)
            {
                if (self.Class != null)
                {
                    throw new InvalidOperationException();
                }
                self.Class = parent;
            }
        }
    }
}