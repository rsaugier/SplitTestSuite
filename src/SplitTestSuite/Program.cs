using System;
using System.IO;
using CommandLine;
using TestSuiteTools;
using TestSuiteTools.Discovery;
using TestSuiteTools.Model;
using TestSuiteTools.Model.Builders;
using TestSuiteTools.OutputFormats;
using TestSuiteTools.Splitting;
using TestSuiteTools.Splitting.Granularity;
using TestSuiteTools.Splitting.SplitStrategy;

namespace SplitTestSuite
{
    enum Granularity
    {
        Method,
        Class,
        Namespace,
        Assembly
    }

    class ProgramParams
    {
        public ProgramParams(
            string testSuitePath,
            int numParts,
            int? part,
            bool quiet,
            string? outTestCaseFilter,
            string? outPlainText,
            Granularity granularity)
        {
            this.TestSuitePath = testSuitePath;
            this.NumParts = numParts;
            this.Part = part;
            this.Quiet = quiet;
            this.OutTestCaseFilter = outTestCaseFilter;
            this.OutPlainText = outPlainText;
            this.Granularity = granularity;
        }

        [Value(0,
            MetaName = "test-suite-path",
            Required = true,
            HelpText = "Path to the input test suite (either a single assembly/exe or a folder with assemblies).")]
        public string TestSuitePath { get; }

        [Option('n', "num-parts", Required = true, HelpText = "The number of parts to split the test suite into.")]
        public int NumParts { get; }

        [Option(
            'p',
            "part",
            Required = false,
            HelpText = "A part index (between 0 and num-parts-1). If specified, only the specified part is output.")]
        public int? Part { get; }

        [Option('q', "quiet", Required = false, HelpText = "Don't output anything.")]
        public bool Quiet { get; }

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

    class Program
    {
        private const string PlainTextOutputFileExtension = "txt";
        private const string TestCaseFilterOutputFileExtension = "testcasefilter.txt";
        private readonly ProgramParams parameters;
        private readonly ILog _log;

        private Program(ProgramParams parameters)
        {
            this.parameters = parameters;
            this._log = parameters.Quiet ? new NullLog() : new ConsoleLog();
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ProgramParams>(args).WithParsed(
                opts =>
                {
                    var program = new Program(opts);
                    program.Run();
                });
        }

        void LogOptions()
        {
            this._log.Info(
                $"Input tests path: {this.parameters.TestSuitePath}",
                $"Number of parts: {this.parameters.NumParts}",
                $"Wanted part: {this.parameters.Part}," +
                $"Quiet: {this.parameters.Quiet}");
        }

        private void WriteOutput(TestSuitePartition testSuitePartition, IOutputFormatter formatter, string? path, string extension)
        {
            if (!Path.HasExtension(path))
            {
                path += "." + extension;
            }

            if (this.parameters.Part.HasValue)
            {
                this._log.Info($"Writing part {this.parameters.Part.Value} to {path}");
                using (var outputStream = this.OpenFile(path))
                {
                    formatter.Output(
                        testSuitePartition.Parts[this.parameters.Part.Value],
                        outputStream);
                    outputStream.Close();
                }
            }
            else
            {
                for (int part = 0; part < this.parameters.NumParts; ++part)
                {
                    string partOutputPath = this.SuffixOutputFilePath(path, part);
                    this._log.Info($"Writing part {part} to {partOutputPath}");
                    using (var outputStream = this.OpenFile(partOutputPath))
                    {
                        formatter.Output(
                            testSuitePartition.Parts[part],
                            outputStream);
                        outputStream.Close();
                    }
                }
            }
        }

        void Run()
        {
            this.LogOptions();

            this._log.Info($"Discovering tests from {this.parameters.TestSuitePath}");
            var testSuite = this.ReadTestSuite(this.parameters.TestSuitePath);

            this._log.Info($"Splitting tests in {this.parameters.NumParts} parts");
            TestSuiteSplitter splitter = this.CreateTestSuiteSplitter();
            TestSuitePartition testSuitePartition = splitter.Split(testSuite, this.parameters.NumParts);

            string? plainTextOutputFile = this.parameters.OutPlainText;
            if (plainTextOutputFile != null)
            {
                this.WriteOutput(testSuitePartition, new PlainTextOutputFormatter(), plainTextOutputFile, PlainTextOutputFileExtension);
            }

            string? testCaseFilterOutputFile = this.parameters.OutTestCaseFilter;
            if (testCaseFilterOutputFile != null)
            {
                this.WriteOutput(testSuitePartition, new TestCaseFilterOutputFormatter(), testCaseFilterOutputFile, TestCaseFilterOutputFileExtension);
            }
        }

        private TestSuiteSplitter CreateTestSuiteSplitter()
        {
            return new TestSuiteSplitter(
                new RoundRobinSplitStrategy(),
                this.CreateGranularityStrategy());
        }

        private string SuffixOutputFilePath(string outputPath, int part)
        {
            return $"{Path.GetFileNameWithoutExtension(outputPath)}.{part}{Path.GetExtension(outputPath)}";
        }

        private FileStream OpenFile(string filePath) => new(filePath, FileMode.Create);

        private TestSuite ReadTestSuite(string testSuitePath)
        {
            TestSuiteBuilder suiteBuilder = new TestSuiteBuilder();
            if (Directory.Exists(testSuitePath))
            {
                var crawler = new TestsFolderCrawler(testSuitePath, this._log);
                crawler.BuildTestSuite(suiteBuilder);
            }
            else if (File.Exists(testSuitePath))
            {
                var reader = new TestsAssemblyReader(testSuitePath, this._log);
                reader.BuildTestSuite(suiteBuilder);
            }
            return suiteBuilder.Build();
        }

        private IGranularityStrategy CreateGranularityStrategy()
        {
            switch (this.parameters.Granularity)
            {
                case Granularity.Method:
                    return new MethodGranularityStrategy();
                case Granularity.Class:
                    return new ClassGranularityStrategy();
                case Granularity.Namespace:
                    return new NamespaceGranularityStrategy();
                case Granularity.Assembly:
                    return new AssemblyGranularityStrategy();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}