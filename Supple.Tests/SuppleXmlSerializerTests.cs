using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Collections;
using Supple.Tests.References;
using Supple.Tests.TestObjects;
using Supple.Xml;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Supple.Tests
{
    [TestClass]
    public class SuppleXmlSerializerTests
    {
        private ISuppleDeserializer deserializer;

        [TestInitialize]
        public void Initialize()
        {
            IRuntimeTypeResolver typeResolver = new StaticTypeResolver();
            typeResolver.AddType<TestInterfaceImpl1>();
            typeResolver.AddType<TestInterfaceImpl2>();
            deserializer = new SuppleXmlDeserializer(typeResolver);
        }

        private MemoryStream CreateMemoryStream(string xml)
        {
            return new MemoryStream(Encoding.Default.GetBytes(xml));
        }

        private T Deserialize<T>(string xml)
        {
            using (MemoryStream stream = CreateMemoryStream(xml))
            {
                return deserializer.Deserialize<T>(stream);
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

            T deserializedObject = deserializer.Deserialize<T>(stream);

            Assert.IsTrue(EnumerableExtentions.IsItemsEqual(expected, deserializedObject));
        }

        [TestMethod]
        public void Deserialize_Attributes_SetProperties()
        {
            // Initialize 
            string objectXml = "<StringPropertiesTestObject Name=\"MyName\" Value=\"MyValue\"/>";

            TestDeserialization(objectXml, new StringPropertiesTestObject { Name = "MyName", Value = "MyValue" });
        }

        [TestMethod]
        public void Deserialize_SubElements_SetProperties()
        {
            string objectXml =
                "<StringPropertiesTestObject>" +
                "<Name>MyName</Name>" +
                "<Value>MyValue</Value>" +
                "</StringPropertiesTestObject>";

            TestDeserialization(objectXml, new StringPropertiesTestObject { Name = "MyName", Value = "MyValue" });
        }

        [TestMethod]
        public void Deserialize_IntMembers_ConvertToInt()
        {
            // Initialize
            string objectXml =
                "<IntMembersTestObject>" +
                "<IntMember>100</IntMember>" +
                "<UIntMember>101</UIntMember>" +
                "<FloatMember>102.678</FloatMember>" +
                "<DoubleMember>103.456</DoubleMember>" +
                "<ByteMember>104</ByteMember>" +
                "</IntMembersTestObject>";

            IntMembersTestObject expectedObject = new IntMembersTestObject
            {
                IntMember = 100,
                UIntMember = 101,
                FloatMember = 102.678f,
                DoubleMember = 103.456,
                ByteMember = 104
            };

            TestDeserialization(objectXml, expectedObject);
        }

        [TestMethod]
        public void Deserialize_SubElementsWithSubElements_SetSubObject()
        {
            string objectXml =
                "<SubPropertiesTestObject>" +
                "<Sub>" +
                "<SubPropertyA>AValue</SubPropertyA>" +
                "<SubPropertyB>BValue</SubPropertyB>" +
                "</Sub>" +
                "</SubPropertiesTestObject>";

            var expected = new SubPropertiesTestObject
            {
                Sub = new SubProperties() { SubPropertyA = "AValue", SubPropertyB = "BValue" }
            };

            TestDeserialization(objectXml, expected);

        }

        [TestMethod]
        public void Deserialize_SubElementsWithSubAttributes_SetSubObject()
        {
            string objectXml =
                "<SubPropertiesTestObject>" +
                "<Sub SubPropertyA=\"AValue\" SubPropertyB=\"BValue\"/>" +
                "</SubPropertiesTestObject>";

            var expected = new SubPropertiesTestObject
            {
                Sub = new SubProperties() { SubPropertyA = "AValue", SubPropertyB = "BValue" }
            };

            TestDeserialization(objectXml, expected);
        }

        [TestMethod]
        public void Deserialize_IList_SetSubItems()
        {
            string objectXml =
                "<ListTestObject>" +
                "<SubProperties SubPropertyA=\"1AValue\" SubPropertyB=\"1BValue\"/>" +
                "<SubProperties SubPropertyA=\"2AValue\" SubPropertyB=\"2BValue\"/>" +
                "</ListTestObject>";

            var list = new ListTestObject()
            {
                new SubProperties { SubPropertyA = "1AValue", SubPropertyB = "1BValue"},
                new SubProperties { SubPropertyA = "2AValue", SubPropertyB = "2BValue"},
            };

            TestDeserializationList(objectXml, list);
        }

        [TestMethod]
        public void Deserialize_List_SetSubItems()
        {
            string objectXml =
                "<ListOfSubProperties>" +
                "<SubProperties SubPropertyA=\"1AValue\" SubPropertyB=\"1BValue\"/>" +
                "<SubProperties SubPropertyA=\"2AValue\" SubPropertyB=\"2BValue\"/>" +
                "</ListOfSubProperties>";

            var list = new List<SubProperties>()
            {
                new SubProperties { SubPropertyA = "1AValue", SubPropertyB = "1BValue"},
                new SubProperties { SubPropertyA = "2AValue", SubPropertyB = "2BValue"},
            };

            var stream = CreateMemoryStream(objectXml);
            var deserialized = deserializer.Deserialize<List<SubProperties>>(stream);

            Assert.IsTrue(EnumerableExtentions.IsItemsEqual(deserialized, list), "Items Are Not Equal!");
        }

        [TestMethod]
        public void Deserialize_Interface_GetCorrectImplementation()
        {
            string objectXml = "<TestInterface Type=\"TestInterfaceImpl1\" OneProperty=\"OneValue\"/>";

            TestDeserialization<ITestInterface>(objectXml, new TestInterfaceImpl1 { OneProperty = "OneValue" });
        }

        [TestMethod]
        public void Deserialize_InterfaceProperty_GetCorrectImplementation()
        {
            string objectXml =
                "<ObjectWithInterfaceProperty>" +
                "   <InterfaceProperty Type=\"TestInterfaceImpl2\" TwoProperty=\"TwoValue\"/>" +
                "</ObjectWithInterfaceProperty>";

            TestDeserialization(objectXml, new ObjectWithInterfaceProperty
            {
                InterfaceProperty = new TestInterfaceImpl2() { TwoProperty = "TwoValue" }
            });
        }

        [TestMethod]
        public void Deserialize_InterfaceList_GetCorrectImplementation()
        {
            string objectXml =
                "<ListOfTestInterface>" +
                "   <TestInterface Type=\"TestInterfaceImpl1\" OneProperty=\"1\"/>" +
                "   <TestInterface Type=\"TestInterfaceImpl2\" TwoProperty=\"2\"/>" +
                "   <TestInterface Type=\"TestInterfaceImpl1\" OneProperty=\"3\"/>" +
                "</ListOfTestInterface>";

            TestDeserializationList(objectXml, new List<ITestInterface>
            {
                new TestInterfaceImpl1 { OneProperty = "1"},
                new TestInterfaceImpl2 { TwoProperty = "2"},
                new TestInterfaceImpl1 { OneProperty = "3"}
            });
        }

        [TestMethod]
        public void Deserialize_ListOfStringProperty_FillItems()
        {
            string objectXml =
                "<ObjectWithListProperty>" +
                    "<Elements>" +
                        "<String>HelloWorld1</String>" +
                        "<String>HelloWorld2</String>" +
                        "<String>HelloWorld3</String>" +
                    "</Elements>" +
                "</ObjectWithListProperty>";


            TestDeserialization(objectXml, new ObjectWithListProperty
            {
                Elements = new List<string> { "HelloWorld1", "HelloWorld2", "HelloWorld3" }
            });

        }

        [TestMethod]
        public void Deserialize_ListOfStringInElementValue_FillItems()
        {
            string objectXml =
                "<ObjectWithListProperty>" +
                    "<Elements>HelloWorld1,HelloWorld2,HelloWorld3</Elements>" +
                "</ObjectWithListProperty>";


            TestDeserialization(objectXml, new ObjectWithListProperty
            {
                Elements = new List<string> { "HelloWorld1", "HelloWorld2", "HelloWorld3" }
            });
        }

        [TestMethod]
        public void Deserialize_ListOfStringInAttribute_FillItems()
        {
            string objectXml =
                "<ObjectWithListProperty " +
                "Elements=\"HelloWorld1,HelloWorld2,HelloWorld3\"/>";


            TestDeserialization(objectXml, new ObjectWithListProperty
            {
                Elements = new List<string> { "HelloWorld1", "HelloWorld2", "HelloWorld3" }
            });
        }

        [TestMethod]
        public void Deserialize_ReferenceContainer_GetReference()
        {
            string objectXml =
                "<ObjectWithReferenceContainer>" +
                    "<NamedElements>" +
                        "<NamedElement Name=\"NameA\" Value=\"ValueA\"/>" +
                        "<NamedElement Name=\"NameB\" Value=\"ValueB\"/>" +
                    "</NamedElements>" +
                    "<Holder A=\"$NameA\" B=\"$NameB\"/>" +
                "</ObjectWithReferenceContainer>";

            var deserialized = Deserialize<ObjectWithReferenceContainer>(objectXml);
            Assert.IsNotNull(deserialized);
            Assert.IsTrue(deserialized.NamedElements.Count == 2);
            Assert.IsTrue(deserialized.NamedElements[0] == deserialized.Holder.A);
            Assert.IsTrue(deserialized.NamedElements[1] == deserialized.Holder.B);

        }

        [TestMethod]
        public void Deserialize_TestDictionary()
        {
            string objectXml =
                "<DictionaryOfStringAndString>" +
                        "<KeyValuePairOfStringAndString Key=\"NameA\" Value=\"ValueA\"/>" +
                        "<KeyValuePairOfStringAndString Key=\"NameB\" Value=\"ValueB\"/>" +
                    "</DictionaryOfStringAndString>";

            var obj = Deserialize<Dictionary<string, string>>(objectXml);

            Assert.AreEqual("ValueA", obj["NameA"]);
            Assert.AreEqual("ValueB", obj["NameB"]);
        }

        [TestMethod]
        public void Deserialize_HasConstructor_CallConstructor()
        {
            string objectXml =
                "<ConstructorObject ParamA=\"ParamA\">" +
                    "<ParamB>ParamB</ParamB>" +
                "</ConstructorObject>";

            ConstructorObject obj = Deserialize<ConstructorObject>(objectXml);

            Assert.AreEqual("ParamA", obj.ParamA);
            Assert.AreEqual("ParamB", obj.ParamB);
        }
    }
}
