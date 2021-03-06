using System.Collections.Generic;

namespace TestSuiteTools.Model.Builders.MutableModel
{
    public class MutableTestClass
    {
        public string Name;
        public HashSet<string> Methods { get; } = new();

        public MutableTestClass(string name)
        {
            this.Name = name;
        }

        public bool TryAddMethod(string methodName)
        {
            if (this.Methods.Contains(methodName))
            {
                return false;
            }
            else
            {
                this.Methods.Add(methodName);
                return true;
            }
        }
    }
}