using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeSpies;

namespace RuntimeSpiesTest
{
    [TestClass]
    public class RuntimeSpyUnitTestGeneratorTests
    {

        [TestMethod]
        public void CreatingTestClass()
        {
            RuntimeSpySequence.Reset();
            string unitTestMethodCode = "var i = 3;";
            Assert.AreEqual("using System;\nusing Microsoft.VisualStudio.TestTools.UnitTesting;\n" +
                            "\nnamespace myNamespace{\n\n" +
                            "[TestClass]\npublic class myTestClassName\n{\n" +
                "[TestMethod]\npublic void myTestName0()\n{\nvar i = 3;\n}\n\n}\n}",
                RuntimeSpyUnitTestGenerator.AddCodeAsUnitTestToTestClass(unitTestMethodCode, "myTestName", "myTestClassName", "myNamespace", "EMPTY"));
        }

        [TestMethod]
        public void AddingTestMethodToATestClass()
        {
            RuntimeSpySequence.Reset();
            string myTestClass = RuntimeSpyUnitTestGenerator.AddCodeAsUnitTestToTestClass("var i = 3;", "myTestName", "myTestClassName", "myNamespace", "EMPTY");
            string unitTestMethodCode = "var i = 3;";
            Assert.AreEqual("using System;\nusing Microsoft.VisualStudio.TestTools.UnitTesting;\n" +
                            "\nnamespace myNamespace{\n\n" +
                            "[TestClass]\npublic class myTestClassName\n{\n" +
                            "[TestMethod]\npublic void myTestName0()\n{\nvar i = 3;\n}\n\n"+
                            "[TestMethod]\npublic void myTestName1()\n{\nvar i = 3;\n}"+
                            "\n\n}\n}",
                RuntimeSpyUnitTestGenerator.AddCodeAsUnitTestToTestClass(unitTestMethodCode, "myTestName", "myTestClassName", "myNamespace", myTestClass));
        }
    }
}
