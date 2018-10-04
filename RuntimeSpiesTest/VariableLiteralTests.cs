using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeSpies;

namespace RuntimeSpiesTest
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

    class TestClassNoGetter
    {
        public TestClassNoGetter()
        {
        }

        private int _FieldA;
        public int FieldA
        {
            set { _FieldA = value; }
        }
        public String FieldB { get; set; }

    }

    [TestClass]
    public class VariableLiteralTests
    {
        [TestMethod]
        public void NoGetterTest()
        {
            var a = new TestClassNoGetter
            {
                FieldA = 5,
                FieldB = "Hi!",
            };
            var myDeclaration = VariableLiteral.GetNewLiteral(a).GetLiteral();

            Assert.AreEqual("new TestClassNoGetter {FieldB = \"Hi!\"}",
                myDeclaration);
        }

        [TestMethod]
        public void NullTests()
        {
            TestClass a = null;
            var myDeclaration = VariableLiteral.GetNewLiteral(a).GetLiteral();

            Assert.AreEqual("null",
                myDeclaration);
        }

        [TestMethod]
        public void NullPropertyTests()
        {
            var a = new TestClass
            {
                FieldA = 5,
                FieldB = null,
                FieldC = true,
                FieldD = '\n',
                FieldE = 123443435465,
                FieldF = 123.453F
            };
            var myDeclaration = VariableLiteral.GetNewLiteral(a).GetLiteral();

            Assert.AreEqual("new TestClass {FieldA = 5,FieldB = null,FieldC = true,FieldD = '\\n',FieldE = 123443435465,FieldF = 123.453F}",
                myDeclaration);
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
            var myDeclaration = VariableLiteral.GetNewLiteral(a).GetLiteral();

            Assert.AreEqual("new TestClass {FieldA = 4,FieldB = \"Hello\\nHello\",FieldC = true,FieldD = '\\n',FieldE = 123443435465,FieldF = 123.453F}",
                myDeclaration);
        }

        [TestMethod]
        public void ArrayTests()
        {

            int[] myArray = {1, 2, 3, 4, 5};

            var myDeclaration = VariableLiteral.GetNewLiteral(myArray).GetLiteral();

            Assert.AreEqual("new System.Int32[] {1,2,3,4,5}",
                myDeclaration);



        }

        [TestMethod]
        public void ListTests()
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
            var b = new TestClass
            {
                FieldA = 4,
                FieldB = "Hello\nHello\nHello",
                FieldC = false,
                FieldD = '\n',
                FieldE = 12653443435465,
                FieldF = 123.47653F
            };

            List<TestClass> myList = new List<TestClass>
            {
                a,
                b
            };

            var myDeclaration = VariableLiteral.GetNewLiteral(myList).GetLiteral();

            Assert.AreEqual("new List<RuntimeSpiesTest.TestClass>{new TestClass {FieldA = 4,FieldB = \"Hello\\nHello\",FieldC = true,FieldD = \'\\n\',FieldE = 123443435465,FieldF = 123.453F},new TestClass {FieldA = 4,FieldB = \"Hello\\nHello\\nHello\",FieldC = false,FieldD = \'\\n\',FieldE = 12653443435465,FieldF = 123.4765F}}",
                myDeclaration);



        }
    }
}