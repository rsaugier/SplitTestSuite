using System.Collections.Generic;
using CommandLine;

namespace SplitTestSuite
{
    class ProgramParams
    {
        public ProgramParams(IEnumerable<string> testSuiteFilesAndFolders,
            int numParts,
            int? part,
            LogLevel logLevel,
            bool quiet,
            bool recurse,
            string? outTestCaseFilter,
            string? outPlainText,
            Granularity granularity)
        {
            this.TestSuiteFilesAndFolders = testSuiteFilesAndFolders;
            this.NumParts = numParts;
            this.Part = part;
            this.Quiet = quiet;
            this.Recurse = recurse;
            this.OutTestCaseFilter = outTestCaseFilter;
            this.OutPlainText = outPlainText;
            this.Granularity = granularity;
            this.LogLevel = logLevel;
        }

        [Value(0,
            MetaName = "test-suite-path",
            Required = true,
            HelpText = "Path to the input test suite (either a single assembly/exe or a folder with assemblies).")]
        public IEnumerable<string> TestSuiteFilesAndFolders { get; }

        [Option('n', "num-parts", Required = true, HelpText = "The number of parts to split the test suite into.")]
        public int NumParts { get; }

        [Option(
            'p',
            "part",
            Required = false,
            HelpText = "A part index (between 0 and num-parts-1). If specified, only the specified part is output.")]
        public int? Part { get; }

        [Option('l', "log-level", Required = false, Default = LogLevel.Info, HelpText = "Don't output anything.")]
        public LogLevel LogLevel { get; }

        [Option('q', "quiet", Required = false, HelpText = "Don't output anything.")]
        public bool Quiet { get; }

        [Option('r', "recurse", Required = false, HelpText = "Recursively find test assemblies in sub-directories.")]
        public bool Recurse { get; }

        [Option("out-filter",
            Required = false,
            HelpText = "Output as TestCaseFilter",
            MetaValue = "FILENAME",
            Group = "output-formats")]
        public string? OutTestCaseFilter { get; }

        [Option("out-plain",
            Required = false,
            HelpText = "Output as plain text list",
            MetaValue = "FILENAME",
            Group = "output-formats")]
        public string? OutPlainText { get; }

        [Option(
            'g',
            "granularity",
            Required = false,
            HelpText = "The granularity of the split.")]
        public Granularity Granularity { get; }
    };
}