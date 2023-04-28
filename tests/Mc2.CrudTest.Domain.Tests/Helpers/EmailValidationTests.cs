using Mc2.CrudTest.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.Helpers
{
    public class EmailValidationTests
    {
        [Theory]
        [InlineData("a@b.com", true)]
        [InlineData("r.pouya@hotmail.com", true)]
        [InlineData("aaaaa@aaad", false)]
        [InlineData("example@email.com", true)]
        [InlineData("example.first.middle.lastname@email.com", true)]
        [InlineData("example@subdomain.email.com", true)]
        [InlineData("example+firstname+lastname@email.com", true)]
        [InlineData("example@234.234.234.234", true)]
        [InlineData("example@[234.234.234.234]", true)]
        [InlineData("0987654321@example.com", true)]
        [InlineData("example@email-one.com", true)]
        [InlineData("_______@email.com", true)]
        [InlineData("example@email.name", true)]
        [InlineData("example@email.co.jp", true)]
        public void EmailValidationTest_WithExceptedResult(string email , bool exceptedResult)
        {
            Assert.Equal(EmailHelper.IsValidEmail(email), exceptedResult);
        }
    }
}
