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
        public void HowTheUserShouldUseIt_Sample()
        {
            var fileReader = ReaderFactory.CreateXmlReader();
            var content = fileReader.ReadContent("exc2.xml");

            Assert.NotNull(content);
        }
        [Fact]
        public void IntegrationTest_WhenWeWantToReadFromTheFileSystem_ItShouldBeRead()
        {
            var fileSystem = new FileSystem();
            var pathValidations = new PathValidations(new FileSystem());
            var fileReader = new XmlFileReader(pathValidations, fileSystem);
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

            var reader = new XmlFileReader(_pathValidations, fileSystem);
            var content = reader.ReadContent($"{expectedDir}{expectedPath}");

            Assert.True(XNode.DeepEquals(XDocument.Parse(expectedContent).Document, content));
        }

        [Fact]
        public void WhenPathValidationsThrowsException_CallerShouldGetTheException()
        {
            var mockSystem = new MockFileSystem();
            var exceptionMessage = nameof(WhenPathValidationsThrowsException_CallerShouldGetTheException) + "throws";
            _pathValidations.When(x => x.ThrowWhenInvalid(Arg.Any<string>())).Do( (callInfo)=> throw new Exception(exceptionMessage));
            var reader = new XmlFileReader(_pathValidations, mockSystem);

            var ex = Assert.Throws<Exception>(() => reader.ReadContent(@"c:\some\path\someFile.ext"));
            Assert.Equal(exceptionMessage, ex.Message);

        }


    }
}
