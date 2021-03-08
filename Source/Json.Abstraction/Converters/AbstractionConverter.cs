using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Json.Abstraction.Extensions;
using Json.Abstraction.Serializers;

namespace Json.Abstraction.Converters
{
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

        private TAbstraction DeserializeAbstractObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject) return default;

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(ref reader, options);

            var type = GetAbstractType(typeToConvert, jsonElement);

            return (TAbstraction)CreateObjectInstance(type, jsonElement, options);
        }

        private object CreateObjectInstance(Type type, JsonElement jsonElement, JsonSerializerOptions options)
        {
            var instance = Activator.CreateInstance(type);

            type.GetProperties().ToList().ForEach(property =>
            {
                var jsonPropertyName = options.PropertyNamingPolicy.ConvertName(property.Name);
                if (jsonElement.TryGetProperty(jsonPropertyName, out var jsonProperty))
                {
                    try
                    {
                        type.GetProperty(property.Name)?.SetValue(instance, GetValue(property.PropertyType, jsonProperty, options));
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

        private Type GetType(Type abstractType, JsonElement jsonElement)
        {
            if (!abstractType.IsAbstract && !abstractType.IsInterface) return abstractType;

            var interfaceType = JsonAbstractSerializer.GetInterfaceType(abstractType);

            return interfaceType != null ? interfaceType : GetAbstractType(abstractType, jsonElement);
        }

        private object GetValue(Type valueType, JsonElement jsonElement, JsonSerializerOptions options)
        {
            if (valueType == typeof(Guid)) return jsonElement.GetGuid();
            if (valueType == typeof(DateTime)) return jsonElement.GetDateTime();
            if (valueType == typeof(string)) return jsonElement.GetString();
            if (valueType == typeof(bool)) return jsonElement.GetBoolean();
            if (valueType == typeof(short)) return jsonElement.GetInt16();
            if (valueType == typeof(int)) return jsonElement.GetInt32();
            if (valueType == typeof(long)) return jsonElement.GetInt64();
            if (valueType == typeof(ushort)) return jsonElement.GetUInt16();
            if (valueType == typeof(uint)) return jsonElement.GetUInt32();
            if (valueType == typeof(ulong)) return jsonElement.GetUInt64();
            if (valueType == typeof(decimal)) return jsonElement.GetDecimal();
            if (valueType == typeof(double)) return jsonElement.GetDouble();
            if (valueType == typeof(float)) return jsonElement.GetSingle();
            if (valueType == typeof(byte[])) return jsonElement.GetBytesFromBase64();
            if (valueType.IsEnum)
            {
                var enumConverter = options.Converters.FirstOrDefault(_ => _ is JsonStringEnumConverter);

                if (enumConverter != null)
                {
                    if (jsonElement.ValueKind == JsonValueKind.Number) return jsonElement.GetInt32();

                    return Enum.Parse(valueType, jsonElement.GetString());
                }

                return jsonElement.GetInt32();
            }
            if (valueType.IsDictionary())
            {
                Type[] genericTypes = valueType.GetGenericArguments();
                var dictionary = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(genericTypes));

                var objectEnumerator = jsonElement.EnumerateObject();

                do
                {
                    if (!objectEnumerator.Current.Equals(default(JsonProperty)))
                    {
                        dictionary.Add(objectEnumerator.Current.Name, GetValue(genericTypes[1], objectEnumerator.Current.Value, options));
                    }
                }
                while (objectEnumerator.MoveNext());

                return dictionary;
            }
            if (valueType.IsArray || valueType.IsGenericEnumerable())
            {
                var result = new ArrayList();
                Type elementsType = null;

                if (valueType.IsArray) elementsType = valueType.GetElementType();
                if (valueType.IsGenericEnumerable()) elementsType = valueType.GetGenericArguments().FirstOrDefault();

                jsonElement.EnumerateArray().ToList().ForEach(item =>
                {
                    result.Add(GetValue(elementsType, item, options));
                });

                if (valueType.IsGenericEnumerable())
                {
                    if (valueType.IsInterface)
                    {
                        return Activator.CreateInstance(typeof(List<>).MakeGenericType(valueType.GetGenericArguments()[0]),
                            result.ToArray(valueType.GetGenericArguments()[0]));
                    }
                    return Activator.CreateInstance(valueType, new object[] { result.ToArray(valueType.GetGenericArguments()[0]) });
                }

                return result.ToArray(valueType.GetElementType());
            }
            if (valueType.IsAbstract)
            {
                var type = GetType(valueType, jsonElement);

                return CreateObjectInstance(type, jsonElement, options);
            }
            if (valueType.IsClass) return CreateObjectInstance(valueType, jsonElement, options);

            return null;
        }
    }
}
