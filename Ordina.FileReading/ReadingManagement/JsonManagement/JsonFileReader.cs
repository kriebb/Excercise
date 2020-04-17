using System;
using System.IO.Abstractions;
using System.Text.Json;

namespace Ordina.FileReading
{
    internal class JsonFileReader : ReaderBase<JsonDocument>, IJsonReader
    {
        private readonly IFileSystem _fileSystem;

        public JsonFileReader(IPathValidations pathValidations, IFileSystem fileSystem) : base(pathValidations)
        {
            this._fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        protected override JsonDocument DoReadContent(string path)
        {

            var text = _fileSystem.File.ReadAllText(path);
            return ToJsonDocument(text);

        }

        protected override JsonDocument DoReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm)
        {
            var encryptedContent = _fileSystem.File.ReadAllText(path);
            var text = decryptionAlgorithm.Decrypt(encryptedContent);

            return ToJsonDocument(text);
        }

        private static JsonDocument ToJsonDocument(string text)
        {

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
    }
}