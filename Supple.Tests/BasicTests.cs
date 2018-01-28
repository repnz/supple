using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using Supple.Xml;
using Supple.Xml.Exceptions;

namespace Supple.Tests
{
    [TestClass]
    public class BasicTests
    {
        private SuppleDeserializerTester _tester;

        [TestInitialize]
        public void Initialize()
        {
            _tester = new SuppleDeserializerTester();
        }

        [TestMethod]
        public void Attributes_SetProperties()
        {
            string objectXml = "<StringPropertiesTestObject Name=\"MyName\" Value=\"MyValue\"/>";

            var obj = _tester.Deserialize<StringPropertiesTestObject>(objectXml);

            Assert.AreEqual("MyName", obj.Name);
            Assert.AreEqual("MyValue", obj.Value);
        }

        [TestMethod]
        public void SubElements_SetProperties()
        {
            string objectXml =
                "<StringPropertiesTestObject>" +
                    "<Name>MyName</Name>" +
                    "<Value>MyValue</Value>" +
                "</StringPropertiesTestObject>";

            var obj = _tester.Deserialize<StringPropertiesTestObject>(objectXml);

            Assert.AreEqual("MyName", obj.Name);
            Assert.AreEqual("MyValue", obj.Value);
        }

        private void TestSubPropertiesTestObject(string objectXml)
        {
            var obj = _tester.Deserialize<SubPropertiesTestObject>(objectXml);

            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Sub);
            Assert.AreEqual("AValue", obj.Sub.SubPropertyA);
            Assert.AreEqual("BValue", obj.Sub.SubPropertyB);
        }

        [TestMethod]
        public void SubElementsWithSubElements_SetSubObject()
        {
            string objectXml =
                "<SubPropertiesTestObject>" +
                    "<Sub>" +
                        "<SubPropertyA>AValue</SubPropertyA>" +
                        "<SubPropertyB>BValue</SubPropertyB>" +
                    "</Sub>" +
                "</SubPropertiesTestObject>";

            TestSubPropertiesTestObject(objectXml);  
        }

        [TestMethod]
        public void SubElementsWithSubAttributes_SetSubObject()
        {
            string objectXml =
                "<SubPropertiesTestObject>" +
                    "<Sub SubPropertyA=\"AValue\" SubPropertyB=\"BValue\"/>" +
                "</SubPropertiesTestObject>";

            TestSubPropertiesTestObject(objectXml);
        }

        [TestMethod]
        public void PropertyNotFound_ThrowsException()
        {
            string objectXml =
              "<StringPropertiesTestObject>" +
                  "<Name>MyName</Name>" +
                  "<Value>MyValue</Value>" +
                  "<SomeProp>SOMEVAL</SomeProp>"+
              "</StringPropertiesTestObject>";

            try
            {
                var obj = _tester.Deserialize<StringPropertiesTestObject>(objectXml);
            }
            catch (MemberNotFoundException e)
            {
                Assert.AreEqual("SomeProp", e.MemberName);
                Assert.AreEqual(typeof(StringPropertiesTestObject), e.BaseType);
                return;
            }

            Assert.Fail("Exception was not thrown");
        }

        [TestMethod]
        public void PropertyWithoutPublicSetter_ThrowsException()
        {
            string objectXml =
              "<PrivatePropertyObject>" +
                  "<SomeProperty>Something</SomeProperty>" +
              "</PrivatePropertyObject>";

            try
            {
                var obj = _tester.Deserialize<PrivatePropertyObject>(objectXml);
            }
            catch (MemberNotFoundException e)
            {
                Assert.AreEqual("SomeProperty", e.MemberName);
                Assert.AreEqual(typeof(PrivatePropertyObject), e.BaseType);
                return;
            }

            Assert.Fail("Exception was not thrown");
        }
    }
}
