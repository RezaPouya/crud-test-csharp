using System.Text;

namespace Mc2.CrudTest.Utility.Helpers
{
    public static class StringGeneratorHelper
    {
        private const string AllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#@$^*()";

        public static string GenerateRandom(int max)
            
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            char[] chars = new char[max];

            for (int i = 0; i < max; i++)
            {
                var character = AllowedChars[random.Next(AllowedChars.Length)]; 
                chars[i] = character;
            }

            return new string(chars);
        }
    }
}