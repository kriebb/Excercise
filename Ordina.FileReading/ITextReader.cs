namespace Ordina.FileReading
{
    public interface ITextReader : IReader<string>
    {
        string ReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm);
    }
}