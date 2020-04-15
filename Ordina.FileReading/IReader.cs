using System.Xml;
using System.Xml.Linq;

namespace Ordina.FileReading
{
    public interface IReader<T>
    {
        T ReadContent(string path);

    }

    public interface ITextReader : IReader<string>
    {

    }
    public interface IXmlReader : IReader<XDocument>
    {
    }
}
