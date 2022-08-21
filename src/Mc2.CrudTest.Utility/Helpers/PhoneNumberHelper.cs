using PhoneNumbers;
using static PhoneNumbers.PhoneNumber.Types;

namespace Mc2.CrudTest.Utility.Helpers
{
    public static class PhoneNumberHelper
    {
        public static bool IsValidCellphoneNumber(string phoneNumber)
        {
            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();

                PhoneNumber phone = phoneNumberUtil.Parse(phoneNumber.Trim(), CountryCodeSource.UNSPECIFIED.ToString());

                return phoneNumberUtil.GetNumberType(phone) == PhoneNumberType.MOBILE ||
                       phoneNumberUtil.GetNumberType(phone) == PhoneNumberType.FIXED_LINE_OR_MOBILE;
            }
            catch
            {
                return false;
            }
        }
    }
}