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

            var linesArray = lines.Reverse().ToArray();
            for (var i = 0; i < linesArray.Length; i++)
            {
                var line = linesArray[i];
                char[] array = line.ToCharArray();
                Array.Reverse(array);

                stringBuilder.Append(new string(array));
                if (i < linesArray.Length-1)
                    stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}