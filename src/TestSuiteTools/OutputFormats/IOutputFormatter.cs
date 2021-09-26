using System.IO;
using TestSuiteTools.Model;

namespace TestSuiteTools.OutputFormats
{
    public interface IOutputFormatter
    {
        void Output(TestSuitePart testSuitePart, Stream outputStream);
    }
}