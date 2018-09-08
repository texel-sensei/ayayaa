using System;
using System.Threading.Tasks;
using ayayaa.message_handling;
using Microsoft.VisualBasic.CompilerServices;

namespace ayayaa.connection
{
    public delegate Response HandleConnection(Packet args);

    public interface IConnection
    {
        void Bind(int port);
        void SetConnectionHandler(HandleConnection handler);
    }
}