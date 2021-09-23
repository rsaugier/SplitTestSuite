using System;
using System.IO;
using System.Linq;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Discovery
{
    public class TestsFolderCrawler
    {
        private readonly string folderPath;
        private readonly IUserOutput userOutput;

        public TestsFolderCrawler(string testSuiteFolderPath, IUserOutput userOutput)
        {
            this.folderPath = testSuiteFolderPath;
            this.userOutput = userOutput;
        }

        public void BuildTestSuite(MethodWiseTestSuiteBuilder suiteBuilder)
        {
            this.userOutput.Say($"Finding all test assemblies in this folder: {this.folderPath}");
            var assemblyPaths = Enumerable.Union(
                Directory.EnumerateFiles(this.folderPath, "*.dll", SearchOption.TopDirectoryOnly),
                Directory.EnumerateFiles(this.folderPath, "*.exe", SearchOption.TopDirectoryOnly));

            int testAssembliesFound = 0;
            foreach (string assemblyPath in assemblyPaths)
            {
                try
                {
                    var reader = new TestsAssemblyReader(assemblyPath, this.userOutput);
                    if (reader.BuildTestSuite(suiteBuilder))
                    {
                        this.userOutput.Say($"Found test assembly: {assemblyPath}");
                        testAssembliesFound++;
                    }
                }
                catch (Exception e)
                {
                    this.userOutput.Say($"Couldn't open {assemblyPath} - skipping it ({e.Message})");
                }
            }

            this.userOutput.Say($"Found {testAssembliesFound} test assemblies");
        }
    }
}