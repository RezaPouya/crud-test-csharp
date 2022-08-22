using Mc2.CrudTest.Domain.Helpers;
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


    [Theory]
    [InlineData("+989121234567", "989121234567")]
    [InlineData("+982188776655", "982188776655")]
    [InlineData("+60-121234567", "60121234567")]
    [InlineData("+31 612345678", "31612345678")]
    [InlineData("+16_158887766", "16158887766")]
    public void MobileSanitizationTest_WithExpectedResult(string phoneNumber, string expectedResult)
    {
        string testResult = PhoneNumberHelper.SanitizePhoneNumber(phoneNumber);

        Assert.Equal(expectedResult, testResult);
    }
}