using Microsoft.Extensions.DependencyInjection;
using Ordina.FileReading;
using Xunit;

namespace Ordina.Excercise
{
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