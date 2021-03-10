using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using Json.Abstraction.Tests.Mocks.Models.Collection;

namespace Json.Abstraction.Tests.Mocks
{
    public class ImmutableListMocks
    {
        public static (string JsonData, object TestObject, Type AbstractType) GetResourceWithImmutableList()
        {
            return (
                @"{
                    ""_t"": ""ClassWithImmutableList"",
                    ""name"": ""Test1"",
                    ""list"": [
                        {
                            ""name"": ""Test2""
                        },
                        {
                            ""name"": ""Test3""
                        }
                    ]
                }",
                new ClassWithImmutableList
                {
                    Name = "Test1",
                    List = new List<ClassWithImmutableList>
                    {
                        new ClassWithImmutableList { Name = "Test2" },
                        new ClassWithImmutableList { Name = "Test3" }
                    }.ToImmutableList()
                },
                typeof(AbstractClassWithImmutableList));
        }
    }
}
