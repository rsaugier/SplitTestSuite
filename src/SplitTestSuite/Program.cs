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
    enum OutputFormat
    {
        PlainTextTestList,
        OrderedTest
    }

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
            string outputPath,
            bool quiet,
            OutputFormat outputFormat,
            Granularity granularity)
        {
            this.TestSuitePath = testSuitePath;
            this.NumParts = numParts;
            this.Part = part;
            this.OutputPath = outputPath;
            this.Quiet = quiet;
            this.OutputFormat = outputFormat;
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

        [Option(
            'o',
            "output",
            Required = true,
            HelpText = "Path to the output file."
                + " If multiple parts are output, the output file name will be suffixed with a dot and the part number,"
                + " just before its extension.")]
        public string OutputPath { get; }

        [Option('q', "quiet", Required = false, HelpText = "Don't output anything.")]
        public bool Quiet { get; }

        [Option(
            'f',
            "format",
            Required = false,
            HelpText = "The output file format. If not specified, will be deduced from output file extension.")]
        public OutputFormat OutputFormat { get; }

        [Option(
            'g',
            "granularity",
            Required = false,
            HelpText = "The granularity of the split.")]
        public Granularity Granularity { get; }
    };

    class Program
    {
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

        void SayOptions()
        {
            this._log.Info(
                $"Input tests path: {this.parameters.TestSuitePath}",
                $"Output file: {this.parameters.OutputPath}",
                $"Output format: {this.parameters.OutputFormat}",
                $"Number of parts: {this.parameters.NumParts}");
        }

        void Run()
        {
            this.SayOptions();

            this._log.Info($"Discovering tests from {this.parameters.TestSuitePath}");
            var testSuite = this.ReadTestSuite(this.parameters.TestSuitePath);

            this._log.Info($"Splitting tests in {this.parameters.NumParts} parts");
            TestSuiteSplitter splitter = this.CreateTestSuiteSplitter();
            TestSuitePartition testSuitePartition = splitter.Split(testSuite, this.parameters.NumParts);

            var outputFormatter = this.CreateOutputFormatter();
            if (this.parameters.Part.HasValue)
            {
                this._log.Info($"Writing part {this.parameters.Part.Value} to {this.parameters.OutputPath}");
                using (var outputStream = this.OpenFile(this.parameters.OutputPath))
                {
                    outputFormatter.Output(
                        testSuitePartition.Parts[this.parameters.Part.Value],
                        outputStream);
                    outputStream.Close();
                }
            }
            else
            {
                for (int part = 0; part < this.parameters.NumParts; ++part)
                {
                    string partOutputPath = this.SuffixOutputFilePath(this.parameters.OutputPath, part);
                    this._log.Info($"Writing part {part} to {partOutputPath}");
                    using (var outputStream = this.OpenFile(partOutputPath))
                    {
                        outputFormatter.Output(
                            testSuitePartition.Parts[part],
                            outputStream);
                        outputStream.Close();
                    }
                }
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

        private TestSuitePart ReadTestSuite(string testSuitePath)
        {
            MethodWiseTestSuiteBuilder suiteBuilder = new MethodWiseTestSuiteBuilder();
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

        private IOutputFormatter CreateOutputFormatter()
        {
            switch (this.parameters.OutputFormat)
            {
                case OutputFormat.PlainTextTestList:
                    return new PlainTextOutputFormatter();
                case OutputFormat.OrderedTest:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
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