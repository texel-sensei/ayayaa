using ayayaa.logging.Enums;
using ayayaa.logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging.Writers
{
    public class ConsoleWriter : IWriter
    {
        public bool WriteMessage(string text, LogPriority priority)
        {
            string formattedText = FormMessage(text, priority);
            Message = formattedText;
            return Write(formattedText);
        }

        public object Message { get; private set; }

        private string FormMessage(string message, LogPriority priority)
        {
            switch (priority)
            {
                case LogPriority.Low:
                    return string.Format("{0} - {1}", "LOW", message);
                case LogPriority.Warning:
                    return string.Format("{0} - {1}", "WAR", message);
                case LogPriority.Medium:
                    return string.Format("{0} - {1}", "MED", message);
                case LogPriority.High:
                    return string.Format("{0} - {1}", "HIGH", message);
                case LogPriority.Exception:
                    return string.Format("{0} - {1}", "ERROR", message);
                case LogPriority.EverythingIsOnFire:
                    return string.Format("{0} - {1}", "FIRE", message);
                default:
                    return string.Format("{0}", message);
            }
        }

        /// <summary>
        /// Performs the actual writing act.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool Write(string text)
        {
            try
            {
                Console.WriteLine(text);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
