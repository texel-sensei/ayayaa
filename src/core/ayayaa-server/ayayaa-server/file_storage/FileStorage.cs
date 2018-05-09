using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using ayayaa.utils;

namespace ayayaa.file_storage
{
    public class FileStorage
    {
        private IFileSystem _storage;
        private readonly string _basePath;
        private readonly int _prefixLength = 2;

        public FileStorage(IFileSystem fileSystem, string basePath)
        {
            _storage = fileSystem;
            _basePath = basePath;

            _storage.CreateFolder(basePath);
        }

        public string StoreData(byte[] data, string extension = null)
        {
            var hasher = new SHA256Managed();
            hasher.Initialize();

            string hash = null;

            using (hasher)
            {
                hash = HexTools.ByteArrayToHexString(hasher.ComputeHash(data));

                var targetPath = BuildPath(hash, extension);
                if (_storage.Exists(targetPath))
                {
                    return null;
                }

                var directory = Path.GetDirectoryName(targetPath);
                _storage.CreateFolder(directory);

                var file = _storage.Open(targetPath);
                using (file)
                {
                    file.Write(data, 0, data.Length);
                }
            }

            return hash;
        }

        public byte[] LoadData(string hash, string extension = null)
        {
            var path = BuildPath(hash, extension);
            using (var stream = _storage.Open(path))
            {
                var length = stream.Length;
                var buffer = new byte[length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public void DeleteData(string hash, string extension = null)
        {
            if (!Exists(hash, extension))
            {
                throw new KeyNotFoundException(
                    $"Can't find hash {hash} with file extensin {extension}"
                );
            }

            var path = BuildPath(hash, extension);
            _storage.Delete(path);
        }

        public bool Exists(string hash, string extension = null)
        {
            var path = BuildPath(hash, extension);
            return _storage.Exists(path);
        }

        private string BuildPath(string hash, string extension)
        {
            var prefix = hash.Substring(0, _prefixLength);
            var filename = hash;
            if (extension != null)
            {
                filename += extension;
            }
            return Path.Combine(_basePath, prefix, filename);
        }
    }
}
