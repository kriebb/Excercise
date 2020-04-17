namespace Ordina.FileReading
{
    public interface IDecryptionAlgorithm
    {
        string Decrypt(string encryptedContent);
    }
}