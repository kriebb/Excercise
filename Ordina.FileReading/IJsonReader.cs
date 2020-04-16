using System.Text.Json;

namespace Ordina.FileReading
{
    public interface IJsonReader
    {
        JsonDocument ReadContent(string path);
    }
}