using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Collections;
using System;
using System.IO;
using System.Text;

namespace Supple.Tests
{
    class SuppleDeserializerTester
    {
        private readonly ISuppleDeserializer _deserializer;

        public SuppleDeserializerTester(ISuppleDeserializer deserializer)
        {
            _deserializer = deserializer;
        }

        private MemoryStream CreateMemoryStream(string xml)
        {
            return new MemoryStream(Encoding.Default.GetBytes(xml));
        }

        public T Deserialize<T>(string xml)
        {
            using (MemoryStream stream = CreateMemoryStream(xml))
            {
                return _deserializer.Deserialize<T>(stream);
            }
        }

        public void TestDeserialization<T>(string xml, T expected)
        {
            T deserializedObject = Deserialize<T>(xml);

            Assert.AreEqual(expected, deserializedObject);
        }

        public void TestDeserializationList<T>(string xml, T expected) where T : System.Collections.IList
        {
            MemoryStream stream = CreateMemoryStream(xml);

            T deserializedObject = _deserializer.Deserialize<T>(stream);

            Assert.IsTrue(EnumerableExtentions.IsItemsEqual(expected, deserializedObject));
        }
    }
}
