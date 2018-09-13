using ayayaa.logging.Enums;
using ayayaa.logging.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ayayaa.logging
{
    public static class LoggingHelper
    {
        public static Dictionary<string, Logger> Loggers;

        public static void Initialize()
        {
            Loggers = new Dictionary<string, Logger>();
            CreateDefaultLoggers();
        }

        #region Private Functions

        private static void CreateDefaultLoggers()
        {
            // Create whatever Loggers you want for your project here.
        }

        /// <summary>
        /// Example for an exception logger.
        /// </summary>
        private static void CreateExceptionLogger()
        {
            Logger exceptionLogger = new Logger();
            string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\ayayaa";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string filePath = directoryPath + "\\EX_Log.log";
            FileWriter exceptionWriter = new FileWriter(filePath);
            exceptionLogger.AddWriter(exceptionWriter, LogPriority.Error);
            AddLogger("EX", exceptionLogger);
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Add a logger to the Helpers collection.
        /// </summary>
        public static bool AddLogger(string tag, Logger logger)
        {
            if (Loggers == null)
                return false;


            if (!Loggers.ContainsKey(tag))
            {
                Loggers.Add(tag, logger);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Removes the logger with the given tag from the collection.
        /// </summary>
        public static bool RemoveLogger(string tag)
        {
            if (Loggers == null)
                return false;

            if (Loggers.ContainsKey(tag))
            {
                Loggers.Remove(tag);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Finds and returns the logger with the given tag.
        /// </summary>
        public static Logger FindLogger(string tag)
        {
            if (Loggers == null)
                return null;

            Logger logger = null;

            if (Loggers.ContainsKey(tag))
                logger = Loggers[tag];

            return logger;
        }

        /// <summary>
        /// Writes given message to all Logs contained in the Logger with the given tag.
        /// </summary>
        public static void WriteToLog(string tag, string message)
        {
            if (Loggers == null)
                return;

            Logger logger = FindLogger(tag);
            if (logger != null)
                WriteToLogs(message);
        }

        /// <summary>
        /// Writes given message to all Logs.
        /// </summary>
        public static void WriteToLogs(string message)
        {
            foreach (string key in Loggers.Keys)
            {
                WriteToLog(key, message);
            }
        }

        #endregion
    }
}
