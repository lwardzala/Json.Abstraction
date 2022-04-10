﻿using System;
using NUnit.Framework;

using Json.Abstraction.Tests.Mocks;
using Json.Abstraction.Tests.Mocks.Models.Mixed;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Json.Abstraction.Tests
{
    public class MixedConvertersTests : TestBase
    {
        [Test]
        public void MixedDeserialize_InterfaceWithNestedAbstract()
        {
            var mock = MixedMocks.GetInterfaceWithNestedAbstractMock();

            var result = DeserializeJson<IModel>(mock.JsonData);

            var json = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Model>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param1);
            Assert.IsInstanceOf<NestedModel>(result.NestedModel, "Wrong instance");
            Assert.AreEqual(3, result.NestedModel.Param1);
            Assert.AreEqual("Test2", ((NestedModel)result.NestedModel).Model.Param1);
            Assert.IsTrue(Convert.FromBase64String("dGVzdA==").SequenceEqual(result.ByteArray));
            Assert.IsTrue(Convert.FromBase64String("dGVzdA==").SequenceEqual(((NestedModel)result.NestedModel).Model.ByteArray));

            Assert.IsNull(result.DoubleToAlwaysIgnore);
            Assert.AreEqual(22.22, result.DoubleToNeverIgnore1);
            Assert.AreEqual(0, result.DoubleToNeverIgnore2);
            Assert.IsNull(result.DoubleToIgnoreWhenWritingDefault1);
            Assert.AreEqual(10, result.DoubleToIgnoreWhenWritingDefault2);
            Assert.IsNull(result.DoubleToIgnoreWhenWritingNull);
            Assert.IsNull(((NestedModel)result.NestedModel).Model.DoubleToAlwaysIgnore);
            Assert.AreEqual(22.22, ((NestedModel)result.NestedModel).Model.DoubleToNeverIgnore1);
            Assert.AreEqual(0, ((NestedModel)result.NestedModel).Model.DoubleToNeverIgnore2);
            Assert.AreEqual(10, ((NestedModel)result.NestedModel).Model.DoubleToIgnoreWhenWritingDefault2);
            Assert.IsNull(((NestedModel)result.NestedModel).Model.DoubleToIgnoreWhenWritingNull);

            Assert.IsFalse(json.Contains("doubleToAlwaysIgnore", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(json.Contains("doubleToNeverIgnore1", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(json.Contains("doubleToNeverIgnore2", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsFalse(json.Contains("doubleToIgnoreWhenWritingDefault1", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(json.Contains("doubleToIgnoreWhenWritingDefault2", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsFalse(json.Contains("doubleToIgnoreWhenWritingNull", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void MixedDeserialize_NestedModelAllBaseTypes()
        {
            var mock = MixedMocks.GetNestedModelAllBaseTypesMock();

            var result = DeserializeJson<NestedModelAllBaseTypes>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dictionary);
            Assert.IsTrue(result.Dictionary.ContainsKey("testKey"));
            Assert.AreEqual("testValue", result.Dictionary["testKey"]);
            Assert.AreEqual(TestEnum.TEST2, result.Enum);
            Assert.AreEqual(TestEnum.TEST2, result.EnumInt);
            Assert.AreEqual(DateTime.Parse("2020-08-12T19:35:00"), result.DateTime);
            Assert.AreEqual(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), result.Guid);
            Assert.AreEqual(true, result.Boolean);
            Assert.AreEqual(10, result.Decimal);
            Assert.AreEqual(2.456, result.Double);
            Assert.AreEqual(45.35665F, result.Float);
            Assert.AreEqual(3, result.Short);
            Assert.AreEqual(-456, result.Int);
            Assert.AreEqual(467585, result.Long);
            Assert.AreEqual(455, result.UShort);
            Assert.AreEqual(UInt32.Parse("45545545"), result.UInt);
            Assert.AreEqual(UInt64.Parse("747474757575"), result.ULong);
            Assert.IsTrue(Convert.FromBase64String("dGVzdA==").SequenceEqual(result.ByteArray));
        }

        [Test]
        public void MixedDeserialize_RawModel()
        {
            var mock = MixedMocks.GetRawModelMock();

            var result = DeserializeJson<RawModel>(mock.JsonData, mock.typeToConvert);

            var json = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dictionary);
            Assert.IsTrue(result.Dictionary.ContainsKey("testKey"));
            Assert.AreEqual("testValue", result.Dictionary["testKey"]);
            Assert.AreEqual(TestEnum.TEST2, result.Enum);
            Assert.AreEqual(DateTime.Parse("2020-08-12T19:35:00"), result.DateTime);
            Assert.AreEqual(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), result.Guid);
            Assert.AreEqual(true, result.Boolean);
            Assert.AreEqual(10, result.Decimal);
            Assert.AreEqual(2.456, result.Double);
            Assert.AreEqual(45.35665F, result.Float);
            Assert.AreEqual(3, result.Short);
            Assert.AreEqual(-456, result.Int);
            Assert.IsNull(result.DoubleToAlwaysIgnore);
            Assert.AreEqual(22.22, result.DoubleToNeverIgnore1);
            Assert.AreEqual(0, result.DoubleToNeverIgnore2);
            Assert.IsNull(result.DoubleToIgnoreWhenWritingDefault1);
            Assert.AreEqual(10, result.DoubleToIgnoreWhenWritingDefault2);
            Assert.IsNull(result.DoubleToIgnoreWhenWritingNull);
            Assert.AreEqual(467585, result.Long);
            Assert.IsTrue(Convert.FromBase64String("dGVzdA==").SequenceEqual(result.ByteArray));

            Assert.IsFalse(json.Contains("doubleToAlwaysIgnore", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(json.Contains("doubleToNeverIgnore1", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(json.Contains("doubleToNeverIgnore2", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsFalse(json.Contains("doubleToIgnoreWhenWritingDefault1", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(json.Contains("doubleToIgnoreWhenWritingDefault2", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsFalse(json.Contains("doubleToIgnoreWhenWritingNull", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void MixedDeserialize_InterfaceWithMultipleClasses()
        {
            var mock = MixedMocks.GetInterfaceWithMultipleClassesMock();

            var result = DeserializeJson<Impl1>(mock.JsonData, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Impl1>(result, "Wrong instance");
            Assert.AreEqual("Test", result.Param1);
            Assert.IsInstanceOf<Impl2>(result.NestedInterface, "Wrong instance");
            Assert.AreEqual("Test2", result.NestedInterface.Param1);
        }

        [Test]
        public void MixedSerialize_InterfaceWithNestedAbstract()
        {
            var mock = MixedMocks.GetInterfaceWithNestedAbstractMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void MixedSerialize_NestedModelAllBaseTypes()
        {
            var mock = MixedMocks.GetNestedModelAllBaseTypesMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData).Replace("\"enumInt\":1", "\"enumInt\":\"TEST2\""), result);
        }

        [Test]
        public void MixedSerialize_RawModel()
        {
            var mock = MixedMocks.GetRawModelMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }

        [Test]
        public void MixedSerialize_InterfaceWithMultipleClasses()
        {
            var mock = MixedMocks.GetInterfaceWithMultipleClassesMock();

            var result = SerializeJson(mock.TestObject, mock.typeToConvert);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetNormalizedJson(mock.JsonData), result);
        }
    }
}
