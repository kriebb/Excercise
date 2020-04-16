using System.Xml;

namespace Ordina.FileReading
{
    public interface IReader<T>
    {
        T ReadContent(string path);
        T ReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm);
    }
}
