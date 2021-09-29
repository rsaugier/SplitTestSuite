using System;
using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model.Builders.MutableModel;

namespace TestSuiteTools.Model.Builders
{
    public class NamespaceWiseTestSuitePartBuilder : IPartBuilder<TestNamespacePart>
    {
        private readonly TestSuite testSuite;
        private readonly MutableTestSuite mutableTestSuite = new();
        private readonly Dictionary<MutableTestNamespace, TestNamespacePart> partByNamespace = new();

        public NamespaceWiseTestSuitePartBuilder(TestSuite testSuite)
        {
            this.testSuite = testSuite;
        }

        public void AddItem(TestNamespacePart item)
        {
            MutableTestAssembly assembly = this.mutableTestSuite.GetOrAddAssembly(item.Namespace.Assembly.Path);
            MutableTestNamespace ns = assembly.GetOrAddNamespace(item.Namespace.Name);
            if (this.partByNamespace.ContainsKey(ns))
            {
                throw new InvalidOperationException($"Namespace {ns.Name} was already added");
            }
            this.partByNamespace[ns] = item;
        }

        public ITestSuitePart Build()
        {
            if (!this.partByNamespace.Any())
            {
                return new TestSuitePart(this.testSuite, new List<TestAssemblyPart>());
            }

            var testAssembly = this.partByNamespace.Values.First().Namespace.Assembly;
            return new TestSuitePart(
                this.testSuite,
                this.mutableTestSuite.Assemblies.Values.Select(
                    a =>
                    {
                        return new TestAssemblyPart(
                            testAssembly,
                            a.Namespaces.Values.Select(ns => this.partByNamespace[ns]).ToList());
                    }).ToList());
        }
    }
}