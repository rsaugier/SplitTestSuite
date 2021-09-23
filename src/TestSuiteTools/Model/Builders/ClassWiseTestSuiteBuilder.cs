using System;
using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model.Mutable;

namespace TestSuiteTools.Model.Builders
{
    public class ClassWiseTestSuiteBuilder : ITestSuiteBuilder<TestClassPart>
    {
        private readonly MutableTestSuite mutableTestSuite = new();
        private readonly Dictionary<MutableTestClass, TestClassPart> partByClass = new();

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

        public TestSuite Build()
        {
            return new TestSuite(
                this.mutableTestSuite.Assemblies.Values.Select(
                    a =>
                    {
                        var asm = new TestAssembly(a.Path);
                        return new TestAssemblyPart(
                            asm,
                            a.Namespaces.Values.Select(
                                ns =>
                                {
                                    var tns = new TestNamespace(ns.Name, asm);
                                    return new TestNamespacePart(
                                        tns,
                                        ns.Classes.Values.Select(
                                            cl => this.partByClass[cl]).ToList());
                                }).ToList());
                    }).ToList());
        }
    }
}