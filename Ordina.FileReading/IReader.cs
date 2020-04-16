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
        string ReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm);
    }
    public interface IXmlReader : IReader<XDocument>
    {
    }

    public interface IDecryptionAlgorithm
    {
        string Decrypt(string encryptedContent);
    }
}
