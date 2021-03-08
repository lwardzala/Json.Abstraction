using System;
using System.Text.Json;

namespace Json.Abstraction.Converters
{
    /// <summary>
    /// Converts anonymous objects
    /// </summary>
    public class ByteArrayConverter : ConverterBase<byte[]>
    {
        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var sByteArray = JsonSerializer.Deserialize<string>(ref reader);
            return Convert.FromBase64String(sByteArray);
        }

        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
