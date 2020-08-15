using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Json.Abstraction.Converters;
using Json.Abstraction.Extensions;
using Json.Abstraction.Serializers;

namespace Json.Abstraction
{
    public enum SerializableType
    {
        Abstraction,
        Interface,
        Object,
        Other
    }

    /// <summary>
    /// Converts abstract types to their appropriate instances
    /// </summary>
    public sealed class JsonAbstractionConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            switch (GetSerializableType(typeToConvert))
            {
                case SerializableType.Interface:
                    return JsonAbstractSerializer.GetInterfaceType(typeToConvert) != null;
                case SerializableType.Abstraction:
                    return typeToConvert.IsAbstract;
                case SerializableType.Object:
                    return true;
                default:
                    return false;
            }
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            switch (GetSerializableType(typeToConvert))
            {
                case SerializableType.Interface:
                    var baseType = JsonAbstractSerializer.GetInterfaceType(typeToConvert);
                    var typeParams = new [] { typeToConvert, baseType };

                    return (JsonConverter)Activator.CreateInstance(typeof(InterfaceConverter<,>).MakeGenericType(typeParams));
                case SerializableType.Abstraction:
                    return (JsonConverter)Activator.CreateInstance(typeof(AbstractionConverter<>).MakeGenericType(typeToConvert));
                case SerializableType.Object:
                    return new ObjectConverter();
                default:
                    throw new Exception($"Unsupported serialization type of {typeToConvert.Name}");
            }
        }

        private SerializableType GetSerializableType(Type typeToConvert)
        {
            if (typeToConvert.IsGenericEnumerable() || typeToConvert.IsDictionary())
            {
                return SerializableType.Other;
            }

            if (typeToConvert.IsInterface)
            {
                var type = JsonAbstractSerializer.GetInterfaceType(typeToConvert);

                return (type != null) ? SerializableType.Interface : SerializableType.Abstraction;
            }

            if (typeToConvert.IsAbstract) return SerializableType.Abstraction;

            return SerializableType.Other;
        }
    }
}
