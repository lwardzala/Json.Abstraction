using System.Text.Json.Serialization;

namespace Json.Abstraction.Tests.Mocks.Models.Objects
{
    public interface INested
    {
        string Param2 { get; set; }
        INested NestedObject { get; set; }
    }

    public class Nested : INested
    {
        public string Param2 { get; set; }
        public INested NestedObject { get; set; }
    }

    public class ResourceWithObject
    {
        public int Param1 { get; set; }
        public INested NestedObject { get; set; }
    }

    public class ResourceWithPropertyNameAttr
    {
        public int Param1 { get; set; }
        [JsonPropertyName("param_2")]
        public int Param2 { get; set; }
        [JsonPropertyName("nested")]
        public INested NestedObject { get; set; }
    }
}
