using System;
using Xunit;
using ayaya_server.message_handling;

namespace tests.message_handling
{
    internal class BrokenHandlerNoAttr : IHandler
    {
        public Response HandleMessage(Packet data) => null;
    }

    [Handler("TestCommand")]
    internal class WorkingHandler : IHandler
    {
        public Response HandleMessage(Packet data)
        {
            Assert.NotNull(data);
            Assert.Equal("TestCommand", data.Command);
            return new Response() {Code = ResponseCode.Ok};
        }
    }
    
    
    public class MessageDispatcherTest
    {
        private readonly MessageDispatcher _dispatcher = new MessageDispatcher();
        
        [Fact]
        public void TestRegistration()
        {
            Assert.ThrowsAny<ArgumentException>(() => _dispatcher.RegisterMessageHandler(null));
            Assert.ThrowsAny<ArgumentException>(() => _dispatcher.RegisterMessageHandler(typeof(string)));
            Assert.ThrowsAny<ArgumentException>(() => _dispatcher.RegisterMessageHandler<BrokenHandlerNoAttr>());
            _dispatcher.RegisterMessageHandler<WorkingHandler>();
        }

        [Fact]
        public void TestUniqueRegistration()
        {
            _dispatcher.RegisterMessageHandler<WorkingHandler>();
            Assert.Throws<ArgumentException>(() => _dispatcher.RegisterMessageHandler<WorkingHandler>());
        }

        [Fact]
        public void TestHandle()
        {
            _dispatcher.RegisterMessageHandler<WorkingHandler>();
            var response = _dispatcher.HandleMessage(new Packet()
            {
                Command = "TestCommand"
            }); 
            Assert.Equal(ResponseCode.Ok, response.Code);
        }
        
        [Fact]
        public void TestHandleNonExisting()
        {
            _dispatcher.RegisterMessageHandler<WorkingHandler>();
           var response = _dispatcher.HandleMessage(new Packet()
            {
                Command = "Doesn't exist"
            }); 
            Assert.Equal(ResponseCode.UnknownCommand, response.Code);
        }

        [Fact]
        public void TestHandlerChange()
        {
            Assert.ThrowsAny<ArgumentException>(()=> _dispatcher.DefaultHandler = null);
            Assert.ThrowsAny<ArgumentException>(() => _dispatcher.DefaultHandler = typeof(MessageDispatcherTest));
            _dispatcher.DefaultHandler = typeof(WorkingHandler);
        }
    }
}