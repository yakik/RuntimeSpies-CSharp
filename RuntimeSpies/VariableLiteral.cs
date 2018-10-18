using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeSpies
{
    abstract public class VariableLiteral
    {
        protected object MyObject;
        internal VariableLiteral(object myObject)
        {
            this.MyObject = myObject;
        }

        public static VariableLiteral GetNewLiteral(object myObject)
        {
            if (myObject == null)
                return new VariableLiteralNull(myObject);
            if (myObject is Enum)
                return new VariableLiteralEnum(myObject);
            if (myObject is Array)
                return new VariableLiteralArray(myObject);
            if (myObject is bool)
                return new VariableLiteralBool(myObject);
            if (myObject is char)
                return new VariableLiteralChar(myObject);
            if (myObject is float)
                return new VariableLiteralFloat(myObject);
            if (myObject is string)
                return new VariableLiteralString(myObject);
            if (myObject is IList && myObject.GetType().IsGenericType)
                return new VariableLiteralList(myObject);
            if (myObject.GetType().IsClass)
                return new VariableLiteralClass(myObject);

            return new VariableLiteralSimple(myObject);
        }

        public abstract string GetLiteral();

    }

    internal class VariableLiteralList : VariableLiteral
    {
        internal VariableLiteralList(object myObject) : base(myObject)
        {

        }

        public override string GetLiteral()
        {
            var declaration = "new List<" + this.MyObject.GetType().GetGenericArguments().Single() + ">{";
            var myCollection = (IEnumerable)this.MyObject;
            var listItemIndex = 0;
            foreach (var listItem in myCollection)
            {

                if (listItemIndex > 0)
                    declaration += ",";
                declaration += VariableLiteral.GetNewLiteral(listItem).GetLiteral();

                listItemIndex++;
            }
            declaration += "}";
            return declaration;
        }
    }

    internal class VariableLiteralEnum : VariableLiteral
    {
        internal VariableLiteralEnum(object myObject) : base(myObject)
        {

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

        public override string GetLiteral()
        {
            var declaration = "";
            int index = 0;
            foreach (Enum value in Enum.GetValues(this.MyObject.GetType()))
            {
                // if (((Enum) this.MyObject).HasFlag(value)) //This is for .Net 4 and up
                if (HasFlag((Enum)this.MyObject, value)) //This to support .Net 3.5
                {
                    if (index > 0) declaration += " | ";
                    declaration += this.MyObject.GetType().Name + "." + value;
                    index++;
                }
            }
            return declaration;
        }
    }

    internal class VariableLiteralClass : VariableLiteral
    {
        internal VariableLiteralClass(object myObject) : base(myObject)
        {

        }

        public override string GetLiteral()
        {
            var declaration = "new " + this.MyObject.GetType().Name + " {";
            var propertyIndex = 0;
            foreach (var property in this.MyObject.GetType().GetProperties())
            {

                if (property.GetGetMethod() != null)
                {
                    if (propertyIndex > 0)
                        declaration += ",";
                    declaration += property.Name + " = " +
                                   VariableLiteral.GetNewLiteral(property.GetValue(this.MyObject, null)).GetLiteral();

                    propertyIndex++;
                }
            }
            declaration += "}";
            return declaration;
        }
    }

    internal class VariableLiteralArray : VariableLiteral
    {
        private Array MyArray { get; set; }

        internal VariableLiteralArray(object myObject) : base(myObject)
        {
            this.MyArray = (Array) MyObject;
        }

        public override string GetLiteral()
        {
            var declaration = "new " + this.MyArray.GetType().GetElementType() + "[] {";
            var elementIndex = 0;
            foreach (var element in this.MyArray)
            {

                if (elementIndex > 0)
                    declaration += ",";
                declaration += VariableLiteral.GetNewLiteral(element).GetLiteral();

                elementIndex++;
            }
            declaration += "}";
            return declaration;
        }
    }


    internal class VariableLiteralString : VariableLiteral
    {
        private string MyString { get; set; }

        internal VariableLiteralString(object myObject) : base(myObject)
        {
            this.MyString = (string)this.MyObject;
        }

        public override string GetLiteral()
        {
            var declaration = "\"" + this.MyString.Replace("\\","\\\\").Replace("\n", "\\n").Replace("\r", "\\r").
                                  Replace("\"", "\\\"").Replace("\v", "\\v").Replace("\t", "\\t").
                                  Replace("\'", "\\\'") + "\"";

            return declaration;
        }
    }

    internal class VariableLiteralChar : VariableLiteral
    {


        internal VariableLiteralChar(object myObject) : base(myObject)
        {

        }

        public override string GetLiteral()
        {
            string declaration;
            if ((char)this.MyObject == '\n')
                declaration = "\'\\n\'";
            else
                declaration = "\'" + this.MyObject + "\'";


            return declaration;
        }
    }

    internal class VariableLiteralFloat : VariableLiteral
    {


        internal VariableLiteralFloat(object myObject) : base(myObject)
        {

        }

        public override string GetLiteral()
        {
            var declaration = this.MyObject.ToString() + "F";

            return declaration;
        }
    }

    internal class VariableLiteralNull : VariableLiteral
    {


        internal VariableLiteralNull(object myObject) : base(myObject)
        {

        }

        public override string GetLiteral()
        {

            return "null";
        }
    }

    internal class VariableLiteralSimple : VariableLiteral
    {


        internal VariableLiteralSimple(object myObject) : base(myObject)
        {

        }

        public override string GetLiteral()
        {
            var declaration = this.MyObject.ToString();

            return declaration;
        }
    }

    internal class VariableLiteralBool : VariableLiteral
    {


        internal VariableLiteralBool(object myObject) : base(myObject)
        {

        }

        public override string GetLiteral()
        {
            if ((bool)this.MyObject)
                return "true";
            else
                return "false";

        }
    }
}
