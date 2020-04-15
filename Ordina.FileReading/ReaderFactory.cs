using System.IO.Abstractions;

namespace Ordina.FileReading
{
    public class ReaderFactory
    {

        public static ITextReader CreateTextReader()
        {
            var fileSystem = new FileSystem();

            return new TextFileReader(new PathValidations(fileSystem), fileSystem);
        }
        public static IXmlReader CreateXmlReader()
        {
            var fileSystem = new FileSystem();

            return new XmlFileReader(new PathValidations(fileSystem), fileSystem);
        }
    }
}
