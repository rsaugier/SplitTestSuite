using System;
using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model.Builders.MutableModel;

namespace TestSuiteTools.Model.Builders
{
    public class ClassWiseTestSuitePartBuilder : IPartBuilder<ITestClassPart>
    {
        private readonly TestSuite testSuite;
        private readonly MutableTestSuite mutableTestSuite = new();
        private readonly Dictionary<MutableTestClass, ITestClassPart> partByClass = new();

        public ClassWiseTestSuitePartBuilder(TestSuite testSuite)
        {
            this.testSuite = testSuite;
        }

        public void AddItem(ITestClassPart item)
        {
            MutableTestAssembly assembly = this.mutableTestSuite.GetOrAddAssembly(item.Whole.TestNamespace.Assembly.Path);
            MutableTestNamespace ns = assembly.GetOrAddNamespace(item.Whole.TestNamespace.Name);
            MutableTestClass cl = ns.GetOrAddClass(item.Whole.Name);
            if (this.partByClass.ContainsKey(cl))
            {
                throw new InvalidOperationException($"Class {cl.Name} was already added");
            }
            this.partByClass[cl] = item;
        }

        public ITestSuitePart Build()
        {
            if (!this.partByClass.Any())
            {
                return new TestSuitePart(this.testSuite, new List<TestAssemblyPart>());
            }

            return new TestSuitePart(
                this.testSuite,
                this.mutableTestSuite.Assemblies.Values.Select(
                    a =>
                    {
                        var testAssembly = this.testSuite.TestAssembliesByPath[a.Path];
                        return new TestAssemblyPart(
                            testAssembly,
                            a.Namespaces.Values.Select(
                                ns =>
                                {
                                    return new TestNamespacePart(
                                        testAssembly.TestNamespacesByName[ns.Name],
                                        ns.Classes.Values.Select(
                                            cl => this.partByClass[cl]).ToList());
                                }).ToList());
                    }).ToList());
        }
    }
}