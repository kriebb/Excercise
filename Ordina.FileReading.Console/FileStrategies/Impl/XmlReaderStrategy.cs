using System.Drawing;
using System.Xml.Linq;
using Ordina.Excercise;

namespace Ordina.FileReading.Console
{
    internal class XmlReaderStrategy : ReaderStrategyBase
    {
        private readonly IXmlReader _xmlReader;

        public XmlReaderStrategy(IXmlReader xmlReader)
        {
            _xmlReader = xmlReader;
        }
        public override string StrategyName { get; set; } = "xml";
        protected override void DoExecute(string startOptionsFile, in bool startOptionsUseEncryption)
        {
            XDocument result;
            if (startOptionsUseEncryption)
                result = _xmlReader.ReadContent(startOptionsFile, new ReverseStringDecryption());
            else
                result = _xmlReader.ReadContent(startOptionsFile);

            if (result == null)
            {
                Colorful.Console.WriteLine($"Couldn't read the {StrategyName} file", Color.Red);
                return;
            }

            Colorful.Console.WriteLine(result.Document.ToString());
        }
    }
}