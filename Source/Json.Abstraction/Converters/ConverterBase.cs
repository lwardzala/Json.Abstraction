using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Json.Abstraction.Extensions;

namespace Json.Abstraction.Converters
{
    public abstract class ConverterBase<T> : JsonConverter<T>
    {
        protected string ConvertDictionaryKey(JsonSerializerOptions options, string key)
        {
            if (options.DictionaryKeyPolicy != null)
                return options.DictionaryKeyPolicy.ConvertName(key);

            return key;
        }

        protected string ConvertPropertyName(JsonSerializerOptions options, string name)
        {
            if (options.PropertyNamingPolicy != null)
                return options.PropertyNamingPolicy.ConvertName(name);

            return name;
        }

        protected object DeserializeBaseTypes(ref Utf8JsonReader reader)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.False:
                case JsonTokenType.True:
                    reader.Read();
                    return reader.GetBoolean();
                case JsonTokenType.Number:
                    if (reader.TryGetInt32(out var intVal)) return intVal;
                    if (reader.TryGetDouble(out var doubleVal)) return doubleVal;
                    return null;
                case JsonTokenType.String:
                    return reader.GetString();
                default:
                    return null;
            }
        }

        protected static bool GetIgnoreProperty(JsonSerializerOptions options, PropertyInfo property, object propertyValue)
        {
            var ignoredAttribute = Attribute.GetCustomAttribute(property, typeof(JsonIgnoreAttribute)) as JsonIgnoreAttribute;
            var ignoreCondition = ignoredAttribute?.Condition ?? options.DefaultIgnoreCondition;

            return
                ignoreCondition == JsonIgnoreCondition.Always ||
                (ignoreCondition == JsonIgnoreCondition.WhenWritingDefault && property.PropertyType.GetDefault() == propertyValue) ||
                (ignoreCondition == JsonIgnoreCondition.WhenWritingNull || options.IgnoreNullValues) && propertyValue == null;
        }
    }
}
