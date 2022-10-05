using Json.Abstraction.Tests.Mocks.Models.Records;
using System;

namespace Json.Abstraction.Tests.Mocks;

public static class RecordsMocks
{
    public static (string JsonData, object TestObject, Type AbstractType) GetRecordMock()
    {
        return (
            @"{
                ""_t"": ""Record"",
                ""param4"": ""test"",
                ""testParam1"": ""Test"",
                ""testParam2"": 1
            }",
            new Record
            (
                TestParam1: "Test",
                TestParam2: 1,
                Param4: "test"
            ),
            typeof(RecordBase));
    }
}
