#! /usr/bin/env python3
import sys
import os

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
        self.lock = asyncio.Lock()

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
        with await self.lock:
            print("New image")
            packet = Packet()
            try:
                print("exists" if os.path.exists(path) else "not existing")
                print("Set image data")
                packet.set_image(path)
            except PermissionError as e:
                print(e)
                return
            if not self.server.connected:
                print("Connect to server")
                await self.server.connect("localhost", 1338)
            print("Send request")
            try:
                response = await self.server.send_packet(packet)
                print(response)
                print(response.serialize())
                if response.data['Code'] == 200:
                    os.remove(path)
            except CommunicationError:
                print("Failed!")

    def test(self):
        loop = asyncio.get_event_loop()
        loop.run_until_complete(self.handle_new_image("test.bmp"))
        loop.stop()


if __name__ == "__main__":
    path = sys.argv[1] if len(sys.argv) > 1 else '.'
    watcher = InputWatcher(path)
    watcher.run()
    #watcher.test()

        
