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
        void WriteMessage(string text);
    }
}
