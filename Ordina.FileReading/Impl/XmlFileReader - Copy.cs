using System;
using System.IO.Abstractions;
using System.Text.Json;

namespace Ordina.FileReading
{
    internal class JsonFileReader : IJsonReader
    {
        private readonly IPathValidations _pathValidations;
        private readonly IFileSystem _fileSystem;
        private readonly ITextReader _textReader;

        public JsonFileReader(IPathValidations pathValidations, IFileSystem fileSystem)
        {
            _pathValidations = pathValidations ?? throw new ArgumentNullException(nameof(pathValidations));
            this._fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }


        public JsonDocument ReadContent(string path)
        {
            _pathValidations.ThrowWhenInvalid(path);

            var content = _fileSystem.File.ReadAllText(path);
            var document = JsonDocument.Parse(content);
            return document;
        }


    }

}