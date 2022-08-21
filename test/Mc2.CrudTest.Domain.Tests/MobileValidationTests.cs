using Mc2.CrudTest.Utility.Helpers;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests;

public class MobileValidationTests
{
    [Theory]
    [InlineData("+989121234567", true)]
    [InlineData("+982188776655", false)]
    [InlineData("+60121234567", true)]
    [InlineData("+31612345678", true)]
    [InlineData("+16158887766", true)]
    public void MobileValidationTest_WithExpectedResult(string phoneNumber, bool expectedResult)
    {
        bool testResult = PhoneNumberHelper.IsValidCellphoneNumber(phoneNumber);
        
        Assert.Equal(expectedResult, testResult);
    }
}