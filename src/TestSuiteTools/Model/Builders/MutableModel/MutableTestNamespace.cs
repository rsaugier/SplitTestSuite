using System.Collections.Generic;

namespace TestSuiteTools.Model.Builders.MutableModel
{
    public class MutableTestNamespace
    {
        public string Name;
        public Dictionary<string, MutableTestClass> Classes { get; } = new();

        public MutableTestNamespace(string name)
        {
            this.Name = name;
        }

        public MutableTestClass GetOrAddClass(string className)
        {
            MutableTestClass? cl = this.Classes.GetValueOrDefault(className);
            if (cl == null)
            {
                cl = new MutableTestClass(className);
                this.Classes.Add(className, cl);
            }
            return cl;
        }
    }
}