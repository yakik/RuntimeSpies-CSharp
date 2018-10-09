using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeSpies;

namespace RuntimeSpiesTest
{
    [TestClass]
    public class RuntimeSpyTests
    {
        private string harness = "";

        private int testFunction(int a, int b)
        {
            var mySpy = new RuntimeSpy();
            mySpy.HowToInstantiateMethodClass = "//No special treatment\n";
            mySpy.SetMethodParameters(MethodBase.GetCurrentMethod(), a, b);
            mySpy.HowToCallMethod = "testFunction";
            var c = a + b;
            mySpy.setMethodReturnValue(c);
            mySpy.addToTestFile("myTestMethod", "myTestClass", "myNamespace", "testFile.cs");
            return c;
        }

        [TestMethod]
        public void TestHarness()
        {
            RuntimeSpySequence.Reset();
            File.Delete("testFile.cs");
            testFunction(23, 3);
            testFunction(1, 2);
            testFunction(-34, 23);
            FileAssert.AreEqual(@"testFile.cs", @"..\..\testFileMASTER.cs");
            //  Assert.AreEqual("//No special treatment\n\nvar a = 23 ;\nvar b = \"twenty three\" ;\n\nAssert.AreEqual(\"new System.Int32[] {1,2,3,4}\", VariableLiteral.GetNewLiteral(testFunction(a, b).getLiteral());\n",
            //   harness);
        }
    }
}
