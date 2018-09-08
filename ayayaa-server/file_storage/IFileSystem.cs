﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ayayaa.file_storage
{
    public interface IFileSystem
    {
        bool Exists(string path);
        void CreateFolder(string path);
        Stream Open(string path);
        void Delete(string path);
    }
}
