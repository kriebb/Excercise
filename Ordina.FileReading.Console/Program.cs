using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Colorful;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ordina.FileReading.Console
{
    class Program
    {
        /// <summary>
        /// Implement a simple GUI/CLI application allowing to specify which file type you want to read,
        /// specify to use the encryption system and specify if role based security is needed or not.
        /// As such I can start the application,
        /// - it will ask me the file type,
        /// - to use the encryption system and
        /// - if role based security should be used or not.
        ///    - If role based security is used it will ask my role and then we will read the file and show the output.
        /// - After this I can read another file in another way without needing to restart the application. 
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool retry = true;

            while (retry)
            {
                var file = ReadFile();
                var fileType = ReadFileType();
                var useEncryption = ReadUseEncryption().ToBoolean();
                var useRbac = ReadUseRbac().ToBoolean();
                var userRole = ReadUserRole(useRbac);

                var serviceCollection = new ServiceCollection();
                serviceCollection.AddFileReading();
                if (useRbac)
                    serviceCollection.Replace(
                        ServiceDescriptor.Singleton<IClaimsRepository>(new CurrentRoleRepository(userRole)));

                serviceCollection.AddSingleton<IReaderStrategy, JsonReaderStrategy>();
                serviceCollection.AddSingleton<IReaderStrategy, XmlReaderStrategy>();
                serviceCollection.AddSingleton<IReaderStrategy, TextReaderStrategy>();
                serviceCollection.AddSingleton<IEntryPoint, EntryPoint>();

                var provider = serviceCollection.BuildServiceProvider();

                var entryPoint = provider.GetService<IEntryPoint>();
                entryPoint.Start(new StartOptions(file, fileType, useEncryption, useRbac, userRole));

                retry = ReadContinue().ToBoolean();

            }

        }

        private static string ReadContinue()
        {
            return ChooseBoolean("Do you want to read another file?");
        }

        private static string ReadFile()
        {

            Colorful.Console.WriteLine("Provide the file in with a fullpath that you want to read?");
            var input = Colorful.Console.ReadLine();
            while (!File.Exists(input))
            {
                Colorful.Console.WriteLine("Can't find your file; Try Again!", Color.Red);

                Colorful.Console.WriteLine("Provide the file in with a fullpath that you want to read?");
                input = Colorful.Console.ReadLine();
            }

            return input;
        }

        private static string ReadUserRole(bool useRba)
        {
            if (!useRba)
                return null;

            var rbacValues
                = new Dictionary<string, string>();
            rbacValues.Add("1", "Read");
            rbacValues.Add("2", "Write");

            rbacValues.Add("3", "Admin");

            Colorful.Console.WriteLine("What Role you do u want?", Color.CadetBlue);
            foreach (var item in rbacValues)
            {
                Colorful.Console.Write(item.Key, Color.Green);
                Colorful.Console.WriteLine("-> " + item.Value);
            }

            Colorful.Console.WriteLine("");
            Colorful.Console.WriteLine("Give a number that is in range and Press ENTER");

            var input = Colorful.Console.ReadLine();
            while (!rbacValues.Keys.Contains(input))
            {
                Colorful.Console.WriteLine("Couldn't validate your input; Try Again!", Color.Red);

                Colorful.Console.WriteLine("Give a number that is in range and Press ENTER");
                input = Colorful.Console.ReadLine();
            }

            return rbacValues[input];
        }

        private static string ReadUseRbac()
        {
            return ChooseBoolean("Do you want to use the RBAC system?");
        }

        private static string ChooseBoolean(string question)
        {
            var encryptionSystemValues
                = new Dictionary<string, string>();
            encryptionSystemValues.Add("1", "No");
            encryptionSystemValues.Add("2", "Yes");

            Colorful.Console.WriteLine(question, Color.CadetBlue);
            foreach (var item in encryptionSystemValues)
            {
                Colorful.Console.Write(item.Key, Color.Green);
                Colorful.Console.WriteLine("-> " + item.Value);
            }

            Colorful.Console.WriteLine("");
            Colorful.Console.WriteLine("Give a number that is specified above in the choicelist and Press ENTER");

            var input = Colorful.Console.ReadLine();
            while (!encryptionSystemValues.Keys.Contains(input))
            {
                Colorful.Console.WriteLine("Couldn't validate your input; Try Again!", Color.Red);

                Colorful.Console.WriteLine("Give a number that is in range and Press ENTER");
                input = Colorful.Console.ReadLine();
            }
            Colorful.Console.WriteLine("");

            return encryptionSystemValues[input];
        }

        private static string ReadUseEncryption()
        {
            return ChooseBoolean("Do you want to use the decryption system?");


        }

        private static string ReadFileType()
        {
            var fileTypes = new Dictionary<string, string>();
            fileTypes.Add("1", "text");
            fileTypes.Add("2", "xml");
            fileTypes.Add("3", "json");

            Colorful.Console.WriteLine("In what format should the file be read that you supplied?", Color.CadetBlue);
            foreach (var item in fileTypes)
            {
                Colorful.Console.Write(item.Key, Color.Green);
                Colorful.Console.WriteLine("-> " + item.Value);
            }

            Colorful.Console.WriteLine("");
            Colorful.Console.WriteLine("Give a number that is in range and Press ENTER");

            var input = Colorful.Console.ReadLine();
            while (!fileTypes.Keys.Contains(input))
            {
                Colorful.Console.WriteLine("Couldn't validate your input; Try Again!", Color.Red);

                Colorful.Console.WriteLine("Give a number that is in range and Press ENTER");
                input = Colorful.Console.ReadLine();
            }

            return fileTypes[input];
        }
    }
}
