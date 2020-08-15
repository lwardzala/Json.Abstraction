using System.Collections.Generic;

namespace Json.Abstraction.Tests.Mocks.Models.Collection
{
    public interface INested2
    {
        string Param1 { get; set; }
        INested2 NestedObject { get; set; }
    }

    public class Nested2 : INested2
    {
        public string Param1 { get; set; }
        public INested2 NestedObject { get; set; }
    }

    public class ResourceWithListOfInterfaces
    {
        public List<INested2> Collection { get; set; }
    }

    public class ResourceWithIListOfInterfaces
    {
        public IList<INested2> Collection { get; set; }
    }

    public class ResourceWithArrayOfInterfaces
    {
        public INested2[] Collection { get; set; }
    }
}
