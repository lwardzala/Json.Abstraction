using System;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Json.Abstraction.Tests
{
    public abstract class TestBase
    {
        protected readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(), new JsonAbstractionConverter() }
        };

        protected T DeserializeJson<T>(string jsonData, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize<T>(jsonData, options ?? SerializerOptions);
        }

        protected T DeserializeJson<T>(string jsonData, Type typeToConvert, JsonSerializerOptions options = null) where T : class
        {
            return (T)JsonSerializer.Deserialize(jsonData, typeToConvert, options ?? SerializerOptions);
        }

        protected string SerializeJson<T>(object obj, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Serialize(obj, typeof(T), options ?? SerializerOptions);
        }

        protected string SerializeJson(object obj, Type valueType, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Serialize(obj, valueType, options ?? SerializerOptions);
        }

        protected string GetNormalizedJson(string jsonData)
        {
            return Regex.Replace(jsonData, @"\s+", string.Empty);
        }
    }
}
