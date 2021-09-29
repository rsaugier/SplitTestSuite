using System.Linq;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;
using Xunit;

namespace TestSuiteTools.UnitTests
{
    public class MethodWiseTestSuiteBuilderTest
    {
        [Fact]
        public void TestSuiteBuilder_Nominal()
        {
            // arrange
            var builder = new TestSuiteBuilder();
            builder.AddTestMethod("/path/to/Assembly1.dll", "Namespace1", "Class1", "Method1");
            builder.AddTestMethod("/path/to/Assembly1.dll", "Namespace1", "Class1", "Method2");
            builder.AddTestMethod("/path/to/Assembly1.dll", "Namespace1", "Class2", "Method1");
            builder.AddTestMethod("/path/to/Assembly1.dll", "Namespace1.SubNamespace", "Class3", "Method1");
            builder.AddTestMethod("/path/to/Assembly2.dll", "Namespace1", "Class1", "Method1");
            builder.AddTestMethod("/path/to/Assembly2.dll", "Namespace1", "Class4", "Method4");

            // act
            TestSuite suite = builder.Build();

            // assert
            Assert.Equal(2, suite.TestAssemblies.Count);

            // check assemblies
            var asm1 = suite.TestAssemblies.First();
            Assert.Equal("/path/to/Assembly1.dll", asm1.Path);
            Assert.Equal(2, asm1.TestNamespaces.Count);

            var asm2 = suite.TestAssemblies.Last();
            Assert.Equal("/path/to/Assembly2.dll", asm2.Path);
            Assert.Equal(1, asm2.TestNamespaces.Count);

            // check namespaces
            var asm1ns1 = asm1.TestNamespaces.First();
            Assert.Equal("Namespace1", asm1ns1.Name);
            Assert.Equal(2, asm1ns1.TestClasses.Count);

            var asm1ns2 = asm1.TestNamespaces.Last();
            Assert.Equal("Namespace1.SubNamespace", asm1ns2.Name);
            Assert.Equal(1, asm1ns2.TestClasses.Count);

            var asm2ns1 = asm2.TestNamespaces.Last();
            Assert.Equal("Namespace1", asm2ns1.Name);
            Assert.Equal(2, asm2ns1.TestClasses.Count);

            // check classes
            var asm1ns1class1 = asm1ns1.TestClasses.First();
            Assert.Equal("Class1", asm1ns1class1.Name);
            Assert.Equal(2, asm1ns1class1.TestMethods.Count);

            var asm1ns1class2 = asm1ns1.TestClasses.Last();
            Assert.Equal("Class2", asm1ns1class2.Name);
            Assert.Equal(1, asm1ns1class2.TestMethods.Count);

            var asm1ns2class3 = asm1ns2.TestClasses.First();
            Assert.Equal("Class3", asm1ns2class3.Name);
            Assert.Equal(1, asm1ns2class3.TestMethods.Count);

            var asm2ns1class1 = asm2ns1.TestClasses.First();
            Assert.Equal("Class1", asm2ns1class1.Name);
            Assert.Equal(1, asm2ns1class1.TestMethods.Count);

            var asm2ns1class2 = asm2ns1.TestClasses.Last();
            Assert.Equal("Class4", asm2ns1class2.Name);
            Assert.Equal(1, asm2ns1class2.TestMethods.Count);

            // check methods
            Assert.Equal("Method1", asm1ns1class1.TestMethods.First().Name);
            Assert.Equal("Method2", asm1ns1class1.TestMethods.Last().Name);
            Assert.Equal("Method1", asm1ns1class2.TestMethods.First().Name);
            Assert.Equal("Method1", asm1ns2class3.TestMethods.First().Name);
            Assert.Equal("Method1", asm2ns1class1.TestMethods.First().Name);
            Assert.Equal("Method4", asm2ns1class2.TestMethods.First().Name);
        }
    }
}