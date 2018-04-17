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
                Debug.WriteLine(json);
                con.Send(json);
            }
        }

        static void Main(string[] args)
        {
            var connection = new ZeroMQConnection();
            Storage = new FileStorage(new DiskFileSystem(), "T:\\images");
            new Program().DoLoop(connection);
        }
    }
}