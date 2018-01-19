using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using Supple.Xml;
using System.Linq;
using System.Collections.Generic;

namespace Supple.Tests
{
    [TestClass]
    public class InterfacesTests
    {
        private SuppleDeserializerTester _tester;

        [TestInitialize]
        public void Initialize()
        {
            StaticTypeResolver typeResolver = new StaticTypeResolver();
            typeResolver.AddType<TestInterfaceImpl1>();
            typeResolver.AddType<TestInterfaceImpl2>();
            typeResolver.AddType<List<string>>();

            _tester = new SuppleDeserializerTester(new SuppleXmlDeserializer(typeResolver));
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
    }
}
