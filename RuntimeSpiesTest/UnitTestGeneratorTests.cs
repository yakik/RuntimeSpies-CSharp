using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeSpies;

namespace RuntimeSpiesTest
{
    [TestClass]
    public class UnitTestGeneratorTests
    {
        [TestMethod]
        public void CreatingTestMethod()
        {
            string myCode = "var i = 3;";
            Assert.AreEqual("[TestMethod]\npublic void myTestName()\n{\nvar i = 3;\n}\n",
                UnitTestGenerator.GetUnitTestMethod(myCode, "myTestName"));
        }

        [TestMethod]
        public void CreatingTestClass()
        {
            string myCode = "[TestMethod]\npublic void myTestName()\n{\nvar i = 3;\n}\n";
            Assert.AreEqual("using System;\nusing Microsoft.VisualStudio.TestTools.UnitTesting;\n" +
                            "\nnamespace myNamespace{\n\n" +
                            "[TestClass]\npublic class myTestClassName\n{\n" +
                "[TestMethod]\npublic void myTestName()\n{\nvar i = 3;\n}\n\n}\n}",
                UnitTestGenerator.GetUnitTestClass(myCode, "myTestClassName", "myNamespace"));
        }

        [TestMethod]
        public void AddingTestMethodToATestClass()
        {
            string myTestClass = "using System;\nusing Microsoft.VisualStudio.TestTools.UnitTesting;\n" +
                                  "\nnamespace myNamespace{\n\n" +
                                  "[TestClass]\npublic class myTestClassName\n{\n" +
                                  "[TestMethod]\npublic void myTestName()\n{\nvar i = 3;\n}\n\n}\n}";
            string myNewTestMethod = "[TestMethod]\npublic void myTestName2()\n{\nvar i = 3;\n}\n";
            Assert.AreEqual("using System;\nusing Microsoft.VisualStudio.TestTools.UnitTesting;\n" +
                            "\nnamespace myNamespace{\n\n" +
                            "[TestClass]\npublic class myTestClassName\n{\n" +
                            "[TestMethod]\npublic void myTestName()\n{\nvar i = 3;\n}\n\n"+
                            "[TestMethod]\npublic void myTestName2()\n{\nvar i = 3;\n}"+
                            "\n\n}\n}",
                UnitTestGenerator.GetTestClassWithAdditionalTest(myTestClass, myNewTestMethod));
        }
    }
}
