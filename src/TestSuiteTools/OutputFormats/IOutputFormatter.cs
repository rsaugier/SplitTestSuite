using System.IO;
using TestSuiteTools.Model;

namespace TestSuiteTools.OutputFormats
{
    public interface IOutputFormatter
    {
        string FormatDisplayName { get; }
        void Output(ITestSuitePart testSuitePart, Stream outputStream);
    }
}