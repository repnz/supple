using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Xml;
using System;

namespace Supple.Tests
{
    [TestClass]
    public class DateTests
    {
        private SuppleXmlDeserializer _deserializer;

        [TestInitialize]
        public void Initialize()
        {
            _deserializer = new SuppleXmlDeserializer();
        }

        [TestMethod]
        public void UseDate_Deserialize()
        {
            string objectXml = "<DateTime>1997/07/31 10:00:00</DateTime>";

            DateTime date = _deserializer.Deserialize<DateTime>(objectXml);

            Assert.AreEqual(new DateTime(1997, 7, 31, 10, 0, 0), date);
        }
    }
}
