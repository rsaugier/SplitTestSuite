using System.Linq;
using System.Reflection;
using TestSuiteTools.Model.Builders;

namespace TestSuiteTools.Discovery
{
    public class TestsAssemblyReader
    {
        private readonly string assemblyPath;
        private readonly ILog _log;

        public TestsAssemblyReader(string assemblyPath, ILog log)
        {
            this.assemblyPath = assemblyPath;
            this._log = log;
        }

        public bool BuildTestSuite(MethodWiseTestSuiteBuilder suiteBuilder)
        {
            this._log.Info($"Loading assembly {this.assemblyPath} to search for tests");
            Assembly assembly = Assembly.LoadFrom(this.assemblyPath);

            bool atLeastOneTestFound = false;
            foreach (TypeInfo typeInfo in assembly.DefinedTypes.Where(this.IsTestClass))
            {
                foreach (MethodInfo testMethod in typeInfo.DeclaredMethods.Where(this.IsTestMethod))
                {
                    if (typeInfo.Namespace != null)
                        suiteBuilder.AddTestMethod(this.assemblyPath, typeInfo.Namespace, typeInfo.Name,
                            testMethod.Name);
                    atLeastOneTestFound = true;
                }
            }

            return atLeastOneTestFound;
        }

        private bool IsTestClass(TypeInfo typeInfo)
        {
            return typeInfo.CustomAttributes.Any(a => a.AttributeType.Name == "TestClassAttribute" && a.AttributeType.Namespace == "Microsoft.VisualStudio.TestTools.UnitTesting");
        }

        private bool IsTestMethod(MethodInfo methodInfo)
        {
            return methodInfo.CustomAttributes.Any(a => a.AttributeType.Name == "TestMethodAttribute" && a.AttributeType.Namespace == "Microsoft.VisualStudio.TestTools.UnitTesting");
        }
    }
}