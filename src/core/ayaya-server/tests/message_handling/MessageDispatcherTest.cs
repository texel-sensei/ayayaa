using System;
using Xunit;
using ayaya_server.message_handling;

namespace tests.message_handling
{
    internal class BrokenHandlerNoAttr : IHandler{
       public void HandleMessage(Packet data){}
    }

    [Handler("TestCommand")]
    internal class WorkingHandler : IHandler
    {
        public void HandleMessage(Packet data)
        {
            Assert.NotNull(data);
            Assert.Equal("TestCommand", data.Command);
        }
    }
    
    
    public class UnitTest1
    {
        private readonly MessageDispatcher _dispatcher = new MessageDispatcher();
        
        [Fact]
        public void TestRegistration()
        {
            Assert.Throws<ArgumentException>(() => _dispatcher.RegisterMessageHandler(typeof(string)));
            Assert.Throws<ArgumentException>(() => _dispatcher.RegisterMessageHandler<BrokenHandlerNoAttr>());
            _dispatcher.RegisterMessageHandler<WorkingHandler>();
            Assert.Throws<ArgumentException>(() => _dispatcher.RegisterMessageHandler<WorkingHandler>());
        }

        [Fact]
        public void TestHandle()
        {
            _dispatcher.RegisterMessageHandler<WorkingHandler>();
            _dispatcher.HandleMessage(new Packet()
            {
                Command = "TestCommand"
            }); 
        }
        
        [Fact]
        public void TestHandleNonExisting()
        {
            _dispatcher.RegisterMessageHandler<WorkingHandler>();
            _dispatcher.HandleMessage(new Packet()
            {
                Command = "Doesn't exist"
            }); 
        }
        
    }
}