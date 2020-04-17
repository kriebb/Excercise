namespace Ordina.FileReading.Console
{
    internal class StartOptions
    {
        public StartOptions(string file, string fileType, bool useEncryption, bool useRbac, string userRole)
        {
            File = file;
            FileType = fileType;
            UseEncryption = useEncryption;
            UseRbac = useRbac;
            UserRole = userRole;
        }

        public string File { get; private set; }
        public string FileType { get; private set; }
        public bool UseEncryption { get; private set; }
        public bool UseRbac { get; private set; }
        public string UserRole { get; private set; }
    }
}