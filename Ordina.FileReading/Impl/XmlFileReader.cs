using System.IO.Abstractions;
using System.Xml.Linq;

namespace Ordina.FileReading
{
    internal class XmlFileReader : IXmlReader
    {
        private readonly IPathValidations _pathValidations;
        private readonly IFileSystem _fileSystem;

        public XmlFileReader(IPathValidations pathValidations, IFileSystem fileSystem)
        {
            _pathValidations = pathValidations;
            this._fileSystem = fileSystem;
        }

        public XDocument ReadContent(string path)
        {
            _pathValidations.ThrowWhenInvalid(path);
            using (var stream = _fileSystem.File.OpenRead(path))
            {
                var xDoc = XDocument.Load(stream);
                return xDoc;
            }
        }
    }
}