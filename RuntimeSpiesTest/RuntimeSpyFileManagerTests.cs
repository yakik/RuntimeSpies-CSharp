using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeSpies;

namespace RuntimeSpiesTest
{
    [TestClass]
    public class RuntimeSpyFileManagerTests
    {
        [TestMethod]
        public void E2ETest()
        {
            File.Delete("dataFileForUnitTest.txt");

            string text1 = RuntimeSpyFileManager.GetTestFileCode("dataFileForUnitTest.txt");
            Assert.AreEqual("EMPTY",text1);
            RuntimeSpyFileManager.UpdateTestFile("dataFileForUnitTest.txt","AA\nBBB\n\n");
            string text2 = RuntimeSpyFileManager.GetTestFileCode("dataFileForUnitTest.txt");
            Assert.AreEqual("AA\nBBB\n\n", text2);
            RuntimeSpyFileManager.UpdateTestFile("dataFileForUnitTest.txt", "AA\nBXXXBB\n\n");
            string text3 = RuntimeSpyFileManager.GetTestFileCode("dataFileForUnitTest.txt");
            Assert.AreEqual("AA\nBXXXBB\n\n", text3);

        }
    }
}
