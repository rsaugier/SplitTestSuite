using System.IO;
using TestSuiteTools.Model;

namespace TestSuiteTools.OutputFormats
{
    public interface IOutputFormatter
    {
        void Output(ITestSuitePart testSuitePart, Stream outputStream);
    }
}