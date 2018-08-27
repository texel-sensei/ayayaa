import aiohttp
from packet import Packet


class CommunicationError(RuntimeError):
    pass


class Connection:
    def __init__(self, context=None):
        self.timeout = 1000
        self.connected = False
        self.endpoint: str = None
        self._url: str = None

    async def connect(self, ip: str, port: int):
        self._url = f"http://{ip}:{port}/api/"
        self.connected = True

    def disconnect(self):
        self.connected = False

    async def send_packet(self, packet: Packet) -> Packet:
        if not self.connected:
            raise RuntimeError("No connection to server!")
        async with aiohttp.ClientSession() as session:
            text = bytes(packet.serialize(), "utf-8")
            async with session.post(self._url, data=text) as response:
                response_text = await response.text()

        return Packet.deserialize(response_text)
