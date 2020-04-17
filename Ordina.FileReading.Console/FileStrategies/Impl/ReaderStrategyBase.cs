using System;
using System.Drawing;

namespace Ordina.FileReading.Console
{
    internal abstract class ReaderStrategyBase : IReaderStrategy
    {
        public abstract string StrategyName { get; set; }
        public void Execute(string startOptionsFile, in bool startOptionsUseEncryption)
        {
            try
            {
                DoExecute(startOptionsFile, startOptionsUseEncryption);

            }
            catch (Exception ex)
            {
                Colorful.Console.WriteLine(ex.Message, Color.Red);
                Colorful.Console.WriteLine("Stopped with reading...", Color.Red);

                Colorful.Console.WriteLine("");
            }
        }

        protected abstract void DoExecute(string startOptionsFile, in bool startOptionsUseEncryption);
    }
}