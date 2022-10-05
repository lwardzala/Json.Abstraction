using Json.Abstraction.Tests.Mocks;
using Json.Abstraction.Tests.Mocks.Models.Records;
using NUnit.Framework;

namespace Json.Abstraction.Tests;

public class RecordsConverterTests : TestBase
{
    [Test]
    public void AbstractionDeserialize_Records()
    {
        var mock = RecordsMocks.GetRecordMock();

        var result = DeserializeJson<RecordBase>(mock.JsonData, mock.AbstractType);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<Record>(result, "Wrong instance");
        Assert.AreEqual("Test", result.TestParam1);
        Assert.AreEqual(1, result.TestParam2);
        Assert.AreEqual("test", ((Record)result).Param4.ToString());
    }

    [Test]
    public void AbstractionSerialize_Records()
    {
        var mock = RecordsMocks.GetRecordMock();

        var result = SerializeJson(mock.TestObject, mock.AbstractType);

        Assert.IsNotNull(result);
        Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
    }
}
