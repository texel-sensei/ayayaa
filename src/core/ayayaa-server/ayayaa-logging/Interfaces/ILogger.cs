using ayayaa.logging.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging.Interfaces
{
    public interface ILogger
    {
        LogType LoggingType { get; }
        IWriter Writer { get; }
        bool WriteToLog(string message, LogPriority priority);
    }
}
