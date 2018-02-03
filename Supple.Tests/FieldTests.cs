using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using Supple.Deserialization;

namespace Supple.Tests
{
    [TestClass]
    public class FieldTests
    {
        private SuppleXmlDeserializer _deserializer;

        [TestInitialize]
        public void InitializeTest()
        {
            _deserializer = new SuppleXmlDeserializer();
        }

        [TestMethod]
        public void ObjectWithFields_Deserialize()
        {
            string objectXml = "<FieldObject Field1=\"Hello\" Field2=\"World\"/>";

            FieldObject f = _deserializer.Deserialize<FieldObject>(objectXml);

            Assert.IsNotNull(f);
            Assert.AreEqual(f.Field1, "Hello");
            Assert.AreEqual(f.Field2, "World");
        }
    }
}
