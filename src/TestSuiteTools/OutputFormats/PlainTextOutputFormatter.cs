using System.IO;
using TestSuiteTools.Model;

namespace TestSuiteTools.OutputFormats
{
    public class PlainTextOutputFormatter : IOutputFormatter
    {
        public string FormatDisplayName => "Plain text tests list";

        public void Output(ITestSuitePart testSuitePart, Stream outputStream)
        {
            var writer = new StreamWriter(outputStream);
            foreach (ITestAssemblyPart testAssembly in testSuitePart.TestAssemblies)
            {
                writer.WriteLine(testAssembly.Path);
                foreach (ITestNamespacePart ns in testAssembly.TestNamespaces)
                {
                    foreach (ITestClassPart testClass in ns.TestClasses)
                    {
                        writer.WriteLine($"  {ns.Name}.{testClass.Name}");
                        foreach (TestMethod method in testClass.TestMethods)
                        {
                            writer.WriteLine("    " + method.Name);
                        }

                        writer.WriteLine();
                    }
                }
            }
            writer.Flush();
        }
    }
}