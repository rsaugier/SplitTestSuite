namespace TestSuiteTools
{
    public interface ILog
    {
        void Info(string message);
        void Info(params string[] messages);
    }
}