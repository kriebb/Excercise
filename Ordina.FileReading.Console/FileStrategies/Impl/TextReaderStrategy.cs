using System.Drawing;
using System.Runtime.CompilerServices;
using Ordina.Excercise;

namespace Ordina.FileReading.Console
{
    internal class TextReaderStrategy : ReaderStrategyBase
    {
        private readonly ITextReader _textReader;

        public TextReaderStrategy(ITextReader textReader)
        {
            _textReader = textReader;
        }
        public override string StrategyName { get; set; } = "text";
        protected override void DoExecute(string startOptionsFile, in bool startOptionsUseEncryption)
        {
            string result;
            if (startOptionsUseEncryption)
                result = _textReader.ReadContent(startOptionsFile, new ReverseStringDecryption());
            else
                result = _textReader.ReadContent(startOptionsFile);

            if (result == null)
            {
                Colorful.Console.WriteLine($"Couldn't read the {StrategyName} file", Color.Red);
                return;
            }

            Colorful.Console.WriteLine(result);
        }
    }
}