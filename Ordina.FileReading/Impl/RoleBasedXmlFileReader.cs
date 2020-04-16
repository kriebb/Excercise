using System;
using System.Xml.Linq;

namespace Ordina.FileReading
{
    internal class RoleBasedXmlFileReader : IXmlReader
    {
        private readonly IXmlReader _xmlReader;
        private readonly IRbacService _rbacService;

        public RoleBasedXmlFileReader(IXmlReader xmlReader, IRbacService rbacService)
        {
            _xmlReader = xmlReader ?? throw new ArgumentNullException(nameof(xmlReader));
            _rbacService = rbacService ?? throw new ArgumentNullException(nameof(rbacService));
        }
        public XDocument ReadContent(string path)
        {
            _rbacService.ThrowWhenCantReadContent(path); //todo, change this with IoC and middleware, for know, this will do
            return _xmlReader.ReadContent(path);
        }
    }
}