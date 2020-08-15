using System;
using System.Linq;
using System.Collections.Generic;

namespace Json.Abstraction.Repositories
{
    internal sealed class JsonSerializerRepository
    {
        private static JsonSerializerRepository _instance;
        private static readonly object _padlock = new object();
        public Dictionary<Type, Type> InterfaceDictionary { get; }
        public Dictionary<Type, List<Type>> AbstractionDictionary { get; }

        JsonSerializerRepository()
        {
            InterfaceDictionary = new Dictionary<Type, Type>();
            AbstractionDictionary = new Dictionary<Type, List<Type>>();
        }

        public static JsonSerializerRepository Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new JsonSerializerRepository();
                    }

                    return _instance;
                }
            }
        }

        public void AddInterfaceDictionary(Type interfaceType, Type classType)
        {
            if (!InterfaceDictionary.ContainsKey(interfaceType))
            {
                InterfaceDictionary.Add(interfaceType, classType);
            }
        }

        public void AddAbstractionDictionary(Type abstractType, Type[] classTypes)
        {
            if (!AbstractionDictionary.ContainsKey(abstractType))
            {
                AbstractionDictionary.Add(abstractType, new List<Type>(classTypes));
            }
            else
            {
                AbstractionDictionary[abstractType] = AbstractionDictionary[abstractType].Concat(classTypes).Distinct().ToList();
            }
        }
    }
}
