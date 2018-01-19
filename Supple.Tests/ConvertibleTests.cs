﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using Supple.Xml;
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
            StaticTypeResolver typeResolver = new StaticTypeResolver();
            typeResolver.AddType<TestInterfaceImpl1>();
            typeResolver.AddType<TestInterfaceImpl2>();
            _tester = new SuppleDeserializerTester(new SuppleXmlDeserializer(typeResolver));
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

    }
}