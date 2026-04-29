using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GardenHelperApp.Server.Converters
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Treat incoming timestamps as LOCAL time
            var dt = reader.GetDateTime();
            return DateTime.SpecifyKind(dt, DateTimeKind.Local);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Write local time WITHOUT converting to UTC or adding "Z"
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss"));
        }
    }
}
