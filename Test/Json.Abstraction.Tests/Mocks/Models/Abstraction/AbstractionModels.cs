using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Json.Abstraction.Tests.Mocks.Models.Abstraction
{
    public interface INested
    {
        int Param1 { get; set; }
    }
    public abstract class NestedBase
    {
        public int Param1 { get; set; }
    }

    public class Nested : NestedBase, INested
    {
        public string Param2 { get; set; }
    }

    public abstract class CollectionObjectsAbstraction
    {
        public string Param1 { get; set; }
    }

    public class ListOfStringsWithPropertyName : CollectionObjectsAbstraction
    {
        [JsonPropertyName("col")]
        public List<string> Collection { get; set; }
    }

    public class ListOfStrings : CollectionObjectsAbstraction
    {
        public List<string> Collection { get; set; }
    }

    public class ArrayOfStrings : CollectionObjectsAbstraction
    {
        public string[] Collection { get; set; }
    }

    public class ListOfObjects : CollectionObjectsAbstraction
    {
        public List<Nested> Collection { get; set; }
    }

    public class ArrayOfObjects : CollectionObjectsAbstraction
    {
        public Nested[] Collection { get; set; }
    }

    public class ListOfAbstractions : CollectionObjectsAbstraction
    {
        public List<NestedBase> Collection { get; set; }
    }

    public class ListOfInterfaces : CollectionObjectsAbstraction
    {
        public List<INested> Collection { get; set; }
    }

    public class IListOfInterfaces : CollectionObjectsAbstraction
    {
        public IList<INested> Collection { get; set; }
    }
}
