using System.Drawing;
using System.Text.Json;
using Ordina.Excercise;

namespace Ordina.FileReading.Console
{
    internal class JsonReaderStrategy : ReaderStrategyBase
    {
        private readonly IJsonReader _jsonReader;

        public JsonReaderStrategy(IJsonReader jsonReader)
        {
            this._jsonReader = jsonReader;
        }
        public override string StrategyName { get; set; } = "json";
        protected override void DoExecute(string startOptionsFile, in bool startOptionsUseEncryption)
        {
            JsonDocument result;
            if (startOptionsUseEncryption)
                result = _jsonReader.ReadContent(startOptionsFile, new ReverseStringDecryption());
            else
                result = _jsonReader.ReadContent(startOptionsFile);

            if (result == null)
            {
                Colorful.Console.WriteLine($"Couldn't read the {StrategyName} file", Color.Red);
                return;
            }

            Colorful.Console.WriteLine(result);
        }
    }
}