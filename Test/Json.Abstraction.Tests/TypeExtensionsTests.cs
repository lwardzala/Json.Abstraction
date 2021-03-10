using System.Collections.Generic;
using NUnit.Framework;

using Json.Abstraction.Extensions;

namespace Json.Abstraction.Tests
{
    public class TypeExtensionsTests
    {
        [Test]
        public void IsDictionary()
        {
            Assert.IsTrue(typeof(IDictionary<object, object>).IsDictionary());
            Assert.IsTrue(typeof(Dictionary<object, object>).IsDictionary());
        }

        [Test]
        public void IsGenericEnumerable()
        {
            Assert.IsTrue(typeof(IEnumerable<object>).IsGenericEnumerable());
            Assert.IsTrue(typeof(IList<object>).IsGenericEnumerable());
            Assert.IsTrue(typeof(ICollection<object>).IsGenericEnumerable());
            Assert.IsTrue(typeof(List<object>).IsGenericEnumerable());
        }
    }
}
