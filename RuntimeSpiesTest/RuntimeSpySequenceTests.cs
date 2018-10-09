using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeSpies;

namespace RuntimeSpiesTest
{
    [TestClass]
    public class RuntimeSpySequenceTests
    {
        [TestMethod]
        public void TestSequence()
        {
            RuntimeSpySequence.Reset();
            Assert.AreEqual(0,RuntimeSpySequence.GetSequence());
            Assert.AreEqual(1,RuntimeSpySequence.GetSequence());
            Assert.AreEqual(2,RuntimeSpySequence.GetSequence());
        }
        [TestMethod]
        public void TestSequence2()
        {
            RuntimeSpySequence.Reset();
            Assert.AreEqual(0, RuntimeSpySequence.GetSequence());
            Assert.AreEqual(1, RuntimeSpySequence.GetSequence());
            Assert.AreEqual(2, RuntimeSpySequence.GetSequence());
        }
    }
}
