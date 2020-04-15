using Ordina.FileReading;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace Ordina.Excercise
{
    public class GivenATextFileReader
    {
        public GivenATextFileReader()
        {

        }

        [Fact]
        public void HowTheUserShouldUseIt_Sample()
        {
            var fileReader = ReaderFactory.Create();
            var content = fileReader.ReadContent("exc1.txt");

            Assert.NotNull(content);
            Assert.StartsWith(@"3. Implement a file reading ""library"" that provides the following functionalities: ", content);
        
            }
        [Fact]
        public void IntegrationTest_WhenWeWantToReadFromTheFileSystem_ItShouldBeRead()
        {
            var fileReader = new TextFileReader(new System.IO.Abstractions.FileSystem());
            var content = fileReader.ReadContent("exc1.txt");

            Assert.NotNull(content);
            Assert.StartsWith(@"3. Implement a file reading ""library"" that provides the following functionalities: ", content);
        }
        [Fact]
        public void FileThatHasBeenReadShouldReturnSameValue()
        {
            string exptectedCurrentDir = @"c:\";
            string expectedPath = @"someFile.tst";

            string expectedContent = "a file";
            System.Collections.Generic.IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add(expectedPath, new MockFileData(expectedContent, System.Text.Encoding.UTF8));

            var fileSystem = new MockFileSystem(fileDictionary, exptectedCurrentDir);

            var reader = new TextFileReader(fileSystem);
            var content = reader.ReadContent($"{exptectedCurrentDir}{expectedPath}");

            Assert.Equal(expectedContent, content);
        }

        [Fact]
        public void FileNotExists_ShouldGiveException()
        {
            string exptectedCurrentDir = @"c:\";
            string expectedPath = @"someFile1.tst";

            string expectedContent = "a file";
            IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add($"{exptectedCurrentDir}{expectedPath}.old", new MockFileData(expectedContent, System.Text.Encoding.UTF8));

            var fileSystem = new MockFileSystem(fileDictionary, exptectedCurrentDir);

            var reader = new TextFileReader(fileSystem);

            Assert.Throws<FileNotFoundException>(() => reader.ReadContent($"{exptectedCurrentDir}{expectedPath}"));

        }

        [Fact]
        public void WhenPathIsNull_ShouldGetException()
        {
            string exptectedCurrentDir = @"c:\";
            string expectedPath = @"someFile1.tst";

            string expectedContent = "a file";
            IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add($"{exptectedCurrentDir}{expectedPath}.old", new MockFileData(expectedContent, System.Text.Encoding.UTF8));

            var fileSystem = new MockFileSystem(fileDictionary, @"C:\");

            var reader = new TextFileReader(fileSystem);

            Assert.Throws<ArgumentNullException>(() => reader.ReadContent(null));

        }

        [Fact]
        public void WhenPathIsStringEmpty_ShouldGetException()
        {
            string exptectedCurrentDir = @"c:\";
            string expectedPath = @"someFile1.tst";

            string expectedContent = "a file";
            System.Collections.Generic.IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add($"{exptectedCurrentDir}{expectedPath}.old", new MockFileData(expectedContent, System.Text.Encoding.UTF8));

            var fileSystem = new MockFileSystem(fileDictionary, exptectedCurrentDir);

            var reader = new TextFileReader(fileSystem);

            Assert.Throws<ArgumentNullException>(() => reader.ReadContent(string.Empty));

        }
        [Fact]
        public void WhenPathIsStringWhiteSpace_ShouldGetException()
        {
            string exptectedCurrentDir = @"c:\";
            string expectedPath = @"someFile1.tst";

            string expectedContent = "a file";
            System.Collections.Generic.IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add(expectedPath + ".old", new MockFileData(expectedContent, System.Text.Encoding.UTF8));

            var fileSystem = new MockFileSystem(fileDictionary, exptectedCurrentDir);

            var reader = new TextFileReader(fileSystem);

            Assert.Throws<ArgumentNullException>(() => reader.ReadContent(" "));

        }
    }
}
