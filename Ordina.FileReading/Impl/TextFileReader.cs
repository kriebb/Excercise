using System;
using System.IO.Abstractions;
using System.Text.RegularExpressions;
using Decor;

namespace Ordina.FileReading
{
    internal class TextFileReader : ReaderBase<string>, ITextReader
    {
        private readonly IFileSystem _fileSystem;

        public TextFileReader(IPathValidations pathValidations, IFileSystem fileSystem) : base(pathValidations)
        {
            _fileSystem = fileSystem;
        }

        protected override string DoReadContent(string path)
        {
            return _fileSystem.File.ReadAllText(path);
        }

        protected override string DoReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm)
        {
            var encryptedContent = ReadContent(path);
            var decryptedAlgorithm = decryptionAlgorithm.Decrypt(encryptedContent);
            return decryptedAlgorithm;
        }
    }
}
