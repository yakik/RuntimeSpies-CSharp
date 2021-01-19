using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeSpies;

namespace RuntimeSpiesTest
{
    class TestClass
    {
        public TestClass(int A, String B, bool C, char D, long E, float F)
        {
            FieldA = A;
            FieldB = B;
            FieldC = C;
            FieldD = D;
            FieldE = E;
            FieldF = F;
        }

        int FieldA { get; set; }
        private String FieldB { get; set; }
        private bool FieldC { get; set; }
        private char FieldD { get; set; }
        private long FieldE { get; set; }
        private float FieldF { get; set; }

    }

    class TestClassNoGetter
    {
       
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
        enum Days { Sun, Mon, tue, Wed, thu, Fri, Sat };
        [Flags] enum myColors { Red = 1, Green = 2, Yellow = 4, Blue = 8 }
  

        [TestMethod]
        public void NullTests()
        {
            TestClass a = null;
            var myDeclaration = VariableLiteral.GetNewLiteral(a);

            Assert.AreEqual("null",
                myDeclaration);
        }

        [TestMethod]
        public void NullPropertyTests()
        {
            var a = new TestClass(5, null, true, '\n', 123443435465, 123.453F);
            
            var myDeclaration = VariableLiteral.GetNewLiteral(a);

            Assert.AreEqual("new TestClass {FieldA = 5,FieldB = null,FieldC = true,FieldD = '\\n',FieldE = 123443435465,FieldF = 123.453F}",
                myDeclaration);
        }



        [TestMethod]
        public void PrimitiveAndClassTests()
        {
            var a = new TestClass(4, "Hello\r\nHello\tmy\'Tab\"\v", true, '\n', 123443435465, 123.453F);
        
            var myDeclaration = VariableLiteral.GetNewLiteral(a);

            Assert.AreEqual("new TestClass {FieldA = 4,FieldB = \"Hello\\r\\nHello\\tmy\\\'Tab\\\"\\v\",FieldC = true,FieldD = '\\n',FieldE = 123443435465,FieldF = 123.453F}",
                myDeclaration);
        }

        [TestMethod]
        public void ArrayTests()
        {

            int[] myArray = {1, 2, 3, 4, 5};

            var myDeclaration = VariableLiteral.GetNewLiteral(myArray);

            Assert.AreEqual("new System.Int32[] {1,2,3,4,5}",
                myDeclaration);



        }

        [TestMethod]
        public void EnumTests()
        {
            Days myDay = Days.Sun;
           

        var myDeclaration = VariableLiteral.GetNewLiteral(myDay);

            Assert.AreEqual("Days.Sun",
                myDeclaration);



        }

        [TestMethod]
        public void EnumFlagsTests()
        {
            myColors testColors = myColors.Blue | myColors.Green;

            var myDeclaration = VariableLiteral.GetNewLiteral(testColors);

            Assert.AreEqual("myColors.Green | myColors.Blue",
                myDeclaration);
        }

        [TestMethod]
        public void ListTests()
        {
            var a = new TestClass(4,"Hello\nHello",true,'\n',123443435465, 123.453F);
            var b = new TestClass(4, "Hello\nHello\nHello", false, '\n', 12653443435465, 123.47653F);
           
            List<TestClass> myList = new List<TestClass>
            {
                a,
                b
            };

            var myDeclaration = VariableLiteral.GetNewLiteral(myList);

            Assert.AreEqual("new List<RuntimeSpiesTest.TestClass>{new TestClass {FieldA = 4,FieldB = \"Hello\\nHello\",FieldC = true,FieldD = \'\\n\',FieldE = 123443435465,FieldF = 123.453F},new TestClass {FieldA = 4,FieldB = \"Hello\\nHello\\nHello\",FieldC = false,FieldD = \'\\n\',FieldE = 12653443435465,FieldF = 123.4765F}}",
                myDeclaration);



        }
    }
}