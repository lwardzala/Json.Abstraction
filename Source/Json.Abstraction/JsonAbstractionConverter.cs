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
        ByteArray,
        Other
    }

    /// <summary>
    /// Converts abstract types to their appropriate instances.
    /// </summary>
    public sealed class JsonAbstractionConverter : JsonConverterFactory
    {
        /// <summary>
        /// Indicates whether to always include discriminator property for serialized objects.
        /// Discriminator is never set if value is <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        public bool? IncludeDiscriminatorProperty { get; init; }

        /// <summary>
        /// Custom discriminator property name.
        /// </summary>
        public string? CustomDiscriminatorPropertyName { get; init; }

        /// <summary>
        /// JsonAbstractionConverter constructor.
        /// </summary>
        public JsonAbstractionConverter()
        {
            IncludeDiscriminatorProperty = null;
            CustomDiscriminatorPropertyName = null;
        }

        /// <summary>
        /// JsonAbstractionConverter constructor.
        /// </summary>
        /// <param name="customDiscriminatorPropertyName">Custom discriminator property name.</param>
        public JsonAbstractionConverter(string customDiscriminatorPropertyName)
        {
            IncludeDiscriminatorProperty = true;
            CustomDiscriminatorPropertyName = customDiscriminatorPropertyName;
        }

        /// <summary>
        /// JsonAbstractionConverter constructor.
        /// </summary>
        /// <param name="includeDiscriminatorProperty">Indicates whether to always include discriminator property for serialized objects. Discriminator is never set if value is <c>false</c>. Default value is <c>true</c>.</param>
        /// <param name="customDiscriminatorPropertyName">Custom discriminator property name.</param>
        public JsonAbstractionConverter(bool includeDiscriminatorProperty, string? customDiscriminatorPropertyName = null)
        {
            IncludeDiscriminatorProperty = includeDiscriminatorProperty;
            CustomDiscriminatorPropertyName = customDiscriminatorPropertyName;
        }

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
                    var typeParams = new[] { typeToConvert, baseType };

                    return (JsonConverter)Activator.CreateInstance(typeof(InterfaceConverter<,>).MakeGenericType(typeParams));
                case SerializableType.Abstraction:
                    return (JsonConverter)Activator.CreateInstance(typeof(AbstractionConverter<>).MakeGenericType(typeToConvert), IncludeDiscriminatorProperty ?? true, CustomDiscriminatorPropertyName ?? "_t");
                case SerializableType.Object:
                    return new ObjectConverter();
                case SerializableType.ByteArray:
                    return new ByteArrayConverter();
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

            if (typeToConvert == typeof(byte[])) return SerializableType.ByteArray;

            return SerializableType.Other;
        }
    }
}
