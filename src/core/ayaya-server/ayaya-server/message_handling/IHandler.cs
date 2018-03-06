using System;
using System.Collections.Concurrent;

namespace ayaya_server.message_handling
{
    public interface IHandler
    {
        void HandleMessage(Packet data);
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