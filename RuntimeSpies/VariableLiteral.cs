using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeSpies
{
    public abstract class VariableLiteral
    {
        protected object MyObject;
        internal VariableLiteral(object myObject)
        {
            MyObject = myObject;
        }

        public static String GetNewLiteral(object myObject)
        {
            switch (myObject)
            {
                case null:
                    return GetNullLiteral(myObject);
                case Enum _:
                    return GetEnumLiteral(myObject);
                case Array _:
                    return GetArrayLiteral(myObject);
                case bool _:
                    return GetBoolLiteral(myObject);
                case char _:
                    return GetCharLiteral(myObject);
                case float _:
                    return GetFloatLiteral(myObject);
                case string _:
                    return GetStringLiteral(myObject);
                case IList _ when myObject.GetType().IsGenericType:
                    return  GetIListLiteral(myObject);
            }

            if (myObject.GetType().IsClass)
                return GetClassLiteral(myObject);

            return GetSimpleLiteral(myObject);
        }

        

        internal static bool HasFlag(Enum variable, Enum value)
            //Source: https://forums.asp.net/t/1813357.aspx?Hasflag+function+is+not+working+in+framework+3+5
        {
            // check if from the same type.
            if (variable.GetType() != value.GetType())
            {
                throw new ArgumentException("The checked flag is not from the same type as the checked variable.");
            }

            Convert.ToUInt64(value);
            ulong num = Convert.ToUInt64(value);
            ulong num2 = Convert.ToUInt64(variable);

            return (num2 & num) == num;
        }

        public  static String GetEnumLiteral(object MyObject)
        {
            var declaration = "";
            int index = 0;
            foreach (Enum value in Enum.GetValues(MyObject.GetType()))
            {
                // if (((Enum) MyObject).HasFlag(value)) //This is for .Net 4 and up
                if (HasFlag((Enum)MyObject, value)) //This to support .Net 3.5
                {
                    if (index > 0) declaration += " | ";
                    declaration += MyObject.GetType().Name + "." + value;
                    index++;
                }
            }
            return declaration;
        }



        public static String GetIListLiteral(object MyObject)
        {
            var declaration = "new List<" + MyObject.GetType().GetGenericArguments().Single() + ">{";
            var myCollection = (IEnumerable)MyObject;
            var listItemIndex = 0;
            foreach (var listItem in myCollection)
            {

                if (listItemIndex > 0)
                    declaration += ",";
                declaration += VariableLiteral.GetNewLiteral(listItem);

                listItemIndex++;
            }
            declaration += "}";
            return declaration;
        }

        public  static String GetClassLiteral(object MyObject)
        {
            var declaration = "new " + MyObject.GetType().Name + " {";
            var propertyIndex = 0;
            foreach (var property in MyObject.GetType().GetProperties())
            {

                if (property.GetGetMethod() != null)
                {
                    if (propertyIndex > 0)
                        declaration += ",";
                    declaration += property.Name + " = " +
                                   VariableLiteral.GetNewLiteral(property.GetValue(MyObject, null));

                    propertyIndex++;
                }
            }
            declaration += "}";
            return declaration;
        }

       
        public static String GetArrayLiteral(object MyObject)
        {
            Array MyArray = (Array) MyObject;
            var declaration = "new " + MyArray.GetType().GetElementType() + "[] {";
            var elementIndex = 0;
            foreach (var element in MyArray)
            {

                if (elementIndex > 0)
                    declaration += ",";
                declaration += VariableLiteral.GetNewLiteral(element);

                elementIndex++;
            }
            declaration += "}";
            return declaration;
        }

       
        public static string GetStringLiteral(object MyObject)
        {
            String MyString = (string)MyObject;
            var declaration = "\"" + MyString.Replace("\\","\\\\").Replace("\n", "\\n").Replace("\r", "\\r").
                                  Replace("\"", "\\\"").Replace("\v", "\\v").Replace("\t", "\\t").
                                  Replace("\'", "\\\'") + "\"";

            return declaration;
        }
  

        public static string GetCharLiteral(object MyObject)
        {
            string declaration;
            if ((char)MyObject == '\n')
                declaration = "\'\\n\'";
            else
                declaration = "\'" + MyObject + "\'";


            return declaration;
        }
   

        public static string GetFloatLiteral(object MyObject)
        {
            var declaration = MyObject.ToString() + "F";

            return declaration;
        }
   

        public static string GetNullLiteral(object MyObject)
        {

            return "null";
        }
   

        public static string GetSimpleLiteral(object MyObject)
        {
            var declaration = MyObject.ToString();

            return declaration;
        }
    

  

        public static string GetBoolLiteral(object MyObject)
        {
            if ((bool)MyObject)
                return "true";
            else
                return "false";

        }
    }
}
