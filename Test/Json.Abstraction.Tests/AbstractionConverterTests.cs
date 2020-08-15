using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Json.Abstraction.Tests.Mocks;
using Json.Abstraction.Tests.Mocks.Models.Abstraction;

namespace Json.Abstraction.Tests
{
    [TestClass]
    public class AbstractionConverterTests : TestBase
    {
        [TestMethod]
        public void AbstractionDeserialize_ListOfStrings()
        {
            var mock = AbstractionMocks.GetListOfStringsMock();

            var result = DeserializeJson<ListOfStrings>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ListOfStrings));
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.AreEqual("opt1", result.Collection[0]);
            Assert.AreEqual("opt2", result.Collection[1]);
        }

        [TestMethod]
        public void AbstractionDeserialize_ArrayOfStrings()
        {
            var mock = AbstractionMocks.GetArrayOfStringsMock();

            var result = DeserializeJson<ArrayOfStrings>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArrayOfStrings));
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Length == 2);
            Assert.AreEqual("opt1", result.Collection[0]);
            Assert.AreEqual("opt2", result.Collection[1]);
        }

        [TestMethod]
        public void AbstractionDeserialize_ListOfObjects()
        {
            var mock = AbstractionMocks.GetListOfObjectsMock();

            var result = DeserializeJson<ListOfObjects>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ListOfObjects));
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.IsInstanceOfType(result.Collection, typeof(List<Nested>));
            Assert.AreEqual(3, result.Collection[0].Param1);
            Assert.AreEqual(4, result.Collection[1].Param1);
        }

        [TestMethod]
        public void AbstractionDeserialize_ArrayOfObjects()
        {
            var mock = AbstractionMocks.GetArrayOfObjectsMock();

            var result = DeserializeJson<ArrayOfObjects>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArrayOfObjects));
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Length == 2);
            Assert.IsInstanceOfType(result.Collection, typeof(Nested[]));
            Assert.AreEqual(3, result.Collection[0].Param1);
            Assert.AreEqual(4, result.Collection[1].Param1);
        }

        [TestMethod]
        public void AbstractionDeserialize_ListOfAbstractions()
        {
            var mock = AbstractionMocks.GetListOfAbstractionsMock();

            var result = DeserializeJson<ListOfAbstractions>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ListOfAbstractions));
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.IsInstanceOfType(result.Collection, typeof(List<NestedBase>));
            Assert.AreEqual(3, result.Collection[0].Param1);
            Assert.AreEqual(4, result.Collection[1].Param1);
        }

        [TestMethod]
        public void AbstractionDeserialize_ListOfInterfaces()
        {
            var mock = AbstractionMocks.GetListOfInterfacesMock();

            var result = DeserializeJson<ListOfInterfaces>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ListOfInterfaces));
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.IsInstanceOfType(result.Collection, typeof(List<INested>));
            Assert.AreEqual(3, result.Collection[0].Param1);
            Assert.AreEqual(4, result.Collection[1].Param1);
        }

        [TestMethod]
        public void AbstractionDeserialize_IListOfInterfaces()
        {
            var mock = AbstractionMocks.GetIListOfInterfacesMock();

            var result = DeserializeJson<IListOfInterfaces>(mock.JsonData, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IListOfInterfaces));
            Assert.AreEqual("Test", result.Param1);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.IsInstanceOfType(result.Collection, typeof(IList<INested>));
            Assert.AreEqual(3, result.Collection[0].Param1);
            Assert.AreEqual(4, result.Collection[1].Param1);
        }

        [TestMethod]
        public void AbstractionSerialize_ListOfStrings()
        {
            var mock = AbstractionMocks.GetListOfStringsMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [TestMethod]
        public void AbstractionSerialize_ArrayOfStrings()
        {
            var mock = AbstractionMocks.GetArrayOfStringsMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [TestMethod]
        public void AbstractionSerialize_ListOfObjects()
        {
            var mock = AbstractionMocks.GetListOfObjectsMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [TestMethod]
        public void AbstractionSerialize_ArrayOfObjects()
        {
            var mock = AbstractionMocks.GetArrayOfObjectsMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [TestMethod]
        public void AbstractionSerialize_ListOfAbstractions()
        {
            var mock = AbstractionMocks.GetListOfAbstractionsMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [TestMethod]
        public void AbstractionSerialize_ListOfInterfaces()
        {
            var mock = AbstractionMocks.GetListOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [TestMethod]
        public void AbstractionSerialize_IListOfInterfaces()
        {
            var mock = AbstractionMocks.GetIListOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.AbstractType);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }
    }
}
