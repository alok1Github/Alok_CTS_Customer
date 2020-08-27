using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Customer.API
{
    public class DateTimeConverter : JsonConverter<DateTimeOffset>
    {
        //   private const string TwitterDateFormat = "ddd MMM dd HH:mm:ss +ffff yyyy";

        private const string TwitterDateFormat = "MM/dd/yyyy";

        public override DateTimeOffset Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return DateTimeOffset.ParseExact(reader.GetString(), TwitterDateFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTimeOffset value,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString(TwitterDateFormat, CultureInfo.InvariantCulture));
    }
}

