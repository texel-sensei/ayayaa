using ayayaa.logging.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging
{
    public static class LoggerExtensions
    {
        public static void LogTrace(this Logger logger, string message)
        {
            logger.WriteToLogs(message, LogPriority.Trace);
        }

        public static void LogDebug(this Logger logger, string message)
        {
            logger.WriteToLogs(message, LogPriority.Debug);
        }

        public static void LogInfo(this Logger logger, string message)
        {
            logger.WriteToLogs(message, LogPriority.Info);
        }

        public static void LogWarning(this Logger logger, string message)
        {
            logger.WriteToLogs(message, LogPriority.Warning);
        }

        public static void LogError(this Logger logger, string message)
        {
            logger.WriteToLogs(message, LogPriority.Error);
        }

        public static void LogFIRE(this Logger logger, string message)
        {
            logger.WriteToLogs(message, LogPriority.FIRE);
        }

        public static void LogThis(this Exception ex)
        {
            Logger exLogger = LoggingHelper.FindLogger("EX");
            if (exLogger != null)
            {
                exLogger.WriteToLogs(ex.Message, LogPriority.Error);
                exLogger.WriteToLogs(ex.StackTrace, LogPriority.Error);

                if (ex.InnerException != null)
                {
                    exLogger.WriteToLogs(ex.InnerException.Message, LogPriority.Error);
                }
            }
        }
    }
}
