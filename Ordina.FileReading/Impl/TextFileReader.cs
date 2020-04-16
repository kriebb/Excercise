using System;
using System.IO.Abstractions;
using System.Text.RegularExpressions;

namespace Ordina.FileReading
{
    internal class TextFileReader : ITextReader
    {
        private readonly IPathValidations _pathValidations;
        private readonly IFileSystem _fileSystem;

        public TextFileReader(IPathValidations pathValidations, IFileSystem fileSystem)
        {
            _pathValidations = pathValidations;
            _fileSystem = fileSystem;
        }


        public string ReadContent(string path)
        {
            _pathValidations.ThrowWhenInvalid(path);

            return _fileSystem.File.ReadAllText(path);
        }

        public string ReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm)
        {
            if(decryptionAlgorithm == null)
                throw new ArgumentNullException(nameof(decryptionAlgorithm),"Please supply a instance of a decryption algorithm");
            var encryptedContent = ReadContent(path);
            var decryptedAlgorithm =  decryptionAlgorithm.Decrypt(encryptedContent);
            return decryptedAlgorithm;
        }
    }
}
