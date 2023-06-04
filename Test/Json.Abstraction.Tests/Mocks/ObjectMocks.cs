using System;

using Json.Abstraction.Tests.Mocks.Models.Objects;

namespace Json.Abstraction.Tests.Mocks
{
    public static class ObjectMocks
    {
        public static (string JsonData, object TestObject, Type typeToConvert) GetResourceWithObjectMock()
        {
            return (
                @"{
                    ""param1"": 3,
                    ""nestedObject"": {
                        ""param2"": ""Test""
                    }
                }",
                new ResourceWithObject
                {
                    Param1 = 3,
                    NestedObject = new Nested { Param2 = "Test" }
                },
                typeof(ResourceWithObject));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetResourceWithPropertyNameAttrMock()
        {
            return (
                @"{
                    ""param1"": 3,
                    ""param_2"": 5,
                    ""nested"": {
                        ""param2"": ""Test""
                    }
                }",
                new ResourceWithPropertyNameAttr
                {
                    Param1 = 3,
                    Param2 = 5,
                    NestedObject = new Nested { Param2 = "Test" }
                },
                typeof(ResourceWithPropertyNameAttr));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetObjectWithNestedObjectMock()
        {
            return (
                @"{
                    ""param2"": ""Test"",
                    ""nestedObject"": {
                        ""param2"": ""Test2""
                    }
                }",
                new Nested
                {
                    Param2 = "Test",
                    NestedObject = new Nested { Param2 = "Test2" }
                },
                typeof(INested));
        }
    }
}
