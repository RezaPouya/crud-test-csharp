using PhoneNumbers;
using static PhoneNumbers.PhoneNumber.Types;

namespace Mc2.CrudTest.Domain.Helpers
{
    public static class PhoneNumberHelper
    {
        private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();

        public static bool IsValidCellphoneNumber(string phoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phoneNumber))
                    return false;

                PhoneNumber phone = PhoneNumberUtil.Parse(phoneNumber.ToLower().Trim(),
                    CountryCodeSource.UNSPECIFIED.ToString());

                return PhoneNumberUtil.GetNumberType(phone) == PhoneNumberType.MOBILE ||
                       PhoneNumberUtil.GetNumberType(phone) == PhoneNumberType.FIXED_LINE_OR_MOBILE;
            }
            catch (NumberParseException)
            {
                return false;
            }
        }

        public static string SanitizePhoneNumber(string phoneNumber)
        {
            return phoneNumber.Replace(" ", "")
                .Replace("+", "")
                .Replace("-", "")
                .Replace("_", "")
                .Replace(",", "")
                .ToLower()
                .Trim()
                .ToString();
        }
    }
}