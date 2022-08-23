using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.Helpers
{
    public class StringHelperValidationTests
    {
        [Theory]
        [InlineData("a@b.com", "a@b.com")]
        [InlineData("A@b.com", "a@b.com")]
        [InlineData("   A@b.com", "a@b.com")]
        [InlineData("A@B   .com", "a@b   .com")]
        [InlineData("A@B   .cDDDDom   ", "a@b   .cddddom")]
        public void StringHelperTest_SanitizeToLower_WithExceptedResult(string input, string exceptedResult)
        {
            Assert.Equal(input.SanitizeToLower(), exceptedResult);
        }
    }
}
