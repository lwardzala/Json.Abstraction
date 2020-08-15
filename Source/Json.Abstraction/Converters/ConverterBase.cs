using System.Text.Json;
using System.Text.Json.Serialization;

namespace Json.Abstraction.Converters
{
    public abstract class ConverterBase<T> : JsonConverter<T>
    {
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
    }
}
