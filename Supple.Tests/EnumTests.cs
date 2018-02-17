using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Deserialization.Exceptions;
using Supple.Tests.TestObjects;

namespace Supple.Tests
{
    [TestClass]
    public class EnumTests
    {
        [TestMethod]
        public void EnumMember_Parse()
        {
            string objXml =
                "<EnumMemberObject>" +
                    "<EnumMember>Two</EnumMember>" +
                "</EnumMemberObject>";

            SuppleDeserializerTester tester = new SuppleDeserializerTester();
            var obj = tester.Deserialize<EnumMemberObject>(objXml);

            Assert.IsNotNull(obj);
            Assert.AreEqual(SomeEnum.Two, obj.EnumMember);
        }

        [TestMethod]
        public void EnumMember_Flags_Parse()
        {
            string objXml =
                "<EnumMemberObject>" +
                    "<EnumMember>One, Two</EnumMember>" +
                "</EnumMemberObject>";

            SuppleDeserializerTester tester = new SuppleDeserializerTester();
            var obj = tester.Deserialize<EnumMemberObject>(objXml);

            Assert.IsNotNull(obj);
            Assert.AreEqual(SomeEnum.Two | SomeEnum.One, obj.EnumMember);
        }

        [TestMethod]
        public void EnumMember_InvalidMember_ThrowsException()
        {
            string objXml =
                "<EnumMemberObject>" +
                    "<EnumMember>BLAH_BLAH</EnumMember>" +
                "</EnumMemberObject>";

            SuppleDeserializerTester tester = new SuppleDeserializerTester();

            try
            {
                tester.Deserialize<EnumMemberObject>(objXml);
            }
            catch (EnumConvertException e)
            {
                Assert.AreEqual(typeof(SomeEnum), e.EnumType);
                Assert.AreEqual("EnumMember", e.Node.Name);
                Assert.AreEqual("BLAH_BLAH", e.Node.Value);
                return;
            }

            Assert.Fail("Exception was not thrown!");
        }
    }
}
