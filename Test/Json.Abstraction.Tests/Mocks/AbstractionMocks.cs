using System;
using System.Collections.Generic;

using Json.Abstraction.Tests.Mocks.Models.Abstraction;

namespace Json.Abstraction.Tests.Mocks
{
    public static class AbstractionMocks
    {
        public static (string JsonData, object TestObject, Type AbstractType) GetListOfStringsMock()
        {
            return (
                @"{
                    ""_t"": ""ListOfStrings"",
                    ""collection"": [
                        ""opt1"",
                        ""opt2""
                    ],
                    ""param1"": ""Test""
                }",
                new ListOfStrings
                {
                    Param1 = "Test",
                    Collection = new List<string>
                    {
                        "opt1",
                        "opt2"
                    }
                },
                typeof(CollectionObjectsAbstraction));
        }

        public static (string JsonData, object TestObject, Type AbstractType) GetListOfStringsWithPropertyNameMock()
        {
            return (
                @"{
                    ""_t"": ""ListOfStringsWithPropertyName"",
                    ""col"": [
                        ""opt1"",
                        ""opt2""
                    ],
                    ""param1"": ""Test""
                }",
                new ListOfStringsWithPropertyName
                {
                    Param1 = "Test",
                    Collection = new List<string>
                    {
                        "opt1",
                        "opt2"
                    }
                },
                typeof(CollectionObjectsAbstraction));
        }

        public static (string JsonData, object TestObject, Type AbstractType) GetArrayOfStringsMock()
        {
            return (
                @"{
                    ""_t"": ""ArrayOfStrings"",
                    ""collection"": [
                        ""opt1"",
                        ""opt2""
                    ],
                    ""param1"": ""Test""
                }",
                new ArrayOfStrings
                {
                    Param1 = "Test",
                    Collection = new string[]
                    {
                        "opt1",
                        "opt2"
                    }
                },
                typeof(CollectionObjectsAbstraction));
        }

        public static (string JsonData, object TestObject, Type AbstractType) GetListOfObjectsMock()
        {
            return (
                @"{
                    ""_t"": ""ListOfObjects"",
                    ""collection"": [
                        {
                            ""param1"": 3
                        },
                        {
                            ""param1"": 4
                        }
                    ],
                    ""param1"": ""Test""
                }",
                new ListOfObjects
                {
                    Param1 = "Test",
                    Collection = new List<Nested>
                    {
                        new Nested { Param1 = 3 },
                        new Nested { Param1 = 4 }
                    }
                },
                typeof(CollectionObjectsAbstraction));
        }

        public static (string JsonData, object TestObject, Type AbstractType) GetArrayOfObjectsMock()
        {
            return (
                @"{
                    ""_t"": ""ArrayOfObjects"",
                    ""collection"": [
                        {
                            ""param1"": 3
                        },
                        {
                            ""param1"": 4
                        }
                    ],
                    ""param1"": ""Test""
                }",
                new ArrayOfObjects
                {
                    Param1 = "Test",
                    Collection = new Nested[]
                    {
                        new Nested { Param1 = 3 },
                        new Nested { Param1 = 4 }
                    }
                },
                typeof(CollectionObjectsAbstraction));
        }
        public static (string JsonData, object TestObject, Type AbstractType) GetListOfAbstractionsMock()
        {
            return (
                @"{
                    ""_t"": ""ListOfAbstractions"",
                    ""collection"": [
                        {
                            ""_t"": ""Nested"",
                            ""param1"": 3
                        },
                        {
                            ""_t"": ""Nested"",
                            ""param1"": 4
                        }
                    ],
                    ""param1"": ""Test""
                }",
                new ListOfAbstractions
                {
                    Param1 = "Test",
                    Collection = new List<NestedBase>
                    {
                        new Nested { Param1 = 3 },
                        new Nested { Param1 = 4 }
                    }
                },
                typeof(CollectionObjectsAbstraction));
        }

        public static (string JsonData, object TestObject, Type AbstractType) GetListOfInterfacesMock()
        {
            return (
                @"{
                    ""_t"": ""ListOfInterfaces"",
                    ""collection"": [
                        {
                            ""param1"": 3
                        },
                        {
                            ""param1"": 4
                        }
                    ],
                    ""param1"": ""Test""
                }",
                new ListOfInterfaces
                {
                    Param1 = "Test",
                    Collection = new List<INested>
                    {
                        new Nested { Param1 = 3 },
                        new Nested { Param1 = 4 }
                    }
                },
                typeof(CollectionObjectsAbstraction));
        }

        public static (string JsonData, object TestObject, Type AbstractType) GetIListOfInterfacesMock()
        {
            return (
                @"{
                    ""_t"": ""IListOfInterfaces"",
                    ""collection"": [
                        {
                            ""param1"": 3
                        },
                        {
                            ""param1"": 4
                        }
                    ],
                    ""param1"": ""Test""
                }",
                new IListOfInterfaces
                {
                    Param1 = "Test",
                    Collection = new List<INested>
                    {
                        new Nested { Param1 = 3 },
                        new Nested { Param1 = 4 }
                    }
                },
                typeof(CollectionObjectsAbstraction));
        }
    }
}
