using System.IO.Abstractions;
using System.Net.Mime;
using System.Xml.Linq;
using Decor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ordina.FileReading
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddFileReading(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDecor();
            serviceCollection.TryAddSingleton<RbacDecorator>();
            serviceCollection.AddSingleton<ITextReader, TextFileReader>().Decorated();
            serviceCollection.AddSingleton<IXmlReader, XmlFileReader>().Decorated(); ;

            serviceCollection.TryAddSingleton<IClaimsRepository, NullClaimsRepository>();
            serviceCollection.TryAddSingleton<IFileSystem>(provider => new FileSystem());
            serviceCollection.TryAddSingleton<IRbacService, DefaultRbacService>();
            serviceCollection.TryAddSingleton<IPathValidations, PathValidations>();
            return serviceCollection;
        }
    }
}