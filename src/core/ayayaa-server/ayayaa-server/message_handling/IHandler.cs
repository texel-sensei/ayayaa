using System;
using System.Collections.Concurrent;

namespace ayayaa.message_handling
{
    public interface IHandler
    {
        Response HandleMessage(Packet data);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class HandlerAttribute : Attribute
    {
        public string Command { get; }

        public HandlerAttribute(string commandString)
        {
            Command = commandString;
        }    
    }
}