using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeSpies;

namespace RuntimeSpiesTest
{
    [TestClass]
    public class VariableLiteralTests
    {
        class TestClass
        {
            public TestClass()
            {
            }

            public int FieldA { get; set; }
            public String FieldB { get; set; }

        }
       

        [TestMethod]
        public void ClassTest()
        {
            var a = new TestClass
            {
                FieldA = 4,
                FieldB = "Hello"
            };
            var myDeclaration = VariableLiteral.GetNewLiteral(a).GetDeclaration();

            Assert.AreEqual(myDeclaration,
                "new TestClass\n{\nFieldA = 4,\nFieldB = \"Hello\"\n}");
           
        }
    }
}