using NUnit.Framework;

using Json.Abstraction.Tests.Mocks;
using Json.Abstraction.Tests.Mocks.Models.Collection;

namespace Json.Abstraction.Tests
{
    public class ImmutableListTests : TestBase
    {
        // TODO: support for immutable
        /*[Test]
        public void AbstractionDeserialize_ImmutableList()
        {
            var mock = ImmutableListMocks.GetResourceWithImmutableList();

            var result = DeserializeJson<AbstractClassWithImmutableList>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ClassWithImmutableList>(result, "Wrong instance");
            Assert.AreEqual("Test1", result.Name);
            Assert.IsTrue(result.List.Count == 2);
            Assert.AreEqual("Test2", result.List[0]);
            Assert.AreEqual("Test3", result.List[1]);
        }*/

        [Test]
        public void AbstractionSeserialize_ImmutableList()
        {
            var mock = ImmutableListMocks.GetResourceWithImmutableList();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }
    }
}
