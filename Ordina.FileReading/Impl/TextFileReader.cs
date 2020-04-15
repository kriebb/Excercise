using System;
using System.IO;
using System.IO.Abstractions;

namespace Ordina.FileReading
{
    public class TextFileReader : IReader
    {
        public TextFileReader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        private IFileSystem _fileSystem;

        public string ReadContent(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path), "The parameter you supplied was not valid.");

            var file = _fileSystem.FileInfo.FromFileName(path);
            if (!file.Exists)
                throw new FileNotFoundException($"couldn't found the file {path}");

            var content = _fileSystem.File.ReadAllText(path);
            return content;
        }
    }
}
