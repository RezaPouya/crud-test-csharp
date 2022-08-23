using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mc2.CrudTest.Domain.JsonConverters
{
    public class DateOfBirthJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ConvertToDateTime(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(ConvertToString(value));
        }

        public DateTime ConvertToDateTime(string? dateTime)
        {
            if (string.IsNullOrEmpty(dateTime))
                throw new ArgumentException("Date of birth cannot be null or empty ");

            return DateTime.ParseExact(dateTime, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
            //return Convert.ToDateTime(dateTime?.ToUpper());
        }

        public string ConvertToString(DateTime date)
        {
            return date.Date.ToString("dd-MMM-yyyy");
        }
    }
}