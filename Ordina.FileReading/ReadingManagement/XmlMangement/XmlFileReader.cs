using System;
using System.IO;
using System.IO.Abstractions;
using System.Security;
using System.Xml;
using System.Xml.Linq;
using Decor;

namespace Ordina.FileReading
{
    internal class XmlFileReader : ReaderBase<XDocument>, IXmlReader
    {
        private readonly IFileSystem _fileSystem;

        public XmlFileReader(IPathValidations pathValidations, IFileSystem fileSystem) : base(pathValidations)
        {
            this._fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        protected override XDocument DoReadContent(string path)
        {
            using (var stream = _fileSystem.File.OpenRead(path))
            {
                return ToXDocument(stream);
            }
        }

        protected override XDocument DoReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm)
        {
            var encryptedContent = _fileSystem.File.ReadAllText(path);
            var decryptedContent = decryptionAlgorithm.Decrypt(encryptedContent);
            using (var stream = decryptedContent.ToStream())
            {
                return ToXDocument(stream);
            }
        }

        private static XDocument ToXDocument(Stream stream)
        {

            try
            {
                var xDoc = XDocument.Load(stream);
                return xDoc;
            }

            catch (System.Xml.XmlException)
            {
                return null;
            }
        }
    }
}
