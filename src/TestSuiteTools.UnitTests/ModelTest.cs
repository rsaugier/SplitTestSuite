using Xunit;
using System.Collections.Generic;
using System.Linq;
using TestSuiteTools.Model;

namespace TestSuiteTools.UnitTests
{
    public class ModelTest
    {
        [Fact]
        [Trait("Category", "Example")]
        public void CreateSimpleTestSuite()
        {
            // Create a simple test suite.
            // This is not the simplest way to do it: see the builders instead.

            var testAssembly = new TestAssembly("/path/to/Assembly1.dll");
            var testNamespace = new TestNamespace("namespace1", testAssembly);
            var testClass = new TestClass("class1", testNamespace);
            var testMethod = new TestMethod("method1", testClass);
            var testClassPart = new TestClassPart(testClass,
                new List<TestMethod>()
                {
                    testMethod
                });
            var testNamespacePart = new TestNamespacePart(testNamespace,
                new List<TestClassPart>()
                {
                    testClassPart
                });
            var testAssemblyPart = new TestAssemblyPart(testAssembly,
                new List<TestNamespacePart>()
                {
                    testNamespacePart
                });
            var testNamespaceLists = new List<TestAssemblyPart>()
            {
                testAssemblyPart
            };

            var testSuite = new TestSuite(testNamespaceLists);

            Assert.Equal(1, testSuite.TestAssemblies.Count);
            TestAssemblyPart asmPart = testSuite.TestAssemblies.Single();
            Assert.Same(testAssemblyPart, asmPart);

            Assert.Equal(1, asmPart.TestNamespaces.Count);
            TestNamespacePart nsPart = asmPart.TestNamespaces.Single();
            Assert.Same(testNamespacePart, nsPart);

            Assert.Equal(1, nsPart.TestClasses.Count);
            TestClassPart classPart = nsPart.TestClasses.Single();
            Assert.Same(testClassPart, classPart);

            Assert.Equal(1, classPart.TestMethods.Count);
            Assert.Same(testMethod, classPart.TestMethods.Single());
        }
    }
}