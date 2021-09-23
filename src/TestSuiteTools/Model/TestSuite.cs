using System.Collections.Generic;

namespace TestSuiteTools.Model
{
    /// <summary>
    /// Immutable test suite.
    /// </summary>
    /// <remarks>
    /// A test suite, in our context, means a list of test methods, grouped by assemblies and classes.
    /// The important point is that it can be a selection of a set of tests within a physical test suite,
    /// and thus can represent a sub-part of such a suite.
    /// </remarks>
    public class TestSuite
    {
        private readonly Dictionary<string, TestAssemblyPart> testAssemblies = new();

        public IReadOnlyCollection<TestAssemblyPart> TestAssemblies => this.testAssemblies.Values;

        public TestSuite(IReadOnlyCollection<TestAssemblyPart> testNamespaceLists)
        {
            foreach (var testNamespaceList in testNamespaceLists)
            {
                this.testAssemblies.Add(testNamespaceList.Assembly.Path, testNamespaceList);
            }
        }
    }
}