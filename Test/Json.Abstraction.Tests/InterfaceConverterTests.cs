using System.Collections.Generic;
using NUnit.Framework;

using Json.Abstraction.Tests.Mocks;
using Json.Abstraction.Tests.Mocks.Models.Objects;
using Json.Abstraction.Tests.Mocks.Models.Collection;

namespace Json.Abstraction.Tests
{
    public class InterfaceConverterTests : TestBase
    {
        [Test]
        public void InterfaceDeserialize_ResourceWithObject()
        {
            var mock = ObjectMocks.GetResourceWithObjectMock();

            var result = DeserializeJson<ResourceWithObject>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResourceWithObject>(result, "Wrong instance");
            Assert.AreEqual(3, result.Param1);
            Assert.IsInstanceOf<Nested>(result.NestedObject, "Wrong instance");
            Assert.AreEqual("Test", result.NestedObject.Param2);
        }

        [Test]
        public void InterfaceDeserialize_ObjectWithNestedObject()
        {
            var mock = ObjectMocks.GetObjectWithNestedObjectMock();

            var result = DeserializeJson<INested>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Nested>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param2);
            Assert.IsInstanceOf<Nested>(result.NestedObject, "Wrong instance");
            Assert.AreEqual("Test2", result.NestedObject.Param2);
        }

        [Test]
        public void InterfaceDeserialize_ResourceWithListOfInterfaces()
        {
            var mock = CollectionMocks.GetResourceWithListOfInterfacesMock();

            var result = DeserializeJson<ResourceWithListOfInterfaces>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResourceWithListOfInterfaces>(result, "Wrong instance");
            Assert.IsNotNull(result.Collection);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.AreEqual("Test", result.Collection[0].Param1);
            Assert.AreEqual("Test2", result.Collection[1].Param1);
        }

        [Test]
        public void InterfaceDeserialize_ResourceWithIListOfInterfaces()
        {
            var mock = CollectionMocks.GetResourceWithIListOfInterfacesMock();

            var result = DeserializeJson<ResourceWithIListOfInterfaces>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResourceWithIListOfInterfaces>(result, "Wrong instance");
            Assert.IsNotNull(result.Collection);
            Assert.IsTrue(result.Collection.Count == 2);
            Assert.AreEqual("Test", result.Collection[0].Param1);
            Assert.AreEqual("Test2", result.Collection[1].Param1);
        }

        [Test]
        public void InterfaceDeserialize_ResourceWithArrayOfInterfaces()
        {
            var mock = CollectionMocks.GetResourceWithArrayOfInterfacesMock();

            var result = DeserializeJson<ResourceWithArrayOfInterfaces>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResourceWithArrayOfInterfaces>(result, "Wrong instance");
            Assert.IsNotNull(result.Collection);
            Assert.IsTrue(result.Collection.Length == 2);
            Assert.AreEqual("Test", result.Collection[0].Param1);
            Assert.AreEqual("Test2", result.Collection[1].Param1);
        }

        [Test]
        public void InterfaceDeserialize_ListOfInterfaces()
        {
            var mock = CollectionMocks.GetListOfInterfacesMock();

            var result = DeserializeJson<List<INested2>>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual("Test", result[0].Param1);
            Assert.AreEqual("Test2", result[1].Param1);
        }

        [Test]
        public void InterfaceDeserialize_IListOfInterfaces()
        {
            var mock = CollectionMocks.GetIListOfInterfacesMock();

            var result = DeserializeJson<IList<INested2>>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual("Test", result[0].Param1);
            Assert.AreEqual("Test2", result[1].Param1);
        }

        [Test]
        public void InterfaceDeserialize_ArrayOfInterfaces()
        {
            var mock = CollectionMocks.GetArrayOfInterfacesMock();

            var result = DeserializeJson<INested2[]>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == 2);
            Assert.AreEqual("Test", result[0].Param1);
            Assert.AreEqual("Test2", result[1].Param1);
        }

        [Test]
        public void InterfaceDeserialize_IEnumerableOfInterfaces()
        {
            var mock = CollectionMocks.GetIEnumerableOfInterfacesMock();

            var result = DeserializeJson<List<INested2>>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual("Test", result[0].Param1);
            Assert.AreEqual("Test2", result[1].Param1);
        }

        [Test]
        public void InterfaceSerialize_ResourceWithObject()
        {
            var mock = ObjectMocks.GetResourceWithObjectMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void InterfaceSerialize_ObjectWithNestedObject()
        {
            var mock = ObjectMocks.GetObjectWithNestedObjectMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void InterfaceSerialize_ResourceWithListOfInterfaces()
        {
            var mock = CollectionMocks.GetResourceWithListOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void InterfaceSerialize_ResourceWithIListOfInterfaces()
        {
            var mock = CollectionMocks.GetResourceWithIListOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void InterfaceSerialize_ResourceWithArrayOfInterfaces()
        {
            var mock = CollectionMocks.GetResourceWithArrayOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void InterfaceSerialize_ListOfInterfaces()
        {
            var mock = CollectionMocks.GetListOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void InterfaceSerialize_IListOfInterfaces()
        {
            var mock = CollectionMocks.GetIListOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void InterfaceSerialize_ArrayOfInterfaces()
        {
            var mock = CollectionMocks.GetArrayOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void InterfaceSerialize_IEnumerableOfInterfaces()
        {
            var mock = CollectionMocks.GetIEnumerableOfInterfacesMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }
    }
}
