using System;
using System.IO.Abstractions;
using System.Linq.Expressions;

namespace Ordina.FileReading
{
    public class ReaderFactory
    {

        public static ITextReader CreateTextReader()
        {
            var fileSystem = new FileSystem();

            return new TextFileReader(new PathValidations(fileSystem), fileSystem);
        }
        public static IXmlReader CreateXmlReader(IRbacService rbacService = null)
        {
            var fileSystem = new FileSystem();
            rbacService = rbacService ?? new DefaultRbacService(new NullClaimsRepository());
            return new RoleBasedXmlFileReader(new XmlFileReader(new PathValidations(fileSystem), fileSystem), rbacService);
        }

    }
}
