using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace myNamespace{

[TestClass]
public class myTestClass
{
[TestMethod]
public void myTestMethod0()
{
//No special treatment

var a = 23 ;
var b = 3 ;

Assert.AreEqual("26", VariableLiteral.GetNewLiteral(testFunction(a, b)).GetLiteral());

}

[TestMethod]
public void myTestMethod1()
{
//No special treatment

var a = 1 ;
var b = 2 ;

Assert.AreEqual("3", VariableLiteral.GetNewLiteral(testFunction(a, b)).GetLiteral());

}

[TestMethod]
public void myTestMethod2()
{
//No special treatment

var a = -34 ;
var b = 23 ;

Assert.AreEqual("-11", VariableLiteral.GetNewLiteral(testFunction(a, b)).GetLiteral());

}

}
}