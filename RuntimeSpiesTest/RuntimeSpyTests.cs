using System;
using System.ComponentModel;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RuntimeSpiesTest
{
    [TestClass]
    public class RuntimeSpyTests
    {
        private string harness = "";

        private int[] testFunction(int a, string b)
        {
            var mySpy = new RuntimeSpy();
            mySpy.HowToInstantiateMethodClass = "//No special treatment\n";
         mySpy.HowToCallMethod = "testFunction";
        mySpy.SetMethodParameters(MethodBase.GetCurrentMethod(), a, b);
            int[] returnedArray = new int[] {1,2,3,4};
            mySpy.setMethodReturnValue(returnedArray);
            harness = mySpy.getHarness();
            return returnedArray;
        }

        [TestMethod]
        public void TestHarness()
        {
            testFunction(23, "twenty three");
            Assert.AreEqual("******Begin UT******\n//No special treatment\n\nvar a = 23 ;\nvar b = \"twenty three\" ;\n\nAssert.AreEqual(\"new System.Int32[] {1,2,3,4}\", VariableLiteral.GetNewLiteral(testFunction(a, b).getLiteral());\n******End UT******\n",
                harness);
        }
    }
}
