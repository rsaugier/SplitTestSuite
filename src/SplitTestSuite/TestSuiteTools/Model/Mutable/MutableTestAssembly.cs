using System.Collections.Generic;

namespace SplitTestSuite.TestSuiteTools.Model.Mutable
{
    internal class MutableTestAssembly
    {
        public string Path;
        public Dictionary<string, MutableTestNamespace> Namespaces { get; } = new();

        public MutableTestAssembly(string path)
        {
            this.Path = path;
        }

        public MutableTestNamespace GetOrAddNamespace(string nsName)
        {
            MutableTestNamespace ns = this.Namespaces.GetValueOrDefault(nsName);
            if (ns == null)
            {
                ns = new MutableTestNamespace(nsName);
                this.Namespaces.Add(nsName, ns);
            }
            return ns;
        }
    }
}