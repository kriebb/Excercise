namespace Ordina.FileReading
{
    public interface IDecryptionReader<T>
    {
        T ReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm);

    }
}