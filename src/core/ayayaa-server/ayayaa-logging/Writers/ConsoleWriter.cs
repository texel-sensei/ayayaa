using ayayaa.logging.Enums;
using ayayaa.logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging.Writers
{
    public class ConsoleWriter : Writer
    {
        public ConsoleWriter()
        {
            // For a console writer, no outside data is required.
        }

        public override void WriteMessage(string text)
        {
            string fText = SerializeMessage(text) as string;
            Write(fText);
        }

        protected override object SerializeMessage(string message)
        {
            // Message does not need to be converted into a specific format for the console.
            return message;
        }


        protected override void Write(object text)
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
