using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ordina.FileReading
{
    public class ReaderFactory
    {
        private static readonly ServiceProvider _provider;
        private static readonly ServiceCollection _serviceCollection;

        static ReaderFactory()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddFileReading();
            _provider = _serviceCollection.BuildServiceProvider();

        }
        /// <summary>
        /// Will build using DI a dependency injection framework and returns the default instance
        /// </summary>
        /// <returns></returns>
        public static ITextReader CreateTextReader(IRbacService rbacService = null)
        {
            if (rbacService != null)
            {
                _serviceCollection.Replace(ServiceDescriptor.Singleton<IRbacService>(rbacService));
                var provider = _serviceCollection.BuildServiceProvider();
                return provider.GetService<ITextReader>();
            }
            return _provider.GetService<ITextReader>();
        }
        /// <param name="rbacService"></param>
        /// <returns></returns>
        public static IXmlReader CreateXmlReader(IRbacService rbacService = null)
        {
            if (rbacService != null)
            {
                _serviceCollection.Replace(ServiceDescriptor.Singleton<IRbacService>(rbacService));
                var provider = _serviceCollection.BuildServiceProvider();
                return provider.GetService<IXmlReader>();
            }
            return _provider.GetService<IXmlReader>();
        }

        public static IJsonReader CreateJsonReader()
        {
            return _provider.GetService<IJsonReader>();
        }
    }
}
