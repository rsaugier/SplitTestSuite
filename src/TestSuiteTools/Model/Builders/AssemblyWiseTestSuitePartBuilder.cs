using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSuiteTools.Model.Builders
{
    public class AssemblyWiseTestSuitePartBuilder : IPartBuilder<TestAssemblyPart>
    {
        private readonly Dictionary<string, TestAssemblyPart> assembliesByPath = new();
        private readonly TestSuite testSuite;

        public AssemblyWiseTestSuitePartBuilder(TestSuite testSuite)
        {
            this.testSuite = testSuite;
        }

        public void AddItem(TestAssemblyPart assembly)
        {
            if (this.assembliesByPath.ContainsKey(assembly.Assembly.Path))
            {
                throw new InvalidOperationException($"Assembly {assembly.Assembly.Path} was already added");
            }

            this.assembliesByPath.Add(assembly.Assembly.Path, assembly);
        }

        public ITestSuitePart Build()
        {
            if (!this.assembliesByPath.Any())
            {
                return new TestSuitePart(this.testSuite, new List<TestAssemblyPart>());
            }

            return new TestSuitePart(
                this.assembliesByPath.Values.First().Assembly.Suite,
                this.assembliesByPath.Values);
        }
    }
}