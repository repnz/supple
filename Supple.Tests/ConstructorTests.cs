﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using Supple.Deserialization.Exceptions;

namespace Supple.Tests
{
    [TestClass]
    public class ConstructorTests
    {
        private SuppleDeserializerTester _tester;

        [TestInitialize]
        public void Initialize()
        {
            _tester = new SuppleDeserializerTester();
        }

        [TestMethod]
        public void HasOnlyConstructor_CallConstructor()
        {
            string objectXml =
                "<ConstructorObject ParamA=\"ParamA\">" +
                    "<ParamB>ParamB</ParamB>" +
                "</ConstructorObject>";

            ConstructorObject obj = _tester.Deserialize<ConstructorObject>(objectXml);

            Assert.AreEqual("ParamA", obj.ParamA);
            Assert.AreEqual("ParamB", obj.ParamB);
        }

        [TestMethod]
        public void HasTwoConstructors_ChooseCorrect()
        {
            string objectXml =
                "<TwoConstructorTestObject>" +
                    "<ParamA>ParamA</ParamA>" +
                    "<ParamB>ParamB</ParamB>" +
                "</TwoConstructorTestObject>";

            var obj = _tester.Deserialize<TwoConstructorTestObject>(objectXml);

            Assert.IsNotNull(obj);
            Assert.AreEqual(obj.ParamA, "ParamA");
            Assert.AreEqual(obj.ParamB, "ParamB");
        }

        [TestMethod]
        public void NotEnoughParams_CallEmptyCtorAndSetProperties()
        {
            string objectXml =
                "<ThreeConstructorTestObject>" +
                    "<ParamA>ParamA</ParamA>" +
                    "<ParamB>ParamB</ParamB>" +
                "</ThreeConstructorTestObject>";

            var obj = _tester.Deserialize<ThreeConstructorTestObject>(objectXml);

            Assert.IsNotNull(obj);
            Assert.AreEqual(obj.ParamA, "ParamA");
            Assert.AreEqual(obj.ParamB, "ParamB");
        }

        [TestMethod]
        public void MissingMatchingConstructor_ThrowsConstructorNotFoundException()
        {
            string objectXml =
                "<ConstructorObject>" +
                    "<ParamB>ParamB</ParamB>" +
                "</ConstructorObject>";

            try
            {
                _tester.Deserialize<ConstructorObject>(objectXml);
            }
            catch (ConstructorNotFoundException e)
            {
                Assert.AreEqual(typeof(ConstructorObject), e.NodeType);
                return;
            }

            Assert.Fail("Exception was not thrown!");
        }

    }
}
