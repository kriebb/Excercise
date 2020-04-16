using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ordina.FileReading
{
    public class ReaderFactory
    {
        private static ServiceProvider _provider;
        private static ServiceCollection _serviceCollection;

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
        public static ITextReader CreateTextReader()
        {
            return _provider.GetService<ITextReader>();
        }

        /// <summary>
        /// When using the defaults, you get a small speedgain because the dependency Injection isn't rebuild. There aren't performance issues at the moment, so we keep it as is.
        /// </summary>
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

    }
}
