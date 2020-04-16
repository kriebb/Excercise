using Ordina.FileReading;
using Xunit;

namespace Ordina.Excercise
{
    public class Samples
    {
        [Fact]
        public void HowTheUserShouldUse_AFileTextReader_ReadContent_Sample()
        {
            var fileReader = ReaderFactory.CreateTextReader();
            var content = fileReader.ReadContent("exc1.txt");

            Assert.NotNull(content);
            Assert.StartsWith(@"3. Implement a file reading ""library"" that provides the following functionalities: ", content);

        }
        [Fact]
        public void HowTheUserShouldUse_AFileTextReader_ReadContentWithDecryption_Sample()
        {
            var fileReader = ReaderFactory.CreateTextReader();
            var content = fileReader.ReadContent("exc3.txt", new ReverseStringDecryption());

            Assert.NotNull(content);

            var decryptedContent = fileReader.ReadContent("exc3.decrypted.txt");

            Assert.Equal(decryptedContent, content);

        }
        [Fact]
        public void HowTheUserShouldUse_AXmlTextReader_Sample()
        {
            var fileReader = ReaderFactory.CreateXmlReader();
            var content = fileReader.ReadContent("exc2.xml");

            Assert.NotNull(content);
        }
    }
}