namespace Mc2.CrudTest.Domain
{
    public static class ErrorMessages
    {
        public static Dictionary<string, string> Errors = new Dictionary<string, string>()
        {
            { ErrorCodes.CustomerErrorCodes.InvalidMobileNumber , "Invalid Mobile Number" },
            { ErrorCodes.CustomerErrorCodes.InvalidEmail , "Invalid Email address" },
            { ErrorCodes.CustomerErrorCodes.InvalidBankAccountNumber , "Invalid Bank Account Number" },
            { ErrorCodes.CustomerErrorCodes.DuplicateCustomerInfo , "Duplicate customer by First-name, Last-name, Date-of-Birth" },
            { ErrorCodes.CustomerErrorCodes.DuplicateEmail , "Duplicate customer by Email address" },
        };

        public static string GetMessage(string code)
        {
            var message = "";

            if (Errors.TryGetValue(code, out message))
            {
                return message.Trim();
            }

            return message.Trim(); ;
        }
    }
}