using Assessment.Logging.Interface;
using NLog;

namespace Assessment.Logging
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogInfo(string message) => logger.Info(message);
        public void LogError(string message) => logger.Error(message);
    }
}
