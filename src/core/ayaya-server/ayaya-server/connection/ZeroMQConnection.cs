using ZeroMQ;
using ZeroMQ.lib;

namespace ayayaa.connection
{
    public class ZeroMQConnection : IConnection
    {
        private ZeroMQ.ZContext _context;
        private ZeroMQ.ZSocket _socket;

        public ZeroMQConnection(ZeroMQ.ZContext ctx = null)
        {
            _context = ctx ?? new ZeroMQ.ZContext();
            _socket = new ZSocket(_context, ZSocketType.REP);
        }
        
        public void Bind(int port)
        {
            _socket.Bind($"tcp://*:{port}");
        }

        public bool Send(string data)
        {
            _socket.Send(new ZFrame(data));
            return true;
        }

        public string Receive()
        {
            var msg = _socket.ReceiveMessage();
            return msg.PopString();
        }
    }
}