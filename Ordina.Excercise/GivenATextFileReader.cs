using System;
using Ordina.FileReading;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Decor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ordina.Excercise
{
    public class GivenATextFileReader
    {
        private readonly IPathValidations _pathValidations;
        private readonly string _expectedContent;
        private readonly MockFileSystem _fileSystem;
        private readonly string _expFullNamePath;

        public GivenATextFileReader()
        {
            _pathValidations = NSubstitute.Substitute.For<IPathValidations>();

            var exptectedCurrentDir = @"c:\";
            var expectedPath = @"someFile.tst";
            _expFullNamePath = $"{exptectedCurrentDir}{expectedPath}";
            _expectedContent = "a file";
            System.Collections.Generic.IDictionary<string, MockFileData> fileDictionary = new Dictionary<string, MockFileData>();
            fileDictionary.Add(expectedPath, new MockFileData(_expectedContent, System.Text.Encoding.UTF8));

            _fileSystem = new MockFileSystem(fileDictionary, exptectedCurrentDir);
        }


        [Fact]
        public void IntegrationTest_WhenWeWantToReadFromTheFileSystem_ItShouldBeRead()
        {
            var fileSystem = new FileSystem();
            var pathValidations = new PathValidations(new FileSystem());
            var fileReader = new TextFileReader(pathValidations, fileSystem);
            var content = fileReader.ReadContent("exc1.txt");

            Assert.NotNull(content);
            Assert.StartsWith(@"3. Implement a file reading ""library"" that provides the following functionalities: ", content);
        }
        [Fact]
        public void FileThatHasBeenReadShouldReturnSameValue()
        {


            var reader = new TextFileReader(_pathValidations, _fileSystem);
            var content = reader.ReadContent(_expFullNamePath);

            Assert.Equal(_expectedContent, content);
        }

        [Fact]
        public void WhenWeSupplyAnDecryptionAlgorithm_ItShouldBeCalled()
        {
            var reader = new TextFileReader(_pathValidations, _fileSystem);

            var decryptionAlgorithm = NSubstitute.Substitute.For<IDecryptionAlgorithm>();

            var decryptedContent = reader.ReadContent(_expFullNamePath, decryptionAlgorithm);

            decryptionAlgorithm.Received(1).Decrypt(Arg.Is(_expectedContent));

        }

        [Fact]
        public void WhenWeAskToReadContent_WithoutDecryption_RoleProviderShouldBeChecked()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddFileReading();
            var mockedRbacService = Substitute.For<IRbacService>();

            serviceCollection.Replace(ServiceDescriptor.Singleton<IRbacService>(mockedRbacService));
            serviceCollection.Replace(ServiceDescriptor.Singleton<IFileSystem>(_fileSystem));

            var provider = serviceCollection.BuildServiceProvider(true);

            var reader = provider.GetService<ITextReader>();
            reader.ReadContent(_expFullNamePath);

            mockedRbacService.Received(1).ThrowWhenCantReadContent(_expFullNamePath);
        }

        [Fact]
        public void WhenWeAskToReadContent_WithDecryption_RoleProviderShouldBeChecked()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddFileReading();
            var mockedRbacService = Substitute.For<IRbacService>();
            var decryption = new ReverseStringDecryption();
            serviceCollection.Replace(ServiceDescriptor.Singleton<IRbacService>(mockedRbacService));
            serviceCollection.Replace(ServiceDescriptor.Singleton<IFileSystem>(_fileSystem));

            var provider = serviceCollection.BuildServiceProvider(true);

            var reader = provider.GetService<ITextReader>();
            reader.ReadContent(_expFullNamePath, decryption);

            mockedRbacService.Received(1).ThrowWhenCantReadContent(_expFullNamePath);
        }
        [Fact]
        public void WhenWeSupplyAnANullReferenceForADecryptionAlgorithm_ItShouldBeChecked()
        {
            var reader = new TextFileReader(_pathValidations, _fileSystem);
            Assert.Throws<ArgumentNullException>(() => reader.ReadContent(_expFullNamePath, null));
        }
        [Fact]
        public void WhenPathValidationsThrowsException_CallerShouldGetTheException()
        {
            var mockSystem = new MockFileSystem();
            var exceptionMessage = nameof(WhenPathValidationsThrowsException_CallerShouldGetTheException) + "throws";
            _pathValidations.When(x => x.ThrowWhenInvalid(Arg.Any<string>())).Do((callInfo) => throw new Exception(exceptionMessage));
            var reader = new TextFileReader(_pathValidations, mockSystem);

            var ex = Assert.Throws<Exception>(() => reader.ReadContent(@"c:\some\path\someFile.ext"));
            Assert.Equal(exceptionMessage, ex.Message);

        }


    }
}
