namespace TestSuiteTools.Model
{
    public class TestAssembly
    {
        public string Path { get; }

        public TestAssembly(string path)
        {
            this.Path = path;
        }
    }
}