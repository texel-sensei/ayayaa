﻿using System.IO;

namespace ayayaa.file_storage
{
    public class DiskFileSystem : IFileSystem
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
        }

        public Stream Open(string path)
        {
            return File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }
    }
}