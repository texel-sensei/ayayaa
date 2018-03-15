using System;
using System.Collections.Generic;

namespace ayaya_server.message_handling
{
    public class Packet
    {
        public string Command;
        public dynamic Data;
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
        public dynamic Data;
    }
}
