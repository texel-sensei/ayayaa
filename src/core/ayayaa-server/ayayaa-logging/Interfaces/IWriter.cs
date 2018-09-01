using ayayaa.logging.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging.Interfaces
{
    public interface IWriter
    {
        /// <summary>
        /// Takes the given text and priority, formats it into a proper message for the given writer and finally logs it.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        bool WriteMessage(string text, LogPriority priority);

        /// <summary>
        /// The final message that gets sent after calling WriteMessage(). Used for testing purposes.
        /// </summary>
        object Message { get; }
    }
}
