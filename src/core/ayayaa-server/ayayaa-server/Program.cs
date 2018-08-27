using System;
using System.Collections.Generic;
using System.Diagnostics;
using ayayaa.connection;
using ayayaa.file_storage;
using ayayaa.message_handling;
using ayayaa.message_handling.Handlers;
using Newtonsoft.Json;

namespace ayayaa
{
    class Program
    {
        public static FileStorage Storage;

        private void DoLoop(IConnection con)
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.RegisterMessageHandler<UploadImageHandler>();

            con.SetConnectionHandler(request => dispatcher.HandleMessage(request));

            con.Bind(1338);

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine(); // Wait until stop
        }

        static void Main(string[] args)
        {

            Console.WriteLine("ayayaa-server starting...");
            var path = "./images";
            if (args.Length == 1)
            {
                path = args[0];
            }

            Storage = new FileStorage(new DiskFileSystem(), path);
            using (var connection = new NancyConnection())
            {
                NancyConnection.Storage = Storage;
                new Program().DoLoop(connection);
            }
        }
    }
}