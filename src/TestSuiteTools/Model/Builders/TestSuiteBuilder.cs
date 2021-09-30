using System;
using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model.Builders.MutableModel;

namespace TestSuiteTools.Model.Builders
{
    public class TestSuiteBuilder : IPartBuilder<TestMethod>
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
            this.AddTestMethod(method.Class.TestNamespace.Assembly.Path,
                method.Class.TestNamespace.Name,
                method.Class.Name,
                method.Name);
        }

        ITestSuitePart IPartBuilder<TestMethod>.Build()
        {
            return Build();
        }

        public TestSuite Build()
        {
            List<TestAssembly> testAssemblies = this.mutableTestSuite.Assemblies.Values.Select(
                a =>
                {
                    List<TestNamespace> testNamespaces = a.Namespaces.Values.Select(
                        ns =>
                        {
                            List<TestClass> testClasses = ns.Classes.Values.Select(
                                cl =>
                                {
                                    var testMethods = cl.Methods.Select(m => new TestMethod(m)).ToList();
                                    var testClass = new TestClass(cl.Name, testMethods);
                                    foreach (var testMethod in testMethods)
                                    {
                                        TestMethod.Setter.SetParent(testMethod, testClass);
                                    }
                                    return testClass;
                                }).ToList();
                            var testNamespace = new TestNamespace(
                                ns.Name,
                                testClasses);
                            foreach (var testClass in testClasses)
                            {
                                TestClass.Setter.SetParent(testClass, testNamespace);
                            }
                            return testNamespace;
                        }).ToList();
                    var testAssembly = new TestAssembly(
                        a.Path,
                        testNamespaces);
                    foreach (var testNamespace in testNamespaces)
                    {
                        TestNamespace.Setter.SetParent(testNamespace, testAssembly);
                    }
                    return testAssembly;
                }).ToList();
            var testSuite = new TestSuite(testAssemblies);
            foreach (var testAssembly in testAssemblies)
            {
                TestAssembly.Setter.SetParent(testAssembly, testSuite);
            }
            return testSuite;
        }
    }
}