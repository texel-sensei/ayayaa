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
        /// <returns></returns>
        void WriteMessage(string text);

        /// <summary>
        /// Internal use only. This function converts the message into a value that's usable to the writer.
        /// (e.g. byte[] for sending it to a remote host.)
        /// </summary>
        /// <param name="message"></param>
        object SerializeMessage(string message);

        /// <summary>
        /// Internal use only. Performs the actual writing.
        /// </summary>
        /// <param name="message"></param>
        void Write(object message);
    }
}
