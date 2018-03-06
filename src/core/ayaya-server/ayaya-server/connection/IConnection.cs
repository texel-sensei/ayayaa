using System;
using Microsoft.VisualBasic.CompilerServices;

namespace ayayaa.connection
{
    public interface IConnection
    {
        void Bind(int port);
        bool Send(string data);
        string Receive();
    }
}