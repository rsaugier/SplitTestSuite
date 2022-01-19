using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Discovery
{
    public class TestsFolderCrawler
    {
        private readonly string folderPath;
        private readonly bool exploreSubFoldersRecursively;
        private readonly ILog _log;

        public TestsFolderCrawler(string testSuiteFolderPath, bool exploreSubFoldersRecursively, ILog log)
        {
            this.folderPath = testSuiteFolderPath;
            this.exploreSubFoldersRecursively = exploreSubFoldersRecursively;
            this._log = log;
        }

        public void BuildTestSuite(TestSuiteBuilder suiteBuilder)
        {
            var foundAssemblies = new List<string>();
            this._log.Info($"Finding all test assemblies in this folder: {this.folderPath}");
            var assemblyPaths = Enumerable.Union(
                Directory.EnumerateFiles(
                    this.folderPath,
                    "*.dll",
                    exploreSubFoldersRecursively ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly),
                Directory.EnumerateFiles(
                    this.folderPath,
                    "*.exe",
                    exploreSubFoldersRecursively ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));

            int testAssembliesFound = 0;
            foreach (string assemblyPath in assemblyPaths)
            {
                try
                {
                    var reader = new TestsAssemblyReader(assemblyPath, this._log);
                    if (reader.BuildTestSuite(suiteBuilder))
                    {
                        this._log.Info($"Found test assembly: {assemblyPath}");
                        foundAssemblies.Add(assemblyPath);
                        testAssembliesFound++;
                    }
                }
                catch (Exception e)
                {
                    this._log.Debug($"Couldn't open {assemblyPath} - skipping it ({e.Message})");
                }
            }

            this._log.Info($"Found {testAssembliesFound} test assemblies:");
            this._log.Info(foundAssemblies.ToArray());
        }
    }
}