﻿using System;
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
            var a = myObject.GetType();
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

        public abstract string GetDeclaration();

    }

    internal class VariableLiteralList : VariableLiteral
    {
        internal VariableLiteralList(object myObject) : base(myObject)
        {

        }

        public override string GetDeclaration()
        {
            var declaration = "new List<" + this.MyObject.GetType().GetGenericArguments().Single() + ">{";
            var myCollection = (IEnumerable)this.MyObject;
            var listItemIndex = 0;
            foreach (var listItem in myCollection)
            {

                if (listItemIndex > 0)
                    declaration += ",";
                declaration += VariableLiteral.GetNewLiteral(listItem).GetDeclaration();

                listItemIndex++;
            }
            declaration += "}";
            return declaration;
        }
    }

    internal class VariableLiteralClass : VariableLiteral
    {
        internal VariableLiteralClass(object myObject) : base(myObject)
        {

        }

        public override string GetDeclaration()
        {
            var declaration = "new " + this.MyObject.GetType().Name + " {";
            var propertyIndex = 0;
            foreach (var property in this.MyObject.GetType().GetProperties())
            {

                if (propertyIndex > 0)
                    declaration += ",";
                declaration += property.Name + " = " +
                               VariableLiteral.GetNewLiteral(property.GetValue(this.MyObject, null)).GetDeclaration();

                propertyIndex++;
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

        public override string GetDeclaration()
        {
            var declaration = "new " + this.MyArray.GetType().GetElementType() + "[] {";
            var elementIndex = 0;
            foreach (var element in this.MyArray)
            {

                if (elementIndex > 0)
                    declaration += ",";
                declaration += VariableLiteral.GetNewLiteral(element).GetDeclaration();

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

        public override string GetDeclaration()
        {
            var declaration = "\"" + this.MyString.Replace("\n", "\\n") + "\"";

            return declaration;
        }
    }

    internal class VariableLiteralChar : VariableLiteral
    {


        internal VariableLiteralChar(object myObject) : base(myObject)
        {

        }

        public override string GetDeclaration()
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

        public override string GetDeclaration()
        {
            var declaration = this.MyObject.ToString() + "F";

            return declaration;
        }
    }

    internal class VariableLiteralSimple : VariableLiteral
    {


        internal VariableLiteralSimple(object myObject) : base(myObject)
        {

        }

        public override string GetDeclaration()
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

        public override string GetDeclaration()
        {
            if ((bool)this.MyObject)
                return "true";
            else
                return "false";

        }
    }
}
