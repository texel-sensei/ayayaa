﻿namespace ayaya_server.message_handling
{
    public class UnknowCommandHandler : IHandler
    {
        public Response HandleMessage(Packet data) => new Response()
        {
            Code = ResponseCode.UnknownCommand,
            CodeText = $"Unknown command {data.Command}"
        };
    }
}