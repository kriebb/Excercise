namespace Ordina.FileReading.Console
{
    public static class StringExtensions
    {
        public static bool ToBoolean(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            if (value.ToLower() == "Yes".ToLower())
                return true;
            if (value.ToLower() == "No".ToLower())
                return false;

            return false;
        }
    }
}