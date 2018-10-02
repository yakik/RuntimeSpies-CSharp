using System;
using System.Collections.Generic;
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
            return new VariableLiteralClass(myObject);
        }

         abstract public string GetDeclaration();

     }

   
}
