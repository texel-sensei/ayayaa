using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging.Enums
{
    /// <summary>
    /// A collection of priorities for the Logging system. They each have a number attached to them and numbers lower than the 
    /// minimum value given during Initialization of the Logger-class won't be printed.
    /// </summary>
    public enum LogPriority
    {
        Trace = 1,
        Debug = 2,
        Info = 3,
        Warning = 4,
        Error = 5,
        FIRE = 6
    }
}
