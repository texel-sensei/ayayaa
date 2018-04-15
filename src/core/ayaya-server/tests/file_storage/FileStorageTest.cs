using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using ayaya_server.file_storage;

using Xunit;

namespace tests.file_storage
{

    internal class MockFileSystem : IFileSystem
    {
        private readonly Dictionary<string, MemoryStream> _data = new Dictionary<string, MemoryStream>();
        private readonly HashSet<string> _folders = new HashSet<string>();

        public bool Exists(string path)
        {
            return _data.ContainsKey(path) || _folders.Contains(path);
        }

        public void CreateFolder(string path)
        {
            _folders.Add(path);
        }

        public Stream Open(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!_folders.Contains(dir))
            {
                throw new FileNotFoundException("Not found", path);
            }

            if (!Exists(path))
            {
                _data[path] = new MemoryStream();
            }

            var stream = _data[path];
            if (!stream.CanRead)
            {
                stream = new MemoryStream(stream.ToArray());
                _data[path] = stream;
            }
            return stream;
        }

        public void Delete(string path)
        {
            _data.Remove(path);
        }

        public int NumFiles => _data.Count;
    }

    public class FileStorageTest
    {
        private readonly MockFileSystem _fileSystem = new MockFileSystem();
        private FileStorage tested;

        [Fact]
        private void TestCreateBaseFolder ()
        {
            Assert.False(_fileSystem.Exists("foo/bar"));
            tested = new FileStorage(_fileSystem, "foo/bar");
            Assert.True(_fileSystem.Exists("foo/bar"));
            Assert.Equal(0, _fileSystem.NumFiles);
        }

        [Fact]
        private void TestRecjectDup()
        {
            tested = new FileStorage(_fileSystem, "foo/bar");
            var bytes = new byte[] {1, 2, 3, 4};
            var hash = tested.StoreData(bytes, ".dat");
            Assert.Equal(1, _fileSystem.NumFiles);
            Assert.NotNull(hash);
            hash = tested.StoreData(bytes, ".dat");
            Assert.Equal(1, _fileSystem.NumFiles);
            Assert.Null(hash);
        }

        [Fact]
        private void TestExists()
        {
            tested = new FileStorage(_fileSystem, "foo/bar");
            var bytes = new byte[] { 1, 2, 3, 4 };
            var hash = tested.StoreData(bytes);

            Assert.True(tested.Exists(hash));
            Assert.False(tested.Exists(hash, ".dat"));

            var hash2 = tested.StoreData(new byte[] {65, 66, 67}, ".txt");
            Assert.True(tested.Exists(hash2, ".txt"));
            Assert.False(tested.Exists(hash2, ".dat"));
            Assert.False(tested.Exists(hash2));

            Assert.True(tested.Exists(hash));
            Assert.False(tested.Exists(hash, ".dat"));
        }

        [Fact]
        private void TestLoadingStoringData()
        {
            tested = new FileStorage(_fileSystem, "foo/bar");
            var bytes = new byte[] { 1, 2, 3, 4 };
            var hash = tested.StoreData(bytes);

            var retreived = tested.LoadData(hash);
            Assert.Equal(bytes, retreived);
        }

        [Fact]
        private void TestDeletion()
        {
            tested = new FileStorage(_fileSystem, "foo/bar");

            Assert.ThrowsAny<KeyNotFoundException>(() => tested.DeleteData("asdf"));

            var bytes = new byte[] { 1, 2, 3, 4 };
            var hash = tested.StoreData(bytes);

            Assert.True(tested.Exists(hash));
            Assert.Equal(1, _fileSystem.NumFiles);

            tested.DeleteData(hash);

            Assert.False(tested.Exists(hash));
            Assert.Equal(0, _fileSystem.NumFiles);
        }
    }
}