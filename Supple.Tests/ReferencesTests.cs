using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.References;
using Supple.Xml;

namespace Supple.Tests
{
    [TestClass]
    public class ReferencesTests
    {
        private SuppleDeserializerTester _tester;

        [TestInitialize]
        public void Initialize()
        {
            StaticTypeResolver typeResolver = new StaticTypeResolver();
            _tester = new SuppleDeserializerTester(new SuppleXmlDeserializer(typeResolver));
        }

        [TestMethod]
        public void ReferenceValue_GetReference()
        {
            string objectXml =
                "<ObjectWithReferenceContainer>" +
                    "<NamedElements>" +
                        "<NamedElement Name=\"NameA\" Value=\"ValueA\"/>" +
                        "<NamedElement Name=\"NameB\" Value=\"ValueB\"/>" +
                    "</NamedElements>" +
                    "<Holder A=\"$NameA\" B=\"$NameB\"/>" +
                "</ObjectWithReferenceContainer>";

            var deserialized = _tester.Deserialize<ObjectWithReferenceContainer>(objectXml);
            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(deserialized.NamedElements);
            Assert.IsTrue(deserialized.NamedElements.Count == 2);
            Assert.IsTrue(deserialized.NamedElements[0] == deserialized.Holder.A);
            Assert.IsTrue(deserialized.NamedElements[1] == deserialized.Holder.B);

        }
    }
}
