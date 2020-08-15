using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Json.Abstraction.Tests.Mocks.Models.Abstraction;

namespace Json.Abstraction.Tests
{
    [TestClass]
    public class JsonAbstractionConverterTests
    {
        [TestMethod]
        public void CanConvert_BaseTypes_ShouldNotConvert()
        {
            var converter = new JsonAbstractionConverter();

            Assert.IsFalse(converter.CanConvert(typeof(int)));
            Assert.IsFalse(converter.CanConvert(typeof(string)));
            Assert.IsFalse(converter.CanConvert(typeof(Dictionary<string, string>)));
            Assert.IsFalse(converter.CanConvert(typeof(IList<string>)));
        }

        [TestMethod]
        public void CanConvert_ConvertibleTypes_ShouldNotConvert()
        {
            var converter = new JsonAbstractionConverter();

            Assert.IsFalse(converter.CanConvert(typeof(Nested)));
            Assert.IsFalse(converter.CanConvert(typeof(List<INested>)));
        }

        [TestMethod]
        public void CanConvert_InterfaceType_ShouldConvert()
        {
            var converter = new JsonAbstractionConverter();

            Assert.IsTrue(converter.CanConvert(typeof(INested)));
            Assert.IsTrue(converter.CanConvert(typeof(NestedBase)));
        }
    }
}
