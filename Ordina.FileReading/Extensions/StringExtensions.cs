using System.IO;

namespace Ordina.FileReading
{
    public static class StringExtensions
    {
        public static Stream ToStream(this string input)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(input);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}