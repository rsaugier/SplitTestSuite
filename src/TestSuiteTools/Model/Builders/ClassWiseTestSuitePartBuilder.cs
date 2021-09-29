using System;
using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model.Builders.MutableModel;

namespace TestSuiteTools.Model.Builders
{
    public class ClassWiseTestSuitePartBuilder : IPartBuilder<TestClassPart>
    {
        private readonly TestSuite testSuite;
        private readonly MutableTestSuite mutableTestSuite = new();
        private readonly Dictionary<MutableTestClass, TestClassPart> partByClass = new();

        public ClassWiseTestSuitePartBuilder(TestSuite testSuite)
        {
            this.testSuite = testSuite;
        }

        public void AddItem(TestClassPart item)
        {
            MutableTestAssembly assembly = this.mutableTestSuite.GetOrAddAssembly(item.Class.Namespace.Assembly.Path);
            MutableTestNamespace ns = assembly.GetOrAddNamespace(item.Class.Namespace.Name);
            MutableTestClass cl = ns.GetOrAddClass(item.Class.Name);
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

            var testNamespace = this.partByClass.Values.First().Class.Namespace;
            return new TestSuitePart(
                this.testSuite,
                this.mutableTestSuite.Assemblies.Values.Select(
                    a =>
                    {
                        return new TestAssemblyPart(
                            testNamespace.Assembly,
                            a.Namespaces.Values.Select(
                                ns =>
                                {
                                    return new TestNamespacePart(
                                        testNamespace,
                                        ns.Classes.Values.Select(
                                            cl => this.partByClass[cl]).ToList());
                                }).ToList());
                    }).ToList());
        }
    }
}