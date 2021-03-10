using System.Collections.Immutable;

namespace Json.Abstraction.Tests.Mocks.Models.Collection
{
    public class ClassWithImmutableList : AbstractClassWithImmutableList
    {
    }

    public abstract class AbstractClassWithImmutableList
    {
        public string Name { get; set; }
        public ImmutableList<ClassWithImmutableList> List { get; set; }
    }
}
