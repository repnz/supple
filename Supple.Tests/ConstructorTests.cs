using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using Supple.Xml;

namespace Supple.Tests
{
    [TestClass]
    public class ConstructorTests
    {
        private SuppleDeserializerTester _tester;

        [TestInitialize]
        public void Initialize()
        {
            StaticTypeResolver typeResolver = new StaticTypeResolver();
            typeResolver.AddType<TestInterfaceImpl1>();
            typeResolver.AddType<TestInterfaceImpl2>();
            _tester = new SuppleDeserializerTester(new SuppleXmlDeserializer(typeResolver));
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
    }
}
