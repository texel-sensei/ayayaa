using System;
using System.Collections.Generic;
using System.Diagnostics;
using ayayaa.connection;
using ayaya_server.file_storage;
using ayaya_server.message_handling;
using ayaya_server.message_handling.Handlers;
using Newtonsoft.Json;

namespace ayaya_server
{
    class Program
    {
        public static FileStorage Storage;

        private void DoLoop(IConnection con)
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.RegisterMessageHandler<UploadImageHandler>();
            con.Bind(1338);

            while(true) {
                var rawData = con.Receive();
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawData);
                var inPacket = new Packet()
                {
                    Command = dict["command"],
                    Data = dict
                };
                var response = dispatcher.HandleMessage(inPacket);

                var json = JsonConvert.SerializeObject(response);
                Console.WriteLine(json);
                con.Send(json);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("ayayaa-server starting...");
            var connection = new ZeroMQConnection();

            var path = "./images";
            if (args.Length == 1)
            {
                path = args[0];
            }

            Storage = new FileStorage(new DiskFileSystem(), path);
            new Program().DoLoop(connection);
        }
    }
}