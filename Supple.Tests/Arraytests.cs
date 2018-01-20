using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supple.Tests.TestObjects;
using Supple.Xml;
using System.Collections.Generic;

namespace Supple.Tests
{
    [TestClass]
    public class ArrayTests
    {
        private SuppleDeserializerTester _tester;

        [TestInitialize]
        public void Initialize()
        {
            _tester = new SuppleDeserializerTester();
        }

        [TestMethod]
        public void ArrayAsValue_Deserialize()
        {
            string objectXml = "<ArrayOfInt32>[1,2]</ArrayOfInt32>";

            var obj = _tester.Deserialize<int[]>(objectXml);

            Assert.IsNotNull(obj);
            Assert.AreEqual(1, obj[0]);
            Assert.AreEqual(2, obj[1]);
        }

        [TestMethod]
        public void ArrayAsElements_Deserialize()
        {
            string objectXml = "<ArrayOfInt32><Int32>10</Int32><Int32>20</Int32></ArrayOfInt32>";

            var obj = _tester.Deserialize<int[]>(objectXml);

            Assert.IsNotNull(obj);
            Assert.AreEqual(10, obj[0]);
            Assert.AreEqual(20, obj[1]);
        }

        [TestMethod]
        public void ArrayOfInterfaces_Deserialize()
        {
            string objectXml = 
                "<ArrayOfTestInterface>" +
                "   <TestInterface Type=\"TestInterfaceImpl1\" OneProperty=\"1\"/>" +
                "   <TestInterface Type=\"TestInterfaceImpl2\" TwoProperty=\"2\"/>" +
                "   <TestInterface Type=\"TestInterfaceImpl1\" OneProperty=\"3\"/>" +
                "</ArrayOfTestInterface>";

            var objects = _tester.Deserialize<ITestInterface[]>(objectXml);

            Assert.IsNotNull(objects);
        }

        [TestMethod]
        public void MultiDimensionalArray_Deserialize()
        {
            string objectXml =
                "<ArrayOfArrayOfInt32>" +
                    "<ArrayOfInt32>[0,1]</ArrayOfInt32> "+
                    "<ArrayOfInt32>[7,8]</ArrayOfInt32>" +
                "</ArrayOfArrayOfInt32>";

            int[][] arr = _tester.Deserialize<int[][]>(objectXml);

            Assert.AreEqual(0, arr[0][0]);
            Assert.AreEqual(1, arr[0][1]);
            Assert.AreEqual(7, arr[1][0]);
            Assert.AreEqual(8, arr[1][1]);
        }

        [TestMethod]
        public void ArrayOfList_Deserialize()
        {
            string objectXml =
                "<ArrayOfListOfInt32>" +
                    "<ListOfInt32>[0,1]</ListOfInt32> " +
                    "<ListOfInt32>[7,8]</ListOfInt32>" +
                "</ArrayOfListOfInt32>";

            List<int>[] arrayOfLists = _tester.Deserialize<List<int>[]>(objectXml);

            Assert.AreEqual(0, arrayOfLists[0][0]);
            Assert.AreEqual(1, arrayOfLists[0][1]);
            Assert.AreEqual(7, arrayOfLists[1][0]);
            Assert.AreEqual(8, arrayOfLists[1][1]);
        }
    }
}
