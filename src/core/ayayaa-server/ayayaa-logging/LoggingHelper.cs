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
            // Pretty much every Project needs logging for exceptions and it allows us to create an exception extension later on.
            CreateExceptionLogger();

            // ...
        }

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
        /// <param name="tag"></param>
        /// <param name="logger"></param>
        /// <returns>Success</returns>
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
        /// <param name="tag"></param>
        /// <returns>Success</returns>
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
        /// <param name="tag"></param>
        /// <returns>Logger</returns>
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
        /// <param name="tag"></param>
        /// <param name="message"></param>
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
        /// <param name="message"></param>
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
