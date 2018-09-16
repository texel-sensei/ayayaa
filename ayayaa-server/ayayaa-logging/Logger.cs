using ayayaa.logging.Enums;
using ayayaa.logging.Interfaces;
using ayayaa.logging.Writers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging
{
    public class Logger : ILogger
    {
        public Logger()
        {
            Writers = new Dictionary<IWriter, LogPriority>();
        }

        public Dictionary<IWriter, LogPriority> Writers { get; }


        #region Public Functions

        /// <summary>
        /// Writes the given message to the given log. This function does not check minimum priority of the given log.
        /// </summary>
        public void WriteToLog(string message, LogPriority priority, IWriter log)
        {
            string fMessage = FormatEntry(message, priority);
            log.WriteMessage(fMessage);
        }

        /// <summary>
        /// Writes the given message into the logs. If the given priority is lower than the minimum priority of the log, 
        /// the message won't be written. The return value contains the success for each log.
        /// </summary>
        public Dictionary<IWriter, bool> WriteToLogs(string message, LogPriority priority)
        {
            Dictionary<IWriter, bool> results = new Dictionary<IWriter, bool>();
            string fMessage = FormatEntry(message, priority);

            foreach (IWriter writer in Writers.Keys) 
            {
                if (priority >= Writers[writer])
                {
                    writer.WriteMessage(fMessage);
                    results.Add(writer, true);
                }
                else
                {
                    results.Add(writer, false);
                }
            }

            return results;
        }

        /// <summary>
        /// Use this to add any amount of logs to the logger. The given priority is the minimum priority required for that log.
        /// </summary>
        public void AddWriter(IWriter writer, LogPriority priority)
        {
            if (Writers != null)
                Writers.Add(writer, priority);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Internal use only. Takes the incoming message and gives it a proper format for our log.
        /// </summary>
        private string FormatEntry(string message, LogPriority priority)
        {
            switch (priority)
            {
                case LogPriority.Trace:
                    return string.Format("[{0}] {1}", "TRACE", message);
                case LogPriority.Debug:
                    return string.Format("[{0}] {1}", "DEBUG", message);
                case LogPriority.Info:
                    return string.Format("[{0}] {1}", "INFO", message);
                case LogPriority.Warning:
                    return string.Format("[{0}] {1}", "WARNING", message);
                case LogPriority.Error:
                    return string.Format("[{0}] {1}", "ERROR", message);
                case LogPriority.FIRE:
                    return string.Format("[{0}] {1}", "FIRE", message);
                default:
                    return string.Format("{0}", message);
            }
        }

        #endregion
    }
}