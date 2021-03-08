using System;
using System.Collections.Generic;

using Json.Abstraction.Tests.Mocks.Models.Mixed;

namespace Json.Abstraction.Tests.Mocks
{
    public static class MixedMocks
    {
        public static (string JsonData, object TestObject, Type typeToConvert) GetInterfaceWithNestedAbstractMock()
        {
            return (
                @"{
                    ""param1"": ""Test"",
                    ""dateTime"": ""2020-08-12T19:35:00"",
                    ""nestedModel"": {
                        ""_t"": ""NestedModel"",
                        ""model"": {
                            ""param1"": ""Test2"",
                            ""dateTime"": ""2020-08-12T19:35:00""
                        },
                        ""dateTime"": ""2020-08-12T19:35:00"",
                        ""param1"": 3
                    }
                }",
                new Model
                {
                    Param1 = "Test",
                    DateTime = DateTime.Parse("2020-08-12T19:35:00"),
                    NestedModel = new NestedModel
                    {
                        Param1 = 3,
                        Model = new Model
                        {
                            Param1 = "Test2",
                            DateTime = DateTime.Parse("2020-08-12T19:35:00")
                        },
                        DateTime = DateTime.Parse("2020-08-12T19:35:00")
                    }
                },
                typeof(IModel));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetNestedModelAllBaseTypesMock()
        {
            return (
                @"{
                    ""_t"": ""NestedModelAllBaseTypes"",
                    ""dictionary"": {""testKey"": ""testValue""},
                    ""enum"": ""TEST2"",
                    ""enumInt"": 1,
                    ""dateTime"": ""2020-08-12T19:35:00"",
                    ""guid"": ""0f8fad5b-d9cb-469f-a165-70867728950e"",
                    ""boolean"": true,
                    ""decimal"": 10,
                    ""double"": 2.456,
                    ""float"": 45.35665,
                    ""short"": 3,
                    ""int"": -456,
                    ""long"": 467585,
                    ""uShort"": 455,
                    ""uInt"": 45545545,
                    ""uLong"": 747474757575,
                    ""byteArray"": ""dGVzdA=="",
                    ""param1"": 0
                }",
                new NestedModelAllBaseTypes
                {
                    Dictionary = new Dictionary<string, string> { { "testKey", "testValue" } },
                    Enum = TestEnum.TEST2,
                    EnumInt = (TestEnum)1,
                    DateTime = DateTime.Parse("2020-08-12T19:35:00"),
                    Guid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Boolean = true,
                    Decimal = 10,
                    Double = 2.456,
                    Float = 45.35665F,
                    Short = 3,
                    Int = -456,
                    Long = 467585,
                    UShort = 455,
                    UInt = 45545545,
                    ULong = 747474757575,
                    ByteArray = Convert.FromBase64String("dGVzdA=="),
                    Param1 = 0
                },
                typeof(NestedModelBase));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetRawModelMock()
        {
            return (
                @"{
                    ""dictionary"": {""testKey"": ""testValue""},
                    ""enum"": ""TEST2"",
                    ""dateTime"": ""2020-08-12T19:35:00"",
                    ""guid"": ""0f8fad5b-d9cb-469f-a165-70867728950e"",
                    ""boolean"": true,
                    ""decimal"": 10,
                    ""double"": 2.456,
                    ""float"": 45.35665,
                    ""short"": 3,
                    ""int"": -456,
                    ""long"": 467585,
                    ""byteArray"": ""dGVzdA==""
                }",
                new RawModel
                {
                    Dictionary = new Dictionary<string, string> { { "testKey", "testValue" } },
                    Enum = TestEnum.TEST2,
                    DateTime = DateTime.Parse("2020-08-12T19:35:00"),
                    Guid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Boolean = true,
                    Decimal = 10,
                    Double = 2.456,
                    Float = 45.35665F,
                    Short = 3,
                    Int = -456,
                    Long = 467585,
                    ByteArray = Convert.FromBase64String("dGVzdA==")
                },
                typeof(RawModel));
        }

        public static (string JsonData, object TestObject, Type typeToConvert) GetInterfaceWithMultipleClassesMock()
        {
            return (
                @"{
                    ""_t"": ""Impl1"",
                    ""param1"": ""Test"",
                    ""nestedInterface"": {
                        ""_t"": ""Impl2"",
                        ""param1"": ""Test2""
                    }
                }",
                new Impl1
                {
                    Param1 = "Test",
                    NestedInterface = new Impl2
                    {
                        Param1 = "Test2"
                    }
                },
                typeof(IInterface));
        }
    }
}
