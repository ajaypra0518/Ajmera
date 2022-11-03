namespace Assessment.Logging.Interface
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}
