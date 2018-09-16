using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using ayayaa.file_storage;

namespace ayayaa.message_handling.Handlers
{
    [Handler("upload_image")]
    class UploadImageHandler : IHandler
    {
        private readonly FileStorage _storage = Program.Storage;

        public Response HandleMessage(Packet data)
        {
            Console.WriteLine($"Store image request for :{data.Data["filename"]}");
            var imageData = DecodeImage(data.Data["image"]);
            var extension = Path.GetExtension(data.Data["filename"]);
            var hash = _storage.StoreData(imageData, extension);

            if (hash != null)
            {
                return new Response()
                {
                    Code = ResponseCode.Ok,
                    CodeText = "ImageStorageSuccess",
                    Data = new Dictionary<string, string>
                    {
                        {"hash", hash},
                        {"file_extension", extension}
                    }
                };
            }
            else
            {
                return new Response()
                {
                    Code = ResponseCode.RequestError,
                    CodeText = "Already Existing"
                };
            }
        }

        private static byte[] DecodeImage(string encoded)
        {
            byte[] data = Convert.FromBase64String(encoded);

            using (var gzip = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
            using (var memory = new MemoryStream())
            {
                gzip.CopyTo(memory);
                return memory.ToArray();
            }
        }
    }
}
