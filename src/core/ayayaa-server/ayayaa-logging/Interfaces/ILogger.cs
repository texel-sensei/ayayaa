using ayayaa.logging.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging.Interfaces
{
    public interface ILogger
    {
        Dictionary<IWriter, LogPriority> Writers { get; }

        void AddWriter(IWriter writer, LogPriority priority);
        void WriteToLog(string message, LogPriority priority, IWriter log);
        Dictionary<IWriter, bool> WriteToLogs(string message, LogPriority priority);
        string FormatEntry(string message, LogPriority priority);
    }
}
