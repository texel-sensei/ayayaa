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
        public Logger(LogType lType)
        {
            LoggingType = lType;

            switch (LoggingType)
            {
                case LogType.Console:
                    Writer = new ConsoleWriter();
                    break;
                case LogType.File:
                    break;
                case LogType.Remoteserver:
                    break;
                default:
                    break;
            }
        }

        public LogType LoggingType { get; }
        public IWriter Writer { get; }

        public bool WriteToLog(string message, LogPriority priority)
        {
            try
            {
                return Writer.WriteMessage(message, priority);
            }
            catch
            {
                return false;
            }
        }
    }
}
