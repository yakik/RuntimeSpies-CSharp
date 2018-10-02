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
            public bool FieldC { get; set; }
            public char FieldD { get; set; }
            public long FieldE { get; set; }
            public float FieldF { get; set; }

        }


        [TestMethod]
        public void PrimitiveAndClassTests()
        {
            var a = new TestClass
            {
                FieldA = 4,
                FieldB = "Hello\nHello",
                FieldC = true,
                FieldD = '\n',
                FieldE = 123443435465,
                FieldF = 123.453F
            };
            var myDeclaration = VariableLiteral.GetNewLiteral(a).GetDeclaration();

            Assert.AreEqual(
                "new TestClass {FieldA = 4,FieldB = \"Hello\\nHello\",FieldC = true,FieldD = '\\n',FieldE = 123443435465,FieldF = 123.453F}",
                myDeclaration);

        }

    }
}