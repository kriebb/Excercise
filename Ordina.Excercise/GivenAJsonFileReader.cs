using System;
using Ordina.FileReading;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using NSubstitute;
using Xunit;

namespace Ordina.Excercise
{
    public class GivenAJsonFileReader
    {
        private readonly IPathValidations _pathValidations;
        private readonly MockFileSystem _fileSystem;
        private readonly string _expFullNamePath;

        public GivenAJsonFileReader()
        {
            _pathValidations = NSubstitute.Substitute.For<IPathValidations>();

            var exptectedCurrentDir = @"c:\";
            var expectedPath = @"someFile.tst";
            _expFullNamePath = $"{exptectedCurrentDir}{expectedPath}";
            var expectedContent = @"{ ""foo"":""bar""}";
            System.Collections.Generic.IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add(expectedPath, new MockFileData(expectedContent, System.Text.Encoding.UTF8));

            _fileSystem = new MockFileSystem(fileDictionary, exptectedCurrentDir);
        }


        [Fact]
        public void IntegrationTest_WhenWeWantToReadFromTheFileSystem_ItShouldBeRead()
        {
            var fileSystem = new FileSystem();
            var pathValidations = new PathValidations(new FileSystem());
            var fileReader = new JsonFileReader(pathValidations, fileSystem);
            var content = fileReader.ReadContent("exc7.json");

            Assert.NotNull(content);
            Assert.True(content.RootElement.GetProperty("foo").ValueEquals("bar"));
        }
        [Fact]
        public void FileThatHasBeenReadShouldReturnSameValue()
        {
            var reader = new JsonFileReader(_pathValidations, _fileSystem);
            var content = reader.ReadContent(_expFullNamePath);

            Assert.True(content.RootElement.GetProperty("foo").ValueEquals("bar"));
        }


        [Fact]
        public void WhenPathValidationsThrowsException_CallerShouldGetTheException()
        {
            var mockSystem = new MockFileSystem();
            var exceptionMessage = nameof(WhenPathValidationsThrowsException_CallerShouldGetTheException) + "throws";
            _pathValidations.When(x => x.ThrowWhenInvalid(Arg.Any<string>())).Do((callInfo) => throw new Exception(exceptionMessage));
            var reader = new JsonFileReader(_pathValidations, mockSystem);

            var ex = Assert.Throws<Exception>(() => reader.ReadContent(@"c:\some\path\someFile.ext"));
            Assert.Equal(exceptionMessage, ex.Message);

        }


    }
}
