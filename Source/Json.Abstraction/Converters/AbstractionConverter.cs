using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using Json.Abstraction.Serializers;
using System.Runtime.Serialization;

namespace Json.Abstraction.Converters;

/// <summary>
/// Converts abstract types to objects with discriminator parameter
/// </summary>
/// <typeparam name="TAbstraction">Abstraction type</typeparam>
public class AbstractionConverter<TAbstraction> : ConverterBase<TAbstraction>
{
    private readonly string _discriminatorPropertyName = "_t";

    public override TAbstraction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DeserializeAbstractObject(ref reader, typeToConvert, options);
    }

    public override void Write(Utf8JsonWriter writer, TAbstraction value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString(_discriminatorPropertyName, value.GetType().Name);

        value.GetType().GetProperties()
            .Where(x => !Attribute.IsDefined(x, typeof(JsonIgnoreAttribute)))
            .ToList().ForEach(property =>
        {
            var propertyJsonName = ConvertPropertyName(options, property.Name);
            var propertyValue = value.GetType().GetProperty(property.Name)?.GetValue(value);

            if (propertyValue != null || !options.IgnoreNullValues)
            {
                writer.WritePropertyName(propertyJsonName);
                JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
            }
        });

        writer.WriteEndObject();
    }

    private TAbstraction DeserializeAbstractObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject) return default;

        var jsonElement = JsonSerializer.Deserialize<JsonElement>(ref reader, options);

        var type = GetAbstractType(typeToConvert, jsonElement);

        return (TAbstraction)CreateObjectInstance(type, jsonElement, options);
    }

    private object CreateObjectInstance(Type type, JsonElement jsonElement, JsonSerializerOptions options)
    {
        var instance = FormatterServices.GetUninitializedObject(type);

        type.GetProperties()
            .Where(x => !Attribute.IsDefined(x, typeof(JsonIgnoreAttribute)))
            .ToList().ForEach(property =>
        {
            var jsonPropertyName = ConvertPropertyName(options, property.Name);
            if (jsonElement.TryGetProperty(jsonPropertyName, out var jsonProperty))
            {
                try
                {
                    type.GetProperty(property.Name)?.SetValue(instance, JsonSerializer.Deserialize(jsonProperty, property.PropertyType, options));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Couldn't cast JSON {jsonPropertyName} value to {property.Name} of type {property.PropertyType.Name}", ex);
                }
            }
        });

        return instance;
    }

    private Type GetAbstractType(Type abstractType, JsonElement jsonElement)
    {
        if (!jsonElement.TryGetProperty(_discriminatorPropertyName, out var typeName))
            throw new Exception($"Json data of abstract object must contain '_t' property");

        var type = JsonAbstractSerializer.GetAbstractionType(abstractType, typeName.GetString());

        if (type == null) throw new Exception($"Couldn't cast type {abstractType.Name}");

        return type;
    }
}
