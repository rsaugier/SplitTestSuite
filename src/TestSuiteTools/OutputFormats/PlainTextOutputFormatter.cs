using System.IO;
using TestSuiteTools.Model;

namespace TestSuiteTools.OutputFormats
{
    public class PlainTextOutputFormatter : IOutputFormatter
    {
        public void Output(TestSuitePart testSuitePart, Stream outputStream)
        {
            using (var writer = new StreamWriter(outputStream))
            {
                foreach (TestAssemblyPart testAssembly in testSuitePart.TestAssemblies)
                {
                    writer.WriteLine(testAssembly.Path);
                    foreach (TestNamespacePart ns in testAssembly.TestNamespaces)
                    {
                        foreach (TestClassPart testClass in ns.TestClasses)
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
            }
        }
    }
}