using System.IO;
using SplitTestSuite.TestSuiteTools.Model;

namespace SplitTestSuite.TestSuiteTools.OutputFormats
{
    internal interface IOutputFormatter
    {
        void Output(TestSuite testSuitePart, Stream outputStream);
    }
}