using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using Ordina.FileReading;
using Xunit;

namespace Ordina.Excercise
{
    public class GivenPathValidations
    {
        private readonly PathValidations _pathValidation;
        private readonly string _expectedPath;
        private readonly string _expectedCurrentDir;

        public GivenPathValidations()
        {
            _expectedCurrentDir = @"c:\";
            _expectedPath = @"someFile1.tst";

            var mockFileSystem = new MockFileSystem();

            _pathValidation = new PathValidations(mockFileSystem);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void WhenPathIsInvalid_ShouldThrowException(string path)
        {
            Assert.Throws<ArgumentNullException>(() => _pathValidation.ThrowWhenInvalid(path));
        }


        [Fact]
        public void FileNotExists_ShouldGiveException()
        {


            Assert.Throws<FileNotFoundException>(() => _pathValidation.ThrowWhenInvalid($"{_expectedCurrentDir}{_expectedPath}"));

        }
    }
}