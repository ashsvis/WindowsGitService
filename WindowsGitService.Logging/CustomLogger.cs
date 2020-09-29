using log4net;
using log4net.Config;

namespace WindowsGitService.Logging
{
    public static class CustomLogger
    {
        public static readonly ILog Log;

        static CustomLogger()
        {
            Log = LogManager.GetLogger("LOGGER");

            XmlConfigurator.Configure();
        }

        public static ILog GetLogger()
        {
            return Log;
        }
    }
}
