using System.Collections.Generic;
using System.Linq;

namespace Ordina.FileReading.Console
{
    internal class EntryPoint : IEntryPoint
    {
        private readonly IEnumerable<IReaderStrategy> _readerStrategies;

        public EntryPoint(IEnumerable<IReaderStrategy> readerStrategies)
        {
            _readerStrategies = readerStrategies;
        }
        public void Start(StartOptions startOptions)
        {
            _readerStrategies.Single(x => x.StrategyName == startOptions.FileType)
                .Execute(startOptions.File, startOptions.UseEncryption);
        }
    }
}