using Json.Abstraction.Tests.Mocks.Models.Records;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Json.Abstraction.Tests;

public class CustomDiscriminatorPropertyTests : TestBase
{
    [Test]
    public void AbstractionSerialize_CustomDiscriminatorName()
    {
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(), new JsonAbstractionConverter(customDiscriminatorPropertyName: "_custom") }
        };

        var result = SerializeJson(new Record
            (
                TestParam1: "Test",
                TestParam2: 1,
                Param4: "test"
            ),
            typeof(RecordBase), serializerOptions);

        Assert.IsNotNull(result);
        Assert.AreEqual(
            @"{""_custom"":""Record"",""param4"":""test"",""testParam1"":""Test"",""testParam2"":1}"
            , result);
    }

    [Test]
    public void AbstractionSerialize_ExcludeDiscriminator()
    {
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(), new JsonAbstractionConverter(includeDiscriminatorProperty: false) }
        };

        var result = SerializeJson(new Record
            (
                TestParam1: "Test",
                TestParam2: 1,
                Param4: "test"
            ),
            typeof(RecordBase), serializerOptions);

        Assert.IsNotNull(result);
        Assert.AreEqual(
            @"{""param4"":""test"",""testParam1"":""Test"",""testParam2"":1}"
            , result);
    }
}
