#! /usr/bin/env python3
import sys

import asyncio
from watchdog.observers import Observer
from watchdog.events import PatternMatchingEventHandler
from watchdog.events import FileCreatedEvent

from connection import Connection, CommunicationError
from packet import Packet


class InputWatcher(PatternMatchingEventHandler):
    ignore_directories = True
    patterns = ['*.jpg', '*.bmp', '*.png', '*.gif']

    def __init__(self, path):
        super().__init__()
        self.observer = Observer()
        self.observer.schedule(self, path)
        self.server = Connection()
        self.main_loop = None

    def run(self):
        loop = self.main_loop = asyncio.get_event_loop()
        loop.run_in_executor(None, self.observer.start)
        loop.run_forever()
        loop.close()
        self.observer.stop()
        self.observer.join()

    def on_created(self, event: FileCreatedEvent):
        asyncio.run_coroutine_threadsafe(self.handle_new_image(event.src_path), self.main_loop)

    async def handle_new_image(self, path):
        print("New image")
        packet = Packet()
        try:
            print("Set image data")
            packet.set_image(path)
        except PermissionError as e:
            print(e)
            return
        if not self.server.connected:
            print("Connect to server")
            await self.server.connect("127.0.0.1", 1337)
        print("Send request")
        try:
            response = await self.server.send_packet(packet)
            print(response)
        except CommunicationError:
            print("Failed!")


if __name__ == "__main__":
    path = sys.argv[1] if len(sys.argv) > 1 else '.'
    watcher = InputWatcher(path)
    watcher.run()

        
