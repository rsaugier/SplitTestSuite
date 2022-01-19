using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    class Program
    {
        private const string PlainTextOutputFileExtension = "txt";
        private const string TestCaseFilterOutputFileExtension = "testcasefilter.txt";
        private readonly ProgramParams parameters;
        private readonly ILog _log;

        private Program(ProgramParams parameters)
        {
            this.parameters = parameters;
            this._log = parameters.Quiet ?
                        new NullLog() :
                        (new LogFilter(new ConsoleLog(), parameters.LogLevel));
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
            this._log.Info($"Input tests assemblies / folders:");
            this._log.Info(this.parameters.TestSuiteFilesAndFolders.ToArray());
            this._log.Info(
                $"Recurse subdirectories: {this.parameters.Recurse}",
                $"Number of parts: {this.parameters.NumParts}",
                $"Wanted part index: {this.parameters.Part}",
                $"Split granularity: {this.parameters.Granularity}",
                $"Plain text output file: {this.parameters.OutPlainText ?? "<none>"}",
                $"Test case filter output file: {this.parameters.OutTestCaseFilter ?? "<none>"}",
                $"Log level: {this.parameters.LogLevel}" + (this.parameters.Quiet ? " (but quiet so no output!)" : ""),
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
                this._log.Info($"Writing part {this.parameters.Part.Value} to '{path}' as '{formatter.FormatDisplayName}'");
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
                    this._log.Info($"Writing part {part} to '{partOutputPath}' as '{formatter.FormatDisplayName}'");
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

            this._log.Info($"Discovering tests...");
            var testSuite = this.ReadTestSuite(this.parameters.TestSuiteFilesAndFolders);
            this._log.Info($"Total test assemblies found: {testSuite.TestAssemblies.Count}");
            this._log.Info($"Splitting tests in {this.parameters.NumParts} parts");
            TestSuiteSplitter splitter = this.CreateTestSuiteSplitter();
            TestSuitePartition testSuitePartition = splitter.Split(testSuite, this.parameters.NumParts);

            string? plainTextOutputFile = this.parameters.OutPlainText;
            if (plainTextOutputFile != null)
            {
                this.WriteOutput(
                    testSuitePartition,
                    new PlainTextOutputFormatter(),
                    plainTextOutputFile,
                    PlainTextOutputFileExtension);
            }

            string? testCaseFilterOutputFile = this.parameters.OutTestCaseFilter;
            if (testCaseFilterOutputFile != null)
            {
                this.WriteOutput(
                    testSuitePartition,
                    new TestCaseFilterOutputFormatter(this._log),
                    testCaseFilterOutputFile,
                    TestCaseFilterOutputFileExtension);
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

        private TestSuite ReadTestSuite(IEnumerable<string> filesOrFolders)
        {
            var suiteBuilder = new TestSuiteBuilder();
            foreach (string path in filesOrFolders)
            {
                if (Directory.Exists(path))
                {
                    var crawler = new TestsFolderCrawler(path, this.parameters.Recurse, this._log);
                    crawler.BuildTestSuite(suiteBuilder);
                }
                else if (File.Exists(path))
                {
                    var reader = new TestsAssemblyReader(path, this._log);
                    reader.BuildTestSuite(suiteBuilder);
                }
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