using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeSpies
{
    internal class VariableLiteralClass : VariableLiteral
    {
        internal VariableLiteralClass(object myObject) : base(myObject)
        {

        }

        public override string GetDeclaration()
        {
            var declaration =  "new " + this.MyObject.GetType().Name + "\n{\n";
            declaration += "FieldA = 4,\nFieldB = \"Hello\"\n}";
            return declaration;
        }
    }
}
