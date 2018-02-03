using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using System.Linq;
using System.Collections.Generic;
using Supple.Deserialization.Exceptions;

namespace Supple.Tests
{
    [TestClass]
    public class InterfacesTests
    {
        private SuppleDeserializerTester _tester;

        [TestInitialize]
        public void Initialize()
        {
            _tester = new SuppleDeserializerTester();
        }

        [TestMethod]
        public void Interface_GetCorrectImplementation()
        {
            string objectXml = "<TestInterface Type=\"TestInterfaceImpl1\" OneProperty=\"OneValue\"/>";

            _tester.TestDeserialization<ITestInterface>(objectXml, new TestInterfaceImpl1 { OneProperty = "OneValue" });
        }

        [TestMethod]
        public void InterfaceProperty_GetCorrectImplementation()
        {
            string objectXml =
                "<ObjectWithInterfaceProperty>" +
                "   <InterfaceProperty Type=\"TestInterfaceImpl2\" TwoProperty=\"TwoValue\"/>" +
                "</ObjectWithInterfaceProperty>";

            _tester.TestDeserialization(objectXml, new ObjectWithInterfaceProperty
            {
                InterfaceProperty = new TestInterfaceImpl2() { TwoProperty = "TwoValue" }
            });
        }

        [TestMethod]
        public void GenericInterface_GetCorrectImplementation()
        {
            string objectXml =
                "<EnumerableOfString Type=\"ListOfString\">" +
                    "<String>Hi</String>"+
                "</EnumerableOfString>";

            IEnumerable<string> str = _tester.Deserialize<IEnumerable<string>>(objectXml);

            Assert.IsNotNull(str);
            Assert.AreEqual("Hi", str.First());
        }

        [TestMethod]
        public void Interface_WithoutType_ThrowsException()
        {
            string objectXml =
                "<TestInterface>" +
                "</TestInterface>";
            try
            {
                _tester.Deserialize<ITestInterface>(objectXml);
            }
            catch (InterfaceTypeNotFoundException e)
            {
                Assert.AreEqual("TestInterface", e.Node.Name);
                Assert.AreEqual(typeof(ITestInterface), e.InterfaceType);
                return;
            }

            Assert.Fail("Exception was not thrown");
        }

        [TestMethod]
        public void Interface_WithUnknownType_ThrowsException()
        {
            string objectXml =
                "<TestInterface Type=\"Impl\">" +
                "</TestInterface>";
            try
            {
                _tester.Deserialize<ITestInterface>(objectXml);
            }
            catch (RuntimeTypeException e)
            {
                Assert.AreEqual("Impl", e.TypeName);
                Assert.AreEqual(typeof(ITestInterface), e.InterfaceType);
                return;
            }

            Assert.Fail("Exception was not thrown");
        }
    }
}
