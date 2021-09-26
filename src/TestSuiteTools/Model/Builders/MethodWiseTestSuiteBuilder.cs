using System;
using System.Linq;
using TestSuiteTools.Model.Builders.MutableModel;

namespace TestSuiteTools.Model.Builders
{
    public class MethodWiseTestSuiteBuilder : ITestSuiteBuilder<TestMethod>
    {
        private readonly MutableTestSuite mutableTestSuite = new();

        public void AddTestMethod(string assemblyPath, string @namespace, string testClassName, string testMethodName)
        {
            MutableTestAssembly assembly = this.mutableTestSuite.GetOrAddAssembly(assemblyPath);
            MutableTestNamespace ns = assembly.GetOrAddNamespace(@namespace);
            MutableTestClass cl = ns.GetOrAddClass(testClassName);
            bool added = cl.TryAddMethod(testMethodName);
            if (!added)
            {
                throw new InvalidOperationException($"Test method '{@namespace}.{testClassName}.{testMethodName}' has already been added (from assembly {assemblyPath})");
            }
        }

        public void AddItem(TestMethod method)
        {
            this.AddTestMethod(method.Class.Namespace.Assembly.Path,
                          method.Class.Namespace.Name,
                          method.Class.Name,
                          method.Name);
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
                                            cl =>
                                            {
                                                var klass = new TestClass(cl.Name, tns);
                                                return new TestClassPart(
                                                    klass,
                                                    cl.Methods.Select(
                                                            m => new TestMethod(m, klass))
                                                        .ToList());
                                            }).ToList());
                                }).ToList());
                    }).ToList());
        }
    }
}