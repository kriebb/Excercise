using System;
using Ordina.FileReading;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Xml;
using System.Xml.Linq;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ordina.Excercise
{
    public class GivenAXmlFileReader
    {
        private readonly IPathValidations _pathValidations;

        public GivenAXmlFileReader()
        {
            _pathValidations = NSubstitute.Substitute.For<IPathValidations>();
        }


        [Fact]
        public void IntegrationTest_WhenWeWantToReadFromTheFileSystem_ItShouldBeRead()
        {
            var fileSystem = new FileSystem();
            var pathValidations = new PathValidations(new FileSystem());
            var textReader = Substitute.For<ITextReader>();
            var fileReader = new XmlFileReader(pathValidations, fileSystem, textReader);
            var content = fileReader.ReadContent("exc2.xml");

            Assert.NotNull(content);
            Assert.StartsWith(@"<raw>", content.ToString());
        }
        [Fact]
        public void FileThatHasBeenReadShouldReturnSameValue()
        {
            string expectedDir = @"c:\";
            string expectedPath = @"someFile.tst";

            string expectedContent = "<xml></xml>";
            System.Collections.Generic.IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add($"{expectedDir}{expectedPath}", new MockFileData(expectedContent, System.Text.Encoding.UTF8));

            var fileSystem = new MockFileSystem(fileDictionary, expectedDir);
            var textReader = Substitute.For<ITextReader>();
            var reader = new XmlFileReader(_pathValidations, fileSystem, textReader);
            var content = reader.ReadContent($"{expectedDir}{expectedPath}");

            Assert.True(XNode.DeepEquals(XDocument.Parse(expectedContent).Document, content));
        }

        [Fact]
        public void FileWithInvalidXmlThatHasBeenReadShouldReturnSameValue()
        {
            string expectedDir = @"c:\";
            string expectedPath = @"someFile.tst";

            string expectedContent = "<xml></xml<";
            System.Collections.Generic.IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add($"{expectedDir}{expectedPath}", new MockFileData(expectedContent, System.Text.Encoding.UTF8));

            var fileSystem = new MockFileSystem(fileDictionary, expectedDir);
            var textReader = Substitute.For<ITextReader>();
            var reader = new XmlFileReader(_pathValidations, fileSystem, textReader);
            var content = reader.ReadContent($"{expectedDir}{expectedPath}");

            Assert.Null(content);
        }

        [Fact]
        public void WhenPathValidationsThrowsException_CallerShouldGetTheException()
        {
            var mockSystem = new MockFileSystem();
            var exceptionMessage = nameof(WhenPathValidationsThrowsException_CallerShouldGetTheException) + "throws";
            _pathValidations.When(x => x.ThrowWhenInvalid(Arg.Any<string>())).Do((callInfo) => throw new Exception(exceptionMessage));
            var textReader = Substitute.For<ITextReader>();

            var reader = new XmlFileReader(_pathValidations, mockSystem, textReader);

            var ex = Assert.Throws<Exception>(() => reader.ReadContent(@"c:\some\path\someFile.ext"));
            Assert.Equal(exceptionMessage, ex.Message);

        }

        [Fact]
        public void EncryptedXmlFileShouldBeAbleToBeRead()
        {
            string expectedDir = @"c:\";
            string expectedPath = @"someFile.tst";

            string encryptedContent = ">lmx/<>lmx<";
            string expectedDecryptedContent = "<xml></xml>";
            System.Collections.Generic.IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add($"{expectedDir}{expectedPath}", new MockFileData(encryptedContent, System.Text.Encoding.UTF8));

            var fileSystem = new MockFileSystem(fileDictionary, expectedDir);
            var textReader = Substitute.For<ITextReader>();
            var decryption = new ReverseStringDecryption();
            textReader.ReadContent(Path(expectedDir, expectedPath), decryption).Returns(expectedDecryptedContent);

            var reader = new XmlFileReader(_pathValidations, fileSystem, textReader);
            var content = reader.ReadContent(Path(expectedDir, expectedPath), decryption);

            Assert.True(XNode.DeepEquals(XDocument.Parse(expectedDecryptedContent).Document, content));
        }

        private static string Path(string expectedDir, string expectedPath)
        {
            return $"{expectedDir}{expectedPath}";

        }


        [Fact]
        public void FalseEncryptedXmlFileShouldReturnNull()
        {
            string expectedDir = @"c:\";
            string expectedPath = @"someFile.tst";

            string encryptedContent = "<lmx/<>lmx<";
            string expectedDecryptedContent = "<xml></xml<";
            System.Collections.Generic.IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add($"{expectedDir}{expectedPath}", new MockFileData(encryptedContent, System.Text.Encoding.UTF8));

            var fileSystem = new MockFileSystem(fileDictionary, expectedDir);
            var textReader = Substitute.For<ITextReader>();
            var decryption = new ReverseStringDecryption();
            textReader.ReadContent(Path(expectedDir, expectedPath), decryption).Returns(expectedDecryptedContent);

            var reader = new XmlFileReader(_pathValidations, fileSystem, textReader);
            var content = reader.ReadContent($"{expectedDir}{expectedPath}", decryption);

            Assert.Null(content);
        }
    }
}
