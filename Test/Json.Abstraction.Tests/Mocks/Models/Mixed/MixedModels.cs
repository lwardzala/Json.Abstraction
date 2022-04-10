using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Json.Abstraction.Tests.Mocks.Models.Mixed
{
    public enum TestEnum
    {
        TEST1,
        TEST2
    }

    public interface IModel
    {
        string Param1 { get; set; }
        DateTime DateTime { get; set; }
        NestedModelBase NestedModel { get; set; }
        byte[] ByteArray { get; set; }

        double? DoubleToAlwaysIgnore { get; set; }
        double? DoubleToNeverIgnore1 { get; set; }
        double? DoubleToNeverIgnore2 { get; set; }
        double? DoubleToIgnoreWhenWritingDefault1 { get; set; }
        double? DoubleToIgnoreWhenWritingDefault2 { get; set; }
        double? DoubleToIgnoreWhenWritingNull { get; set; }
    }

    public class Model : IModel
    {
        public string Param1 { get; set; }
        public DateTime DateTime { get; set; }
        public NestedModelBase NestedModel { get; set; }
        public byte[] ByteArray { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public double? DoubleToAlwaysIgnore { get; set; }

        public double? DoubleToNeverIgnore1 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public double? DoubleToNeverIgnore2 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? DoubleToIgnoreWhenWritingDefault1 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? DoubleToIgnoreWhenWritingDefault2 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? DoubleToIgnoreWhenWritingNull { get; set; }
    }

    public abstract class NestedModelBase
    {
        public int Param1 { get; set; }
    }

    public class NestedModel : NestedModelBase
    {
        public IModel Model { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class NestedModelAllBaseTypes : NestedModelBase
    {
        public IDictionary<string, string> Dictionary { get; set; }
        public TestEnum Enum { get; set; }
        public TestEnum EnumInt { get; set; }
        public DateTime DateTime { get; set; }
        public Guid Guid { get; set; }
        public bool Boolean { get; set; }
        public decimal Decimal { get; set; }
        public double Double { get; set; }
        public float Float { get; set; }
        public short Short { get; set; }
        public int Int { get; set; }
        public long Long { get; set; }
        public ushort UShort { get; set; }
        public uint UInt { get; set; }
        public ulong ULong { get; set; }
        public byte[] ByteArray { get; set; }
    }

    public class RawModel
    {
        public IDictionary<string, string> Dictionary { get; set; }
        public TestEnum Enum { get; set; }
        public DateTime DateTime { get; set; }
        public Guid Guid { get; set; }
        public bool Boolean { get; set; }
        public decimal Decimal { get; set; }
        public double Double { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public double? DoubleToAlwaysIgnore { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public double? DoubleToNeverIgnore1 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public double? DoubleToNeverIgnore2 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? DoubleToIgnoreWhenWritingDefault1 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? DoubleToIgnoreWhenWritingDefault2 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? DoubleToIgnoreWhenWritingNull { get; set; }

        public float Float { get; set; }
        public short Short { get; set; }
        public int Int { get; set; }
        public long Long { get; set; }
        public byte[] ByteArray { get; set; }
    }

    public interface IInterface
    {
        string Param1 { get; set; }
    }

    public class Impl1 : IInterface
    {
        public string Param1 { get; set; }
        public IInterface NestedInterface { get; set; }
    }

    public class Impl2 : IInterface
    {
        public string Param1 { get; set; }
    }
}
