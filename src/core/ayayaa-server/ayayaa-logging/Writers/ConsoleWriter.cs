using ayayaa.logging.Enums;
using ayayaa.logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging.Writers
{
    public class ConsoleWriter : IWriter
    {
        public ConsoleWriter()
        {
            // For a console writer, no outside data is required.
        }

        public void WriteMessage(string text)
        {
            Console.WriteLine(text);
        }
    }
}
