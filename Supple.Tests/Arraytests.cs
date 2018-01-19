using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using Supple.Xml;

namespace Supple.Tests
{
    [TestClass]
    public class ArrayTests
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
        public void ArrayAsValue_Deserialize()
        {
            string objectXml = "<ArrayOfInt32>[1,2]</ArrayOfInt32>";

            var obj = _tester.Deserialize<int[]>(objectXml);

            Assert.IsNotNull(obj);
            Assert.AreEqual(1, obj[0]);
            Assert.AreEqual(2, obj[1]);
        }

        [TestMethod]
        public void ArrayAsElements_Deserialize()
        {
            string objectXml = "<ArrayOfInt32><Int32>10</Int32><Int32>20</Int32></ArrayOfInt32>";

            var obj = _tester.Deserialize<int[]>(objectXml);

            Assert.IsNotNull(obj);
            Assert.AreEqual(10, obj[0]);
            Assert.AreEqual(20, obj[1]);
        }

        [TestMethod]
        public void ArrayOfInterfaces_Deserialize()
        {
            string objectXml = 
                "<ArrayOfTestInterface>" +
                "   <TestInterface Type=\"TestInterfaceImpl1\" OneProperty=\"1\"/>" +
                "   <TestInterface Type=\"TestInterfaceImpl2\" TwoProperty=\"2\"/>" +
                "   <TestInterface Type=\"TestInterfaceImpl1\" OneProperty=\"3\"/>" +
                "</ArrayOfTestInterface>";

            var objects = _tester.Deserialize<ITestInterface[]>(objectXml);

            Assert.IsNotNull(objects);
        }
    }
}
