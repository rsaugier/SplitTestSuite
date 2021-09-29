using System;
using System.IO;
using System.Linq;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Discovery
{
    public class TestsFolderCrawler
    {
        private readonly string folderPath;
        private readonly ILog _log;

        public TestsFolderCrawler(string testSuiteFolderPath, ILog log)
        {
            this.folderPath = testSuiteFolderPath;
            this._log = log;
        }

        public void BuildTestSuite(TestSuiteBuilder suiteBuilder)
        {
            this._log.Info($"Finding all test assemblies in this folder: {this.folderPath}");
            var assemblyPaths = Enumerable.Union(
                Directory.EnumerateFiles(this.folderPath, "*.dll", SearchOption.TopDirectoryOnly),
                Directory.EnumerateFiles(this.folderPath, "*.exe", SearchOption.TopDirectoryOnly));

            int testAssembliesFound = 0;
            foreach (string assemblyPath in assemblyPaths)
            {
                try
                {
                    var reader = new TestsAssemblyReader(assemblyPath, this._log);
                    if (reader.BuildTestSuite(suiteBuilder))
                    {
                        this._log.Info($"Found test assembly: {assemblyPath}");
                        testAssembliesFound++;
                    }
                }
                catch (Exception e)
                {
                    this._log.Info($"Couldn't open {assemblyPath} - skipping it ({e.Message})");
                }
            }

            this._log.Info($"Found {testAssembliesFound} test assemblies");
        }
    }
}