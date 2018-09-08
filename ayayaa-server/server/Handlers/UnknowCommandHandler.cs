using Newtonsoft.Json.Linq;

namespace ayayaa.message_handling
{
    public class UnknowCommandHandler : IHandler
    {
        public Response HandleMessage(Packet data) => new Response()
        {
            Code = ResponseCode.UnknownCommand,
            CodeText = $"Unknown command '{data.Command}'",
            Data = new JObject {{ "Unknown", data.Command }}
        };
    }
}
