using System;
using System.Collections.Generic;

namespace TestSuiteTools.Model.Builders
{
    public class AssemblyWiseTestSuiteBuilder : ITestSuiteBuilder<TestAssemblyPart>
    {
        private readonly Dictionary<string, TestAssemblyPart> assembliesByPath = new();

        public void AddItem(TestAssemblyPart assembly)
        {
            if (this.assembliesByPath.ContainsKey(assembly.Assembly.Path))
            {
                throw new InvalidOperationException($"Assembly {assembly.Assembly.Path} was already added");
            }

            this.assembliesByPath.Add(assembly.Assembly.Path, assembly);
        }

        public TestSuitePart Build()
        {
            return new TestSuitePart(this.assembliesByPath.Values);
        }
    }
}