#! /usr/bin/env python3
import sys
from watchdog.observers import Observer
from watchdog.events import PatternMatchingEventHandler
from watchdog.events import FileCreatedEvent
import time

from packet import Packet


class InputWatcher(PatternMatchingEventHandler):
    ignore_directories = True
    patterns = ['*.jpg', '*.bmp', '*.png', '*.gif']

    def __init__(self, path):
        super().__init__()
        self.observer = Observer()
        self.observer.schedule(self, path)

    def run(self):
        self.observer.start()
        try:
            while True:
                time.sleep(1)
        except KeyboardInterrupt as e:
            self.observer.stop()
        self.observer.join()

    def on_created(self, event: FileCreatedEvent):
        packet = Packet()
        try:
            packet.set_image(event.src_path)
        except PermissionError as e:
            print(e)
            return
        print(packet.serialize())


if __name__ == "__main__":
    path = sys.argv[1] if len(sys.argv) > 1 else '.'
    watcher = InputWatcher(path)
    watcher.run()

        
