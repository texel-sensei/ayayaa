using ayayaa.logging.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging.Interfaces
{
    public interface ILogger
    {
        IWriter Writer { get; }
        LogPriority MinimumPriority { get; }
        bool WriteToLog(string message, LogPriority priority);
        string FormatEntry(string message, LogPriority priority);
    }
}
