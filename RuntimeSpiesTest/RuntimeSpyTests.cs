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
        private int testFunction(int a, int b, string d)
        {
            var mySpy = new RuntimeSpy();
            mySpy.HowToInstantiateMethodClass = "//No special treatment\n";
            mySpy.SetMethodParameters(MethodBase.GetCurrentMethod(), a, b, d);
            mySpy.HowToCallMethod = "testFunction";
            var c = a + b;
            mySpy.setMethodReturnValue(d + c.ToString());
            mySpy.addToTestFile("myTestMethod", "myTestClass", "myNamespace", "testFile.cs");
            return c;
        }

        [TestMethod]
        public void TestHarness()
        {
            RuntimeSpySequence.Reset();
            File.Delete("testFile.cs");
            testFunction(23, 3, "Hi \n \" \\ \v \t \r");
            testFunction(1, 2, "Hello\nHello\n");
            testFunction(-34, 23, "AAAAA");
            FileAssert.AreEqual(@"testFile.cs", @"..\..\testFileMASTER.cs");
        }
    }
}
