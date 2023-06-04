using System.Collections.Generic;
using NUnit.Framework;

using Json.Abstraction.Tests.Mocks;
using Json.Abstraction.Tests.Mocks.Models.Abstraction;

namespace Json.Abstraction.Tests
{
    public class AbstractionConverterTests : TestBase
    {
        [Test]
        public void AbstractionDeserialize_ListOfStrings()
        {
            var mock = AbstractionMocks.GetListOfStringsMock();

            var result = DeserializeJson<ListOfStrings>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ListOfStrings>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.AreEqual("opt1", result.Collection[0]);
            Assert.AreEqual("opt2", result.Collection[1]);
        }

        [Test]
        public void AbstractionDeserialize_ListOfStringsWithPropertyName()
        {
            var mock = AbstractionMocks.GetListOfStringsWithPropertyNameMock();

            var result = DeserializeJson<ListOfStringsWithPropertyName>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ListOfStringsWithPropertyName>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.AreEqual("opt1", result.Collection[0]);
            Assert.AreEqual("opt2", result.Collection[1]);
        }

        [Test]
        public void AbstractionDeserialize_ArrayOfStrings()
        {
            var mock = AbstractionMocks.GetArrayOfStringsMock();

            var result = DeserializeJson<ArrayOfStrings>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ArrayOfStrings>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Length == 2);
            Assert.AreEqual("opt1", result.Collection[0]);
            Assert.AreEqual("opt2", result.Collection[1]);
        }

        [Test]
        public void AbstractionDeserialize_ListOfObjects()
        {
            var mock = AbstractionMocks.GetListOfObjectsMock();

            var result = DeserializeJson<ListOfObjects>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ListOfObjects>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.IsInstanceOf<List<Nested>>(result.Collection, "Wrong instance");
            Assert.AreEqual(3, result.Collection[0].Param1);
            Assert.AreEqual(4, result.Collection[1].Param1);
        }

        [Test]
        public void AbstractionDeserialize_ArrayOfObjects()
        {
            var mock = AbstractionMocks.GetArrayOfObjectsMock();

            var result = DeserializeJson<ArrayOfObjects>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ArrayOfObjects>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Length == 2);
            Assert.IsInstanceOf<Nested[]>(result.Collection, "Wrong instance");
            Assert.AreEqual(3, result.Collection[0].Param1);
            Assert.AreEqual(4, result.Collection[1].Param1);
        }

        [Test]
        public void AbstractionDeserialize_ListOfAbstractions()
        {
            var mock = AbstractionMocks.GetListOfAbstractionsMock();

            var result = DeserializeJson<ListOfAbstractions>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ListOfAbstractions>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.IsInstanceOf<List<NestedBase>>(result.Collection, "Wrong instance");
            Assert.AreEqual(3, result.Collection[0].Param1);
            Assert.AreEqual(4, result.Collection[1].Param1);
        }

        [Test]
        public void AbstractionDeserialize_ListOfInterfaces()
        {
            var mock = AbstractionMocks.GetListOfInterfacesMock();

            var result = DeserializeJson<ListOfInterfaces>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ListOfInterfaces>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.IsInstanceOf<List<INested>>(result.Collection, "Wrong instance");
            Assert.AreEqual(3, result.Collection[0].Param1);
            Assert.AreEqual(4, result.Collection[1].Param1);
        }

        [Test]
        public void AbstractionDeserialize_IListOfInterfaces()
        {
            var mock = AbstractionMocks.GetIListOfInterfacesMock();

            var result = DeserializeJson<IListOfInterfaces>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IListOfInterfaces>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.IsInstanceOf<IList<INested>>(result.Collection, "Wrong instance");
            Assert.AreEqual(3, result.Collection[0].Param1);
            Assert.AreEqual(4, result.Collection[1].Param1);
        }

        [Test]
        public void AbstractionSerialize_ListOfStrings()
        {
            var mock = AbstractionMocks.GetListOfStringsMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void AbstractionSerialize_ListOfStringsWithPropertyName()
        {
            var mock = AbstractionMocks.GetListOfStringsWithPropertyNameMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void AbstractionSerialize_ArrayOfStrings()
        {
            var mock = AbstractionMocks.GetArrayOfStringsMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void AbstractionSerialize_ListOfObjects()
        {
            var mock = AbstractionMocks.GetListOfObjectsMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void AbstractionSerialize_ArrayOfObjects()
        {
            var mock = AbstractionMocks.GetArrayOfObjectsMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void AbstractionSerialize_ListOfAbstractions()
        {
            var mock = AbstractionMocks.GetListOfAbstractionsMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void AbstractionSerialize_ListOfInterfaces()
        {
            var mock = AbstractionMocks.GetListOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void AbstractionSerialize_IListOfInterfaces()
        {
            var mock = AbstractionMocks.GetIListOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }
    }
}
