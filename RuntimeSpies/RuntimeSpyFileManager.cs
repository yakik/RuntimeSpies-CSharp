using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;

namespace RuntimeSpies
{
    public class RuntimeSpyFileManager
    {
        public static string GetTestFileCode(string fileName)
        {
            try
            {
                string allFileContent = File.ReadAllText(fileName);
                return allFileContent;
            }
            catch (IOException)
            {
                return "EMPTY";
            }
        }

        public static void UpdateTestFile(string fileName, string code)
        {
            File.WriteAllText(fileName, code);
        }
    }
}
