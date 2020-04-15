namespace Ordina.FileReading
{
    public class ReaderFactory
    {
        public static IReader Create()
        {
            return new TextFileReader(new System.IO.Abstractions.FileSystem());
        }

    }
}
