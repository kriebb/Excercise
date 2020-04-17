using System;
using System.IO.Abstractions;
using System.Text.Json;

namespace Ordina.FileReading
{
    internal class JsonFileReader : ReaderBase<JsonDocument>, IJsonReader
    {
        private readonly IFileSystem _fileSystem;

        public JsonFileReader(IPathValidations pathValidations, IFileSystem fileSystem):base(pathValidations)
        {
            this._fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        protected override JsonDocument DoReadContent(string path)
        {

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

        protected override JsonDocument DoReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm)
        {
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