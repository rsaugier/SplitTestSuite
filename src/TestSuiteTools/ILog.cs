namespace TestSuiteTools
{
    public interface ILog
    {
        void Error(string message);
        void Error(params string[] messages);
        void Warn(string message);
        void Warn(params string[] messages);
        void Info(string message);
        void Info(params string[] messages);
        void Debug(string message);
        void Debug(params string[] messages);
    }
}