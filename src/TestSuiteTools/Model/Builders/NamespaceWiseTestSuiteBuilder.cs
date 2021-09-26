using System;
using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model.Builders.MutableModel;

namespace TestSuiteTools.Model.Builders
{
    public class NamespaceWiseTestSuiteBuilder : ITestSuiteBuilder<TestNamespacePart>
    {
        private readonly MutableTestSuite mutableTestSuite = new();
        private readonly Dictionary<MutableTestNamespace, TestNamespacePart> partByNamespace = new();

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

        public TestSuite Build()
        {
            return new TestSuite(
                this.mutableTestSuite.Assemblies.Values.Select(
                    a =>
                    {
                        var asm = new TestAssembly(a.Path);
                        return new TestAssemblyPart(
                            asm,
                            a.Namespaces.Values.Select(ns => this.partByNamespace[ns]).ToList());
                    }).ToList());
        }
    }
}