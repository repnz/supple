using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Collections;
using Supple.Tests.TestObjects;
using Supple.Xml;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Supple.Tests
{
    class SuppleDeserializerTester
    {
        private readonly SuppleXmlDeserializer _deserializer;

        public SuppleDeserializerTester()
        {
            StaticTypeResolver typeResolver = new StaticTypeResolver();
            typeResolver.AddType<TestInterfaceImpl1>();
            typeResolver.AddType<TestInterfaceImpl2>();
            typeResolver.AddType<List<string>>();
            _deserializer = new SuppleXmlDeserializer(typeResolver);
        }

        public T Deserialize<T>(string xml) { return _deserializer.Deserialize<T>(xml); }

        public void TestDeserialization<T>(string xml, T expected)
        {
            T deserializedObject = _deserializer.Deserialize<T>(xml);

            Assert.AreEqual(expected, deserializedObject);
        }

        public void TestDeserializationList<T>(string xml, T expected) where T : System.Collections.IList
        {
            T obj = _deserializer.Deserialize<T>(xml);
            Assert.IsTrue(EnumerableExtentions.IsItemsEqual(expected, obj));
        }
    }
}
