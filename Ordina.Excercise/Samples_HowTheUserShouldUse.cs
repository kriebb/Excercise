using System;
using System.Security;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Ordina.FileReading;
using Xunit;

namespace Ordina.Excercise
{
    public class Samples_HowTheUserShouldUse
    {
        [Fact]
        public void GivenAFileTextReader_ReadContent_Sample()
        {
            var fileReader = ReaderFactory.CreateTextReader();
            var content = fileReader.ReadContent("exc1.txt");

            Assert.NotNull(content);
            Assert.StartsWith(@"3. Implement a file reading ""library"" that provides the following functionalities: ", content);

        }
        [Fact]
        public void GivenAFileTextReader_ReadContentWithDecryption_Sample()
        {
            var fileReader = ReaderFactory.CreateTextReader();
            var content = fileReader.ReadContent("exc3.txt", new ReverseStringDecryption());

            Assert.NotNull(content);

            var decryptedContent = fileReader.ReadContent("exc3.decrypted.txt");

            Assert.Equal(decryptedContent, content);

        }
        [Fact]
        public void GivenAXmlTextReader_Sample()
        {
            var fileReader = ReaderFactory.CreateXmlReader();
            var content = fileReader.ReadContent("exc2.xml");

            Assert.NotNull(content);
        }

        [Fact]
        public void GivenACustomClaimRepository_Sample()
        {
            var claimsRepository = NSubstitute.Substitute.For<IClaimsRepository>();
            claimsRepository.CurrentUserHas(Arg.Any<string>()).Returns(false);

            var fileReader = ReaderFactory.CreateXmlReader(new DefaultRbacService(claimsRepository));
            Assert.Throws<SecurityException>(() => fileReader.ReadContent("exc2.xml"));

        }

        [Fact]
        public void GivenACustomRbacService_Sample()
        {
            var rbacService = NSubstitute.Substitute.For<IRbacService>();
            rbacService.When(x => x.ThrowWhenCantReadContent(Arg.Any<string>())).Do(callInfo => throw new SecurityException("testmethod is not allowed"));

            var fileReader = ReaderFactory.CreateXmlReader(rbacService);
            Assert.Throws<SecurityException>(() => fileReader.ReadContent("exc2.xml"));

        }

        [Fact]
        public void GivenTheReaderUsingDependencyInjection_XmlReader_Sample()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddFileReading();
            var provider = serviceCollection.BuildServiceProvider(true);


            //injection simulation

            var reader = provider.GetService<IXmlReader>();
            Assert.NotNull(reader);
        }

        [Fact]
        public void GivenTheReaderUsingDependencyInjection_TextReader_Sample()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddFileReading();
            var provider = serviceCollection.BuildServiceProvider(true);


            //injection simulation

            var reader = provider.GetService<ITextReader>();
            Assert.NotNull(reader);
        }
    }

    public class GivenAServiceCollectionForReadingFiles
    {
        [Fact]
        public void WeShouldBeAbleToResolveEverything()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddFileReading();
            var provider = serviceCollection.BuildServiceProvider(true);

            foreach (var myInterface in serviceCollection)
            {
                var impl = provider.GetService(myInterface.ServiceType);
                if (myInterface.ImplementationType != null)
                    Assert.Equal(myInterface.ImplementationType, impl.GetType());
                else
                {
                    if (myInterface.ImplementationInstance != null)
                        Assert.Equal(myInterface.ImplementationInstance.GetType(), impl.GetType());
                    else
                    {
                        if (myInterface.ImplementationFactory != null)
                        {
                            var instance = myInterface.ImplementationFactory(provider);

                            Assert.Equal(instance?.GetType(), impl.GetType());

                        }
                        else
                        {
                            Assert.False(true, $"There is no implementation found for interface {myInterface.GetType().FullName}");
                        }
                    }
                }
            }
        }
    }


}