using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Customer.API
{
    public class DateTimeConverter : JsonConverter<DateTimeOffset>
    {
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

