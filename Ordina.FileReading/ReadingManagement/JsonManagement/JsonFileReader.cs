using System;
using System.IO.Abstractions;
using System.Text.Json;

namespace Ordina.FileReading
{
    internal class JsonFileReader : IJsonReader
    {
        private readonly IPathValidations _pathValidations;
        private readonly IFileSystem _fileSystem;

        public JsonFileReader(IPathValidations pathValidations, IFileSystem fileSystem)
        {
            _pathValidations = pathValidations ?? throw new ArgumentNullException(nameof(pathValidations));
            this._fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public JsonDocument ReadContent(string path)
        {
            _pathValidations.ThrowWhenInvalid(path);

            var text = _fileSystem.File.ReadAllText(path);
            try
            {
                var document = JsonDocument.Parse(text);
                return document;

            }
            catch (JsonException)
            {
                return null;
            }
        }

        public JsonDocument ReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm)
        {
            _pathValidations.ThrowWhenInvalid(path);

            var text = _fileSystem.File.ReadAllText(path);

            var decryptedContent = decryptionAlgorithm.Decrypt(text);

            try
            {
                var document = JsonDocument.Parse(decryptedContent);
                return document;
            }
            catch (JsonException)
            {
                return null;
            }

        }


    }
}