using System.Collections.Generic;

namespace TestSuiteTools.Model.Mutable
{
    public class MutableTestSuite
    {
        public Dictionary<string, MutableTestAssembly> Assemblies { get; } = new();

        public MutableTestAssembly GetOrAddAssembly(string assemblyPath)
        {
            MutableTestAssembly? assembly = this.Assemblies.GetValueOrDefault(assemblyPath);
            if (assembly == null)
            {
                assembly = new MutableTestAssembly(assemblyPath);
                this.Assemblies.Add(assemblyPath, assembly);
            }
            return assembly;
        }
    }
}