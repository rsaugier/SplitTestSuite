using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestSuiteTools.Model;

namespace TestSuiteTools.OutputFormats
{
    public class TestCaseFilterOutputFormatter : IOutputFormatter
    {
        private readonly ILog log;
        public string FormatDisplayName => "Test case filter";

        public TestCaseFilterOutputFormatter(ILog log)
        {
            this.log = log;
        }

        public void Output(ITestSuitePart testSuitePart, Stream outputStream)
        {
            var writer = new StreamWriter(outputStream);
            List<string> filters = new();
            foreach (ITestAssemblyPart testAssembly in testSuitePart.TestAssemblies)
            {
                OutputAssemblyPart(testAssembly, filters);
            }
            writer.Write(string.Join("|", filters));
            writer.Flush();
        }

        private void OutputAssemblyPart(ITestAssemblyPart testAssembly, List<string> filters)
        {
            if (testAssembly.IsWhole)
            {
                this.log.Debug($"Assembly {testAssembly.Path} is wholly included in part");
                OutputWholeAssembly(testAssembly, filters);
            }
            else
            {
                OutputNamespacePart(testAssembly, filters);
            }
        }

        private void OutputNamespacePart(ITestAssemblyPart testAssembly, List<string> filters)
        {
            foreach (ITestNamespacePart testNamespace in testAssembly.TestNamespaces)
            {
                if (testNamespace.IsWhole)
                {
                    this.log.Debug($"Namespace {testNamespace.Name} is wholly included in part");
                    OutputWholeNamespace(testNamespace, filters);
                }
                else
                {
                    OutputClassPart(filters, testNamespace);
                }
            }
        }

        private void OutputClassPart(List<string> filters, ITestNamespacePart testNamespace)
        {
            foreach (ITestClassPart testClass in testNamespace.TestClasses)
            {
                if (testClass.IsWhole)
                {
                    this.log.Debug($"Class {testClass.Name} is wholly included in part");
                    filters.Add($"ClassName={testClass.QualifiedName}");
                }
                else
                {
                    foreach (TestMethod method in testClass.TestMethods)
                    {
                        filters.Add($"Name={method.Name}");
                    }
                }
            }
        }

        private static void OutputWholeNamespace(ITestNamespacePart testNamespace, List<string> filters)
        {
            foreach (var testClass in testNamespace.TestClasses)
            {
                filters.Add($"ClassName={testClass.QualifiedName}");
            }
        }

        private static void OutputWholeAssembly(ITestAssemblyPart testAssembly, List<string> filters)
        {
            foreach (var testClass in testAssembly.TestNamespaces.SelectMany(ns => ns.TestClasses))
            {
                filters.Add($"ClassName={testClass.QualifiedName}");
            }
        }
    }
}