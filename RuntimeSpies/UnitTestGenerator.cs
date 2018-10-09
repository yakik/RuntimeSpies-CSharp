using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeSpies
{
    public class UnitTestGenerator
    {
        public UnitTestGenerator()
        {
        }

        private static  string beforeNamespaceCode = "using System;\n" +
                                                   "using Microsoft.VisualStudio.TestTools.UnitTesting;\n";

      

        private static  string startClassCode = "[TestClass]\npublic class ";
        private static  string beforeTestMethod = "[TestMethod]\npublic void ";

        public static string GetUnitTestMethod(string code, string unitTestName)
        {
            return beforeTestMethod + unitTestName + "()\n{\n" + code + "\n}\n";
        }

        public static string GetUnitTestClass(string myCode, string myTestClassName, string myNamespace)
        {
            return beforeNamespaceCode + "\nnamespace " + myNamespace + "{\n\n" +
                   startClassCode + myTestClassName + "\n{\n" + myCode + "\n}\n}";
        }

        public static string GetTestClassWithAdditionalTest(string myTestClass, string myNewTestMethod)
        {
            return myTestClass.Substring(0,myTestClass.Length - 3) + myNewTestMethod + "\n}\n}";
        }
    }
}
