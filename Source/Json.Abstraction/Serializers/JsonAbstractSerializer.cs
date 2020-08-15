using System;
using System.Linq;

using Json.Abstraction.Repositories;
using Json.Abstraction.Extensions;

namespace Json.Abstraction.Serializers
{
    public static class JsonAbstractSerializer
    {
        public static void RegisterInterfaceConversion(Type interfaceType, Type classType)
        {
            if (!interfaceType.IsInterface) throw new Exception($"{interfaceType.Name} must be an interface");
            if (!classType.IsClass) throw new Exception($"{classType.Name} must be a class");

            JsonSerializerRepository.Instance.AddInterfaceDictionary(interfaceType, classType);
        }

        public static void RegisterInterfaceConversion<TInterface, TClass>() where TClass : class, TInterface
        {
            var interfaceType = typeof(TInterface);
            var baseType = typeof(TClass);

            RegisterInterfaceConversion(interfaceType, baseType);
        }

        public static void RegisterAbstractionConversion<TAbstraction>(params Type[] types)
        {
            JsonSerializerRepository.Instance.AddAbstractionDictionary(typeof(TAbstraction), types);
        }

        public static Type GetAbstractionType(Type abstractType, string classTypeName)
        {
            var typeFromReflection = abstractType.GetAllInheritedTypes().FirstOrDefault(_ => _.Name == classTypeName);

            if (typeFromReflection != null) return typeFromReflection;

            if (!JsonSerializerRepository.Instance.AbstractionDictionary.ContainsKey(abstractType)) return null;

            var type = JsonSerializerRepository.Instance.AbstractionDictionary[abstractType].FirstOrDefault(_ => _.Name == classTypeName);

            return type;
        }

        public static Type GetInterfaceType(Type interfaceType)
        {
            Type[] typesFromReflection = interfaceType.GetAllInheritedTypes();

            if (typesFromReflection.Length == 1) return typesFromReflection.First();

            if (!JsonSerializerRepository.Instance.InterfaceDictionary.ContainsKey(interfaceType)) return null;

            return JsonSerializerRepository.Instance.InterfaceDictionary[interfaceType];
        }
    }
}
