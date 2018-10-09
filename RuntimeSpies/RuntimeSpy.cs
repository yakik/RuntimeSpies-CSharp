using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using RuntimeSpies;

namespace RuntimeSpiesTest
{
    public class RuntimeSpy
    {
         internal class methodParameter
        {
            public string Name { get; set; }
            public string ValueLiteral { get; set; }
            
            public methodParameter(string name, string valueLiteral)
            {
                this.Name = name;
                this.ValueLiteral = valueLiteral;
            }
        }

        public string HowToInstantiateMethodClass { get; set; }
        public string HowToCallMethod { get; set; }
        string ReturnedLiteral = null;
        string MethodSpiedName;
        List<methodParameter> _methodParameters = new List<methodParameter>();

        

        private void addParameter(string name, object value)
        {
            string valueLiteral = VariableLiteral.GetNewLiteral(value).GetLiteral();
            _methodParameters.Add(new methodParameter(name,valueLiteral));
        }

        public RuntimeSpy()
        {
        }

        private string getCommaSeparatedParametersList()
        {
            string returnedCode = "";
            int parameterIndex = 0;
            foreach (var parameter in _methodParameters)
            {
                if (parameterIndex > 0) returnedCode += ", ";
                returnedCode += parameter.Name ;
                parameterIndex++;
            }

            return returnedCode;

        }

        public string getHarness()
        {
            string harness = "";
            harness += HowToInstantiateMethodClass + "\n";
            foreach (var parameter in _methodParameters)
            {
                harness += "var " + parameter.Name + " = " + parameter.ValueLiteral + " ;\n";
            }

            harness += "\n";
            harness += "Assert.AreEqual(\""+this.ReturnedLiteral + "\""+
                       ", VariableLiteral.GetNewLiteral("+ HowToCallMethod + "(" + getCommaSeparatedParametersList() + ").getLiteral());\n";
            harness += "";
            return harness;

        }

        public void SetMethodParameters(MethodBase methodInfo, params object[] values)
        {
            MethodSpiedName = methodInfo.Name;
            int parameterIndex = 0;
            foreach (var parameter in methodInfo.GetParameters())
            {
                addParameter(parameter.Name, values[parameterIndex]);
                parameterIndex++;
            }

        

        }

        public void setMethodReturnValue(object returnedValue)
        {
            this.ReturnedLiteral = VariableLiteral.GetNewLiteral(returnedValue).GetLiteral();
        }
    }

    

}