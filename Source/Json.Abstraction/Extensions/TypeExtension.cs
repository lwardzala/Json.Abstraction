using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Json.Abstraction.Extensions
{
    /// <summary>
    /// Type extensions
    /// </summary>
    public static class TypeExtension
    {
        public static bool IsDictionary(this Type type)
        {
            if (type.IsGenericType && (typeof(IDictionary).IsAssignableFrom(type) || type.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            {
                return true;
            }

            return false;
        }

        public static bool IsGenericEnumerable(this Type type)
        {
            var genericType = type.GetGenericArguments()?.FirstOrDefault();

            if (typeof(IEnumerable).IsAssignableFrom(type) && (genericType != null))
            {
                return true;
            }

            return false;
        }

        public static Type[] GetAllInheritedTypes(this Type type)
        {
            if (type.IsInterface)
            {
                return Assembly.GetAssembly(type).GetTypes()
                    .Where(_ => _.IsClass && !_.IsAbstract && type.IsAssignableFrom(_)).ToArray();
            }

            if (type.IsAbstract)
            {
                return Assembly.GetAssembly(type).GetTypes()
                    .Where(_ => _.IsClass && !_.IsAbstract && _.IsSubclassOf(type)).ToArray();
            }

            return new Type[] { };
        }

        public static object GetDefault(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            
            return null;
        }
    }
}
