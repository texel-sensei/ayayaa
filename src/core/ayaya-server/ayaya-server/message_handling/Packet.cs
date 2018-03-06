using System;
using System.Collections.Generic;

namespace ayaya_server.message_handling
{
    public class Packet
    {
        public string Command;
        public readonly Dictionary<string, string> Data = new Dictionary<string, string>();
    }
}