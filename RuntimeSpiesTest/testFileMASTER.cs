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
var d = "Hi \n \" \\ \v \t \r" ;

Assert.AreEqual("Hi \n \" \\ \v \t \r26", VariableLiteral.GetNewLiteral(testFunction(a, b, d)).GetLiteral());

}

[TestMethod]
public void myTestMethod1()
{
//No special treatment

var a = 1 ;
var b = 2 ;
var d = "Hello\nHello\n" ;

Assert.AreEqual("Hello\nHello\n3", VariableLiteral.GetNewLiteral(testFunction(a, b, d)).GetLiteral());

}

[TestMethod]
public void myTestMethod2()
{
//No special treatment

var a = 2 ;
var b = 4 ;
var d = "שלום\"שלום" ;

Assert.AreEqual("שלום\"שלום6", VariableLiteral.GetNewLiteral(testFunction(a, b, d)).GetLiteral());

}

[TestMethod]
public void myTestMethod3()
{
//No special treatment

var a = -34 ;
var b = 23 ;
var d = "AAAAA" ;

Assert.AreEqual("AAAAA-11", VariableLiteral.GetNewLiteral(testFunction(a, b, d)).GetLiteral());

}

}
}