using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSuiteTools.Model.Builders
{
    public class AssemblyWiseTestSuitePartBuilder : IPartBuilder<ITestAssemblyPart>
    {
        private readonly Dictionary<string, ITestAssemblyPart> assembliesByPath = new();
        private readonly TestSuite testSuite;

        public AssemblyWiseTestSuitePartBuilder(TestSuite testSuite)
        {
            this.testSuite = testSuite;
        }

        public void AddItem(ITestAssemblyPart assembly)
        {
            if (this.assembliesByPath.ContainsKey(assembly.Path))
            {
                throw new InvalidOperationException($"Assembly {assembly.Path} was already added");
            }

            this.assembliesByPath.Add(assembly.Path, assembly);
        }

        public ITestSuitePart Build()
        {
            if (!this.assembliesByPath.Any())
            {
                return new TestSuitePart(this.testSuite, new List<TestAssemblyPart>());
            }

            return new TestSuitePart(
                this.testSuite,
                this.assembliesByPath.Values);
        }
    }
}