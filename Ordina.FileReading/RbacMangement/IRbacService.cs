namespace Ordina.FileReading
{
    public interface IRbacService
    {
        void ThrowWhenCantReadContent(string path);
    }
}