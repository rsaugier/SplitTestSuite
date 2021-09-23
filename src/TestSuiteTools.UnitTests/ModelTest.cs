using System.Collections.Generic;
using Xunit;
using TestSuiteTools.Model;

namespace TestSuiteTools.UnitTests
{
    public class ModelTest
    {
        [Fact]
        public void CreateSimpleTestSuite()
        {
            var testAssembly = new TestAssembly("/path/to/Assembly1.dll");
            var testNamespace = new TestNamespace("namespace1", testAssembly);
            var testClass = new TestClass("class1", testNamespace);
            var testNamespaceLists = new List<TestAssemblyPart>()
            {
                new TestAssemblyPart(testAssembly,
                    new List<TestNamespacePart>()
                    {
                        new TestNamespacePart(testNamespace,
                            new List<TestClassPart>()
                            {
                                new TestClassPart(testClass,
                                    new List<TestMethod>()
                                    {
                                        new TestMethod("method1", testClass)
                                    })
                            })
                    })
            };

            var _ = new TestSuite(testNamespaceLists);
        }
    }
}