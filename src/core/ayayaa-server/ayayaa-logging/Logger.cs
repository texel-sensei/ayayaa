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
        public Logger(IWriter writer, LogPriority minPriority)
        {
            Writer = writer;
            MinimumPriority = minPriority;
        }

        public IWriter Writer { get; }
        public LogPriority MinimumPriority { get; }

        /// <summary>
        /// Writes the given message into the log. If the given priority is lower than the minimum priority of this logger, the message won't be logged and
        /// the function returns false.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public bool WriteToLog(string message, LogPriority priority)
        {
            // No need to bother logging unimportant infos.
            if (priority < MinimumPriority)
                return false;

            string fMessage = FormatEntry(message, priority);
            Writer.WriteMessage(fMessage);

            return true;
        }

        /// <summary>
        /// Internal use only. Takes the incoming message and gives it a proper format for our log.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public string FormatEntry(string message, LogPriority priority)
        {
            switch (priority)
            {
                case LogPriority.Low:
                    return string.Format("[{0}] {1}", "LOW", message);
                case LogPriority.Warning:
                    return string.Format("[{0}] {1}", "WAR", message);
                case LogPriority.Medium:
                    return string.Format("[{0}] {1}", "MED", message);
                case LogPriority.High:
                    return string.Format("[{0}] {1}", "HIGH", message);
                case LogPriority.Exception:
                    return string.Format("[{0}] {1}", "ERROR", message);
                case LogPriority.EverythingIsOnFire:
                    return string.Format("[{0}] {1}", "FIRE", message);
                default:
                    return string.Format("{0}", message);
            }
        }
    }
}
