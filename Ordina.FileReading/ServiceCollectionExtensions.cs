using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ordina.FileReading
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddFileReading(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<ITextReader, TextFileReader>();
            serviceCollection.AddSingleton<IXmlReader, RoleBasedXmlFileReader>(provider =>
            {
                var rbacService = provider.GetService<IRbacService>();
                var xmlFileReader = provider.GetService<XmlFileReader>();
                return new RoleBasedXmlFileReader(xmlFileReader, rbacService);
            }); //ok for now, but needs to be changed when more dependencies are comin' in.
            serviceCollection.AddSingleton<XmlFileReader>();
            serviceCollection.TryAddSingleton<IClaimsRepository, NullClaimsRepository>();
            serviceCollection.TryAddSingleton<IFileSystem>(provider => new FileSystem());
            serviceCollection.TryAddSingleton<IRbacService, DefaultRbacService>();
            serviceCollection.TryAddSingleton<IPathValidations, PathValidations>();
            return serviceCollection;
        }
    }
}