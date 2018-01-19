using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Collections;
using Supple.Tests.TestObjects;
using Supple.Xml;
using System;
using System.Collections.Generic;

namespace Supple.Tests
{
    [TestClass]
    public class CollectionsTests
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
        public void ListOfSubProperties_SetItems()
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

            var deserialized = _tester.Deserialize<List<SubProperties>>(objectXml);

            Assert.IsTrue(EnumerableExtentions.IsItemsEqual(deserialized, list), "Items Are Not Equal!");
        }

        [TestMethod]
        public void InterfaceList_GetCorrectImplementation()
        {
            string objectXml =
                "<ListOfTestInterface>" +
                "   <TestInterface Type=\"TestInterfaceImpl1\" OneProperty=\"1\"/>" +
                "   <TestInterface Type=\"TestInterfaceImpl2\" TwoProperty=\"2\"/>" +
                "   <TestInterface Type=\"TestInterfaceImpl1\" OneProperty=\"3\"/>" +
                "</ListOfTestInterface>";

            _tester.TestDeserializationList(objectXml, new List<ITestInterface>
            {
                new TestInterfaceImpl1 { OneProperty = "1"},
                new TestInterfaceImpl2 { TwoProperty = "2"},
                new TestInterfaceImpl1 { OneProperty = "3"}
            });
        }

        [TestMethod]
        public void ListOfStrings_FillItems()
        {
            string objectXml =
                "<ObjectWithListProperty>" +
                    "<Elements>" +
                        "<String>HelloWorld1</String>" +
                        "<String>HelloWorld2</String>" +
                        "<String>HelloWorld3</String>" +
                    "</Elements>" +
                "</ObjectWithListProperty>";


            _tester.TestDeserialization(objectXml, new ObjectWithListProperty
            {
                Elements = new List<string> { "HelloWorld1", "HelloWorld2", "HelloWorld3" }
            });

        }

        [TestMethod]
        public void ListOfStringInElementValue_FillItems()
        {
            string objectXml =
                "<ObjectWithListProperty>" +
                    "<Elements>[HelloWorld1, HelloWorld2, HelloWorld3]</Elements>" +
                "</ObjectWithListProperty>";


            _tester.TestDeserialization(objectXml, new ObjectWithListProperty
            {
                Elements = new List<string> { "HelloWorld1", "HelloWorld2", "HelloWorld3" }
            });
        }

        [TestMethod]
        public void ListOfStringInAttribute_FillItems()
        {
            string objectXml =
                "<ObjectWithListProperty " +
                "Elements=\"[HelloWorld1, HelloWorld2, HelloWorld3]\"/>";


            _tester.TestDeserialization(objectXml, new ObjectWithListProperty
            {
                Elements = new List<string> { "HelloWorld1", "HelloWorld2", "HelloWorld3" }
            });
        }

        [TestMethod]
        public void DictionaryOfStringAndString_Deserialize()
        {
            string objectXml =
                "<DictionaryOfStringAndString>" +
                        "<KeyValuePairOfStringAndString Key=\"NameA\" Value=\"ValueA\"/>" +
                        "<KeyValuePairOfStringAndString Key=\"NameB\" Value=\"ValueB\"/>" +
                    "</DictionaryOfStringAndString>";

            var obj = _tester.Deserialize<Dictionary<string, string>>(objectXml);

            Assert.AreEqual("ValueA", obj["NameA"]);
            Assert.AreEqual("ValueB", obj["NameB"]);
        }
    }
}
