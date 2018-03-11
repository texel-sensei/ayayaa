using System;
using System.Collections.Generic;

namespace ayaya_server.message_handling
{
    public class Packet
    {
        public string Command;
        public readonly Dictionary<string, string> Data = new Dictionary<string, string>();
    }

    public enum ResponseCode
    {
        Ok = 200,
        RequestError = 400,
        UnknownCommand = 401
    }

    public class Response
    {
        public ResponseCode Code;
        public string CodeText;
    }
}