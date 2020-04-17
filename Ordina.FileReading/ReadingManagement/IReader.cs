using System.Xml;

namespace Ordina.FileReading
{
    public interface IReader<T>:IDecryptionReader<T>
    {
        T ReadContent(string path);
    }
}
