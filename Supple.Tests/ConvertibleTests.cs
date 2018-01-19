using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using Supple.Xml;
using Supple.Xml.Exceptions;
using System;

namespace Supple.Tests
{
    [TestClass]
    public class ConvertibleTests
    {
        private SuppleDeserializerTester _tester;

        [TestInitialize]
        public void Initialize()
        {
            _tester = new SuppleDeserializerTester();
        }

        [TestMethod]
        public void UseDate_Deserialize()
        {
            string objectXml = "<DateTime>1997/07/31 10:00:00</DateTime>";

            DateTime date = _tester.Deserialize<DateTime>(objectXml);

            Assert.AreEqual(new DateTime(1997, 7, 31, 10, 0, 0), date);
        }

        [TestMethod]
        public void ConvertibleMembers_ConvertMembers()
        {
            string objectXml =
                "<ConvertibleMembersTestObject>" +
                "<IntMember>100</IntMember>" +
                "<UIntMember>101</UIntMember>" +
                "<FloatMember>102.678</FloatMember>" +
                "<DoubleMember>103.456</DoubleMember>" +
                "<ByteMember>104</ByteMember>" +
                "<StringMember>MyAwsomeString</StringMember>"+
                "</ConvertibleMembersTestObject>";

            var obj = _tester.Deserialize<ConvertibleMembersTestObject>(objectXml);

            Assert.AreEqual(100, obj.IntMember);
            Assert.AreEqual<UInt32>(101, obj.UIntMember);
            Assert.AreEqual(102.678f, obj.FloatMember);
            Assert.AreEqual(103.456, obj.DoubleMember);
            Assert.AreEqual<byte>(104, obj.ByteMember);
            Assert.AreEqual("MyAwsomeString", obj.StringMember);
        }

        [TestMethod]
        public void ConvertibleError_ThrowsConvertibleFormatException()
        {
            string objectXml =
                "<ConvertibleMembersTestObject>" +
                    "<IntMember>ori</IntMember>" +
                "</ConvertibleMembersTestObject>";

            try
            {
                var obj = _tester.Deserialize<ConvertibleMembersTestObject>(objectXml);
            }
            catch (ConvertibleFormatException e)
            {
                Assert.AreEqual(typeof(Int32), e.ExpectedType);
                Assert.AreEqual("ori", e.Value);
                Assert.AreEqual("IntMember", e.Name);
                return;
            }

            Assert.Fail("Exception was not thrown!");
            
        }

    }
}
