﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void ReferenceList_GetReference()
        {
            string objectXml =
                "<ObjectWithReferenceList>" +
                    "<NamedElements>" +
                        "<NamedElement Name=\"A\" Value=\"ValueA\"/>" +
                        "<NamedElement Name=\"B\" Value=\"ValueB\"/>" +
                    "</NamedElements>" +
                    "<ReferenceList>[$A, $B]</ReferenceList>" +
                "</ObjectWithReferenceList>";

            var deserialized = _tester.Deserialize<ObjectWithReferenceList>(objectXml);
            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(deserialized.NamedElements);
            Assert.IsNotNull(deserialized.ReferenceList);
            Assert.IsTrue(deserialized.NamedElements.Count == 2);
            Assert.IsTrue(deserialized.NamedElements[0] == deserialized.ReferenceList[0]);
            Assert.IsTrue(deserialized.NamedElements[1] == deserialized.ReferenceList[1]);
        }
    }
}
