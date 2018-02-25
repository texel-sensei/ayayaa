import zmq
import zmq.asyncio
from packet import Packet


class CommunicationError(RuntimeError):
    pass


class Connection:
    def __init__(self, context=None):
        self.timeout = 1000
        self.context = context or zmq.asyncio.Context.instance()
        self.socket = self.context.socket(zmq.REQ)
        self.connected = False
        self.endpoint: str = None

    async def connect(self, ip: str, port: int):
        self.endpoint = "tcp://{}:{}".format(ip, port)
        self.socket.connect(self.endpoint)
        self.connected = True

    def disconnect(self):
        self.connected = False
        self.socket.disconnect(self.endpoint)

    async def send_packet(self, packet: Packet) -> Packet:
        if not self.connected:
            raise RuntimeError("No connection to server!")
        can_send = await self.socket.poll(self.timeout, flags=zmq.POLLOUT)
        if can_send & zmq.POLLOUT:
            text = bytes(packet.serialize(), "utf-8")
            await self.socket.send(text)
        else:
            self.disconnect()
            raise CommunicationError("Connection timed out")

        can_receive = await self.socket.poll(self.timeout, flags=zmq.POLLIN)
        print(can_receive)
        if can_receive & zmq.POLLIN:
            response_text = await self.socket.recv()
        else:
            self.disconnect()
            raise CommunicationError("Connection timed out")
        return Packet.deserialize(response_text)
