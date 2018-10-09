using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeSpies;

namespace RuntimeSpiesTest
{
    [TestClass]
    public class UnitTestGeneratorTests
    {
        [TestMethod]
        public void creatingTestMethod()
        {
            string myCode = "var i = 3;";
            Assert.AreEqual("[TestMethod]\npublic void myTestName(\n{\nvar i = 3;\n}\n",
                UnitTestGenerator.GetUnitTestMethod(myCode, "myTestName"));
        }
    }
}
