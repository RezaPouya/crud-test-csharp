using Mc2.CrudTest.Domain.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.JsonConverters
{
    public class DateOfBirthJsonConverterTest
    {
        [Theory]
        [InlineData("01-JAN-2000", "01-Jan-2000")]
        [InlineData("01-jan-2000", "01-Jan-2000")]
        [InlineData("2000-01-01", "01-Jan-2000")]
        public void Should_be_able_to_cast_to_dateTime(string input, string exceptedResult)
        {
            var jsonConverter = new DateOfBirthJsonConverter();
            var dateTime = jsonConverter.ConvertToDateTime(input);
            var dateTimeString = dateTime.Date.ToString("dd-MMM-yyyy");
            Assert.Equal(dateTimeString, exceptedResult);
        }


        [Theory]
        [InlineData("01-JAN-2000", "01-Jan-2000")]
        public void Should_be_able_to_cast_to_string(string input, string exceptedResult)
        {
            var jsonConverter = new DateOfBirthJsonConverter();
            var dateTime = jsonConverter.ConvertToDateTime(input);
            Assert.Equal(jsonConverter.ConvertToString(dateTime), exceptedResult);
        }
    }
}
