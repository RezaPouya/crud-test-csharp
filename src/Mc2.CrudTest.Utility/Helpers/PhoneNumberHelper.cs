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

                return phoneNumberUtil.IsPossibleNumberForType(phone, PhoneNumberType.MOBILE);
            }
            catch
            {
                return false;
            }
        }
    }
}