using System;
using System.Linq;
using System.Text.Json;

namespace Json.Abstraction.Converters
{
    /// <summary>
    /// Converts simple interface into an object (assumes that there is only one class of TInterface)
    /// </summary>
    /// <typeparam name="TInterface">Interface type</typeparam>
    /// <typeparam name="TClass">Class type</typeparam>
    public class InterfaceConverter<TInterface, TClass> : ConverterBase<TInterface> where TClass : TInterface
    {
        public override TInterface Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<TClass>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, TInterface value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            value.GetType().GetProperties().ToList().ForEach(property =>
            {
                var propertyJsonName = options.PropertyNamingPolicy.ConvertName(property.Name);
                var propertyValue = value.GetType().GetProperty(property.Name)?.GetValue(value);

                if (propertyValue != null || !options.IgnoreNullValues)
                {
                    writer.WritePropertyName(propertyJsonName);
                    JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
                }
            });

            writer.WriteEndObject();
        }
    }
}
