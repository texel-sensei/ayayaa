import base64
import gzip
import json

import os


def dict_property(name: str, *, read_only=False):
    def fget(self):
        return self.data[name]
    if not read_only:
        def fset(self, value):
            self.data[name] = value
    else:
        fset = None
    return property(fget, fset)


class Packet:
    tags = dict_property('tags', read_only=True)
    image = dict_property('image')
    filename = dict_property('filename')

    def __init__(self):
        self.data = {}

    def set_image(self, path):
        with open(path, 'rb') as image:
            self.image = image.read()
        self.filename = os.path.basename(path)

    def serialize(self) -> str:
        copy = {k: v for (k, v) in self.data.items() if k != "image"}

        data = gzip.compress(self.image)
        copy['image'] = str(base64.b64encode(data), 'utf-8')

        return json.dumps(copy)

    @staticmethod
    def deserialize(data: str):
        pack = Packet()
        pack.data = json.loads(data)
        if 'image' in pack.data:
            strdata = pack.image
            bindata = gzip.decompress(base64.b64decode(strdata))
            pack.image = bindata
        return pack
