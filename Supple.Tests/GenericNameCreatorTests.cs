using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using Supple.Xml.NameCreators;
using System.Collections.Generic;

namespace Supple.Tests
{
    [TestClass]
    public class GenericNameCreatorTests
    {
        private GenericNameCreator _nameCreator;

        [TestInitialize]
        public void Initialize()
        {
            _nameCreator = new GenericNameCreator();
        }

        [TestMethod]
        public void NormalType_CreateName()
        {
            string typeName = _nameCreator.CreateName(typeof(ConstructorObject));
            Assert.AreEqual(nameof(ConstructorObject), typeName);
        }

        [TestMethod]
        public void TwoGenericArgs_CreateName()
        {
            string typeName = _nameCreator.CreateName(typeof(KeyValuePair<string,string>));
            Assert.AreEqual("KeyValuePairOfStringAndString", typeName);
        }

        [TestMethod]
        public void OneGenericArg_CreateName()
        {
            string typeName = _nameCreator.CreateName(typeof(List<string>));
            Assert.AreEqual("ListOfString", typeName);
        }

        [TestMethod]
        public void InterfaceType_CreateName()
        {
            string typeName = _nameCreator.CreateName(typeof(ITestInterface));
            Assert.AreEqual("TestInterface", typeName);
        }

        [TestMethod]
        public void InterfaceGenericArg_CreateName()
        {
            string typeName = _nameCreator.CreateName(typeof(List<ITestInterface>));
            Assert.AreEqual("ListOfTestInterface", typeName);
        }
    }
}
