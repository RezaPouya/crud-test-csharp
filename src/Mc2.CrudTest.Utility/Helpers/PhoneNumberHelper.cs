using PhoneNumbers;
using static PhoneNumbers.PhoneNumber.Types;

namespace Mc2.CrudTest.Utility.Helpers
{
    public static class PhoneNumberHelper
    {
        private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();

        public static bool IsValidCellphoneNumber(string phoneNumber)
        {
            try
            {
                
                PhoneNumber phone = PhoneNumberUtil.Parse(phoneNumber.Trim(),
                    CountryCodeSource.UNSPECIFIED.ToString());

                return PhoneNumberUtil.GetNumberType(phone) == PhoneNumberType.MOBILE ||
                       PhoneNumberUtil.GetNumberType(phone) == PhoneNumberType.FIXED_LINE_OR_MOBILE;
            }
            catch(NumberParseException)
            {
                return false;
            }
        }
    }
}