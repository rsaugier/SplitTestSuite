using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;
using Xunit;

namespace TestSuiteTools.UnitTests
{
    public class MethodWiseTestSuiteBuilderTest
    {
        [Fact]
        public void AddTestMethod()
        {
            var builder = new MethodWiseTestSuiteBuilder();
            builder.AddTestMethod("/path/to/Assembly1.dll", "namespace1", "class1", "method1");
            builder.AddTestMethod("/path/to/Assembly2.dll", "namespace2", "class2", "method1");
            TestSuite _ = builder.Build();
        }
    }
}