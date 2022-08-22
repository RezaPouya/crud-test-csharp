namespace System
{
    public static class StringHelper
    {
        public static string SanitizeToLower(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            return input.ToLower().Trim();
        }
    }
}