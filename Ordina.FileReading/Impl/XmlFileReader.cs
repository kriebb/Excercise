using System;
using System.IO;
using System.IO.Abstractions;
using System.Security;
using System.Xml;
using System.Xml.Linq;

namespace Ordina.FileReading
{
    internal class XmlFileReader : IXmlReader
    {
        private readonly IPathValidations _pathValidations;
        private readonly IFileSystem _fileSystem;
        private readonly ITextReader _textReader;

        public XmlFileReader(IPathValidations pathValidations, IFileSystem fileSystem, ITextReader textReader)
        {
            _pathValidations = pathValidations ?? throw new ArgumentNullException(nameof(pathValidations));
            this._fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            _textReader = textReader ?? throw new ArgumentNullException(nameof(textReader));
        }

        public XDocument ReadContent(string path)
        {
            _pathValidations.ThrowWhenInvalid(path);

            using (var stream = _fileSystem.File.OpenRead(path))
            {
                try
                {
                    var xDoc = XDocument.Load(stream);
                    return xDoc;
                }
                catch (XmlException)
                {
                    return null;
                }

            }
        }

        public XDocument ReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm)
        {
            _pathValidations.ThrowWhenInvalid(path);

            var decryptedContent = _textReader.ReadContent(path, decryptionAlgorithm);

            try
            {
                var xDoc = XDocument.Load(GenerateStreamFromString(decryptedContent));
                return xDoc;
            }
            catch (System.Xml.XmlException)
            {
                return null;
            }

        }

        private static Stream GenerateStreamFromString(string input)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(input);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }

}