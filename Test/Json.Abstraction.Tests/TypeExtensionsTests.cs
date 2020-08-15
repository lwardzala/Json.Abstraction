using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Json.Abstraction.Extensions;

namespace Json.Abstraction.Tests
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void IsDictionary()
        {
            Assert.IsTrue(typeof(IDictionary<object, object>).IsDictionary());
            Assert.IsTrue(typeof(Dictionary<object, object>).IsDictionary());
        }

        [TestMethod]
        public void IsGenericEnumerable()
        {
            Assert.IsTrue(typeof(IEnumerable<object>).IsGenericEnumerable());
            Assert.IsTrue(typeof(IList<object>).IsGenericEnumerable());
            Assert.IsTrue(typeof(ICollection<object>).IsGenericEnumerable());
            Assert.IsTrue(typeof(List<object>).IsGenericEnumerable());
        }
    }
}
