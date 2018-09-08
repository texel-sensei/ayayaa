using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging
{
    public class LoggerException : Exception
    {
        public LoggerException() : base("A failure occured during the logging process.") { }

        public LoggerException(string message) : base(message) { }
    }
}
