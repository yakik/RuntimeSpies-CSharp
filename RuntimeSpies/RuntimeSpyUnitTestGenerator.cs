using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeSpies
{
    public class RuntimeSpyUnitTestGenerator
    {
        public RuntimeSpyUnitTestGenerator()
        {
        }

        private static  string beforeNamespaceCode = "using System;\n" +
                                                   "using Microsoft.VisualStudio.TestTools.UnitTesting;\n";

      

        private static  string startClassCode = "[TestClass]\npublic class ";
        private static  string beforeTestMethod = "[TestMethod]\npublic void ";

        private static string GetUnitTestMethod(string code, string unitTestName)
        {
            int mySequence = RuntimeSpySequence.GetSequence();
            return beforeTestMethod + unitTestName + mySequence + "()\n{\n" + code + "\n}\n";
        }

        private static string GetUnitTestClass(string testMethodCode, string myTestClassName, string myNamespace)
        {
            return beforeNamespaceCode + "\nnamespace " + myNamespace + "{\n\n" +
                   startClassCode + myTestClassName + "\n{\n" + testMethodCode + "\n}\n}";
        }

        private static string GetTestClassWithAdditionalTest(string myTestClass, string myNewTestMethod)
        {
            return myTestClass.Substring(0,myTestClass.Length - 3) + myNewTestMethod + "\n}\n}";
        }

        public static string AddCodeAsUnitTestToTestClass(string code, string unitTestName, string testClassName,
            string testNamespaceName, string currentClassCode)
        {
            if (currentClassCode == "EMPTY")
                return GetUnitTestClass(GetUnitTestMethod(code, unitTestName), testClassName,
                    testNamespaceName);
            else
                return GetTestClassWithAdditionalTest(currentClassCode, GetUnitTestMethod(code, unitTestName));
        }
    }
}
