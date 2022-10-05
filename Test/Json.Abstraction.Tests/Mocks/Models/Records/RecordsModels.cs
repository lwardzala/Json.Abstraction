namespace Json.Abstraction.Tests.Mocks.Models.Records;

public abstract record RecordBase(string TestParam1, int TestParam2);

public record Record(string TestParam1, int TestParam2, object Param4) : RecordBase(TestParam1, TestParam2);