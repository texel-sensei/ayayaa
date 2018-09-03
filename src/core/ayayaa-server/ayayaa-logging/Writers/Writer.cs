using ayayaa.logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.logging.Writers
{
    public abstract class Writer : IWriter
    {
        public abstract void WriteMessage(string text);

        /// <summary>
        /// Takes the given message and serializes it into the required format for the writer.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected abstract object SerializeMessage(string message);

        /// <summary>
        /// Performs the actual writing act.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected abstract void Write(object text);
    }
}
