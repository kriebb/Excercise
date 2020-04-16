using System;
using System.IO;
using System.IO.Abstractions;

namespace Ordina.FileReading
{
    internal class PathValidations : IPathValidations
    {
        private readonly IFileSystem _fileSystem;

        public PathValidations(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public void ThrowWhenInvalid(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path), "The parameter you supplied was not valid.");

            var file = _fileSystem.FileInfo.FromFileName(path);
            if (!file.Exists)
                throw new FileNotFoundException($"couldn't found the file {path}");

        }

    }
}