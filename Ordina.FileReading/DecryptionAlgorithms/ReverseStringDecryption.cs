using System;
using System.Linq;
using System.Text;
using Ordina.FileReading;

namespace Ordina.Excercise
{
    public class ReverseStringDecryption : IDecryptionAlgorithm
    {
        public string Decrypt(string encryptedContent)
        {
            if (encryptedContent == null) return null;

            var stringBuilder = new StringBuilder();
            var lines = encryptedContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var line in lines.Reverse())
            {
                char[] array = line.ToCharArray();
                Array.Reverse(array);

                stringBuilder.AppendLine(new string(array));
            }

            return stringBuilder.ToString();
        }
    }
}