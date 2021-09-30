using System.IO;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;
using TestSuiteTools.OutputFormats;
using Xunit;

namespace TestSuiteTools.UnitTests
{
    public class TestCaseFilterOutputFormatterTest
    {
        TestSuite CreateTestSuite()
        {
            var builder = new TestSuiteBuilder();
            builder.AddTestMethod("foo.dll", "MyNamespace", "MyClassA", "MyMethod1");
            builder.AddTestMethod("foo.dll", "MyNamespace", "MyClassA", "MyMethod2");
            builder.AddTestMethod("bar.dll", "MyNamespace", "MyClassB", "MyMethod1");
            return builder.Build();
        }

        [Fact]
        void OutputTestSuiteToTestFilterFormat()
        {
            // setup
            ITestSuitePart suite = CreateTestSuite();
            var formatter = CreateFormatter();

            // act
            var contents = FormatSuiteToString(formatter, suite);

            // assert
            string expected = "ClassName=MyNamespace.MyClassA|" +
                              "ClassName=MyNamespace.MyClassB";
            Assert.Equal(expected, contents);
        }

        private static string FormatSuiteToString(IOutputFormatter formatter, ITestSuitePart suite)
        {
            using (var stream = new MemoryStream())
            {
                formatter.Output(suite, stream);
                stream.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(stream);
                string contents = reader.ReadToEnd();
                return contents;
            }
        }

        private IOutputFormatter CreateFormatter()
        {
            return new TestCaseFilterOutputFormatter();
        }
    }
}