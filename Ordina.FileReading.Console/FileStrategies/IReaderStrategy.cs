namespace Ordina.FileReading.Console
{
    internal interface IReaderStrategy
    {
        string StrategyName { get; set; }
        void Execute(string startOptionsFile, in bool startOptionsUseEncryption);
    }
}