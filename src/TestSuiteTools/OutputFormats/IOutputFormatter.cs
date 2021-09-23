using System.IO;
using TestSuiteTools.Model;

namespace TestSuiteTools.OutputFormats
{
    public interface IOutputFormatter
    {
        void Output(TestSuite testSuitePart, Stream outputStream);
    }
}