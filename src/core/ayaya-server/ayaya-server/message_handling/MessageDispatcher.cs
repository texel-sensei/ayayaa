using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ayaya_server.message_handling
{
    
    public class MessageDispatcher
    {
        private readonly Dictionary<string, Type> _messageHandlers = new Dictionary<string, Type>();
        
        public void HandleMessage(Packet packet)
        {
            var cmd = packet.Command;
            var handler = GetMessageHandler(cmd);
            handler.HandleMessage(packet);
        }

        public void RegisterMessageHandler<T>() where T: IHandler
        {
            RegisterMessageHandler(typeof(T)); 
        }

        public void RegisterMessageHandler(Type handler)
        {
            if (handler == null)
            {
                throw new NullReferenceException();
            }
            if (!typeof(IHandler).IsAssignableFrom(handler))
            {
                throw new ArgumentException($"Message handlers must implement {typeof(IHandler).Name}!");
            }

            var handlerAttribute =
                handler.GetCustomAttributes().FirstOrDefault(attr => attr is HandlerAttribute) as HandlerAttribute;
            if (handlerAttribute == null)
            {
                throw new ArgumentException($"Message handler class needs the {typeof(HandlerAttribute).Name} attribute");
            }

            var existing = _messageHandlers.FirstOrDefault(t => t.Key == handlerAttribute.Command).Value;
            if (existing != null)
            {
                var existingAttribute =
                    existing.GetCustomAttributes().First(a => a is HandlerAttribute) as HandlerAttribute;
                var existingName = existingAttribute.Command;
                throw new ArgumentException($"A handler for the command '{handlerAttribute.Command} already exists! ({existingName})"); 
            }

            _messageHandlers[handlerAttribute.Command] = handler;
        }

        private IHandler GetMessageHandler(string cmd)
        {
            return _messageHandlers[cmd].GetConstructor(new Type[0]).Invoke(new object[0]) as IHandler;
        }
    }
}