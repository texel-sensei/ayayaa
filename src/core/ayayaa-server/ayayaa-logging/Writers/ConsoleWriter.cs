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
            string fText = SerializeMessage(text) as string;
            Write(fText);
        }

        public object SerializeMessage(string message)
        {
            // Message does not need to be converted into a specific format for the console.
            return message;
        }

        /// <summary>
        /// Performs the actual writing act.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public void Write(object text)
        {
            try
            {
                Console.WriteLine(text);
            }
            catch
            {
                throw new LoggerException();
            }
        }
    }
}
