using System;
using System.ComponentModel;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RuntimeSpiesTest
{
    [TestClass]
    public class RuntimeSpyTests
    {
        private string testFunction(int a, string b)
        {
            var mySpy = new RuntimeSpy();
            mySpy.SetMethodCall(MethodBase.GetCurrentMethod(), a, b);

            return mySpy.getHarness();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("a = 23 ;\nb = \"twenty three\" ;\n\ntestFunction(a, b);\n", testFunction(23,"twenty three"));
        }
    }
}
