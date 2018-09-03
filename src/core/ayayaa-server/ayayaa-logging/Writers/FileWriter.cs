using ayayaa.logging.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ayayaa.logging.Writers
{
    public class FileWriter : Writer
    {
        public FileWriter(string path)
        {
            logPath = path;
        }

        private string logPath;

        public override void WriteMessage(string text)
        {
            string sMessage = SerializeMessage(text) as string;
            Write(sMessage);
        }

        protected override object SerializeMessage(string message)
        {
            try
            {
                // In a proper file log we'll want to have a data & time for each entry, so we'll add that over here.
                // The format will be: YYYY-MM-DDTHH:mm:ss
                message = string.Format("[{0}] {1}", DateTime.Now.ToString("s"), message);

                // Probably unncessary, but just in case convert the string from whatever encoding we got to UTF8.
                // That way we can be sure that our logs don't have weird symbols in it.
                byte[] source = Encoding.Default.GetBytes(message);
                byte[] utf8 = Encoding.Convert(Encoding.Default, Encoding.UTF8, source);
                string fMessage = Encoding.UTF8.GetString(utf8);

                return fMessage;
            }
            catch
            {
                throw new LoggerException("An error occured while serializing message in FileLogger.");
            }
            
        }

        protected override void Write(object text)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(logPath, FileMode.Append, FileAccess.Write)))
                {
                    sw.WriteLine(text as string);
                }
            }
            catch
            {
                throw new LoggerException("An error occured while writing message into file in FileLogger.");
            }
        }
    }
}
