using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ayayaa.message_handling;
using Nancy;
using Nancy.Extensions;
using Nancy.Hosting.Self;
using Nancy.ViewEngines.SuperSimpleViewEngine;
using Newtonsoft.Json;
using Response = Nancy.Response;

namespace ayayaa.connection
{
    public class NancyConnection : Nancy.NancyModule, IConnection, IDisposable
    {
        private NancyHost _host;
        private static HandleConnection _handler;

        public NancyConnection()
        {
            Post("/api/", async args => await WaitConnection(args));
            Post("/api/{cmd}/", async args => await WaitConnection(args));
        }

        public void Bind(int port)
        {
            var hostConfig = new HostConfiguration
            {
                UrlReservations = {CreateAutomatically = true},
                RewriteLocalhost = false
            };

            _host = new NancyHost(hostConfig, new Uri($"http://localhost:{port}"));
            _host.Start();
        }

        public void SetConnectionHandler(HandleConnection handler)
        {
            _handler = handler;
        }

        public async Task<string> WaitConnection(DynamicDictionary args)
        {
            string cmd = null;
            if (args.ContainsKey("cmd"))
            {
                cmd = args["cmd"];
            }

            var rawData = Request.Body.AsString();
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawData);

            if (cmd == null)
            {
                cmd = dict["command"];
            }

            var inPacket = new Packet()
            {
                Command = cmd,
                Data = dict
            };

            var response = _handler(inPacket);

            var json = JsonConvert.SerializeObject(response);
            Console.WriteLine(json);
            return json;
        }

        public void Dispose()
        {
            _host?.Dispose();
        }
    }
}
