using System;
using ayayaa.connection;
using Newtonsoft.Json;

namespace ayaya_server
{
    class Program
    {
        private void DoLoop(IConnection con)
        {
            con.Bind(1338);
            while (true)
            {
                var data = con.Receive();
                var response = handle_data(data);
                con.Send(response);
            }
        }

        private string handle_data(string data)
        {
            return "{}";
        }

        static void Main(string[] args)
        {
            var connection = new ZeroMQConnection();
            new Program().DoLoop(connection);
        }
    }
}