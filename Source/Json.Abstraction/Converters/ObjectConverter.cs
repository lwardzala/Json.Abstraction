using System;
using System.Collections;
using System.Text.Json;

namespace Json.Abstraction.Converters
{
    /// <summary>
    /// Converts anonymous objects
    /// </summary>
    public class ObjectConverter : ConverterBase<object>
    {
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ParseObject(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        private object ParseObject(ref Utf8JsonReader reader, Type refObjectType, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.StartArray:
                    reader.Read();
                    var result = new ArrayList();
                    while (reader.TokenType != JsonTokenType.EndArray)
                    {
                        result.Add(ParseObject(ref reader, refObjectType.GetElementType(), options));
                        reader.Read();
                    }
                    return result;
                case JsonTokenType.StartObject:
                    return JsonSerializer.Deserialize<object>(ref reader, options);
                default:
                    return DeserializeBaseTypes(ref reader);
            }
        }
    }
}
