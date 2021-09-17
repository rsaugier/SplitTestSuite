namespace SplitTestSuite
{
    public interface IUserOutput
    {
        void Say(string message);
        void Say(params string[] messages);
    }
}