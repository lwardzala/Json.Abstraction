using System;
using System.Collections.Generic;

using Json.Abstraction.Tests.Mocks.Models.Collection;

namespace Json.Abstraction.Tests.Mocks
{
    public static class CollectionMocks
    {
        public static (string JsonData, object TestObject, Type typeToConvert) GetResourceWithListOfInterfacesMock()
        {
            return (
                @"{
                    ""collection"": [
                        {
                            ""param1"": ""Test""
                        },
                        {
                            ""param1"": ""Test2""
                        }
                    ]
                }",
                new ResourceWithListOfInterfaces
                {
                    Collection = new List<INested2>
                    {
                        new Nested2 { Param1 = "Test" },
                        new Nested2 { Param1 = "Test2" }
                    }
                },
                typeof(ResourceWithListOfInterfaces));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetResourceWithIListOfInterfacesMock()
        {
            return (
                @"{
                    ""collection"": [
                        {
                            ""param1"": ""Test""
                        },
                        {
                            ""param1"": ""Test2""
                        }
                    ]
                }",
                new ResourceWithIListOfInterfaces
                {
                    Collection = new List<INested2>
                    {
                        new Nested2 { Param1 = "Test" },
                        new Nested2 { Param1 = "Test2" }
                    }
                },
                typeof(ResourceWithIListOfInterfaces));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetResourceWithArrayOfInterfacesMock()
        {
            return (
                @"{
                    ""collection"": [
                        {
                            ""param1"": ""Test""
                        },
                        {
                            ""param1"": ""Test2""
                        }
                    ]
                }",
                new ResourceWithArrayOfInterfaces
                {
                    Collection = new INested2[]
                    {
                        new Nested2 { Param1 = "Test" },
                        new Nested2 { Param1 = "Test2" }
                    }
                },
                typeof(ResourceWithArrayOfInterfaces));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetListOfInterfacesMock()
        {
            return (
                @"[
                    {
                        ""param1"": ""Test""
                    },
                    {
                        ""param1"": ""Test2""
                    }
                ]",
                new List<INested2>
                {
                    new Nested2 { Param1 = "Test" },
                    new Nested2 { Param1 = "Test2" }
                },
                typeof(List<INested2>));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetIListOfInterfacesMock()
        {
            return (
                @"[
                    {
                        ""param1"": ""Test""
                    },
                    {
                        ""param1"": ""Test2""
                    }
                ]",
                new List<INested2>
                {
                    new Nested2 { Param1 = "Test" },
                    new Nested2 { Param1 = "Test2" }
                },
                typeof(IList<INested2>));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetArrayOfInterfacesMock()
        {
            return (
                @"[
                    {
                        ""param1"": ""Test""
                    },
                    {
                        ""param1"": ""Test2""
                    }
                ]",
                new INested2[]
                {
                    new Nested2 { Param1 = "Test" },
                    new Nested2 { Param1 = "Test2" }
                },
                typeof(INested2[]));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetIEnumerableOfInterfacesMock()
        {
            return (
                @"[
                    {
                        ""param1"": ""Test""
                    },
                    {
                        ""param1"": ""Test2""
                    }
                ]",
                new INested2[]
                {
                    new Nested2 { Param1 = "Test" },
                    new Nested2 { Param1 = "Test2" }
                },
                typeof(IEnumerable<INested2>));
        }
    }
}
