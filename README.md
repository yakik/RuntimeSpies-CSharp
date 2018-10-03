### The Problem We're trying to solve

When working with Legacy code one of the main problems is we do not have tests. To start refactoring code we better have tests to let us do it with calm.
While integration tests are good, unit tests are much better. Integration tests take a long time and a complicated environment (if that's not the case for you - use the integration tests). Unit tests are fast and do not need any special environment. When we code we run our unit tests every 2-3 minutes if not more, and it takes them under a second to complete.
Writing unit tests for legcy code isn't easy. One of the problems is that the code is usually entangled in many other pieces of code, which makes harnessing it inot a unit tests very difficult.
The problem we're trying to solve here is to help harnessing legacy code into a unit test.

### Runtime Spies - CSharp
The Runtime Spies are pieces of code you add to your code (on a side branch - this doesn't go into production) which capture traffic going into and out of the code you want to test (is it one function? A set of function? That's for you to decide).
After adding the code you run scenarios in the integration environenment. The Runtime spies capture the traffic and produce a harness for the unit test.

Then you should start refactoring.

## Main Features
- Capture the arguments sent into a function and producing literals that can be used in a unit test harness to simulate the call.

### example (see explanation below)
This is taken from one unit test from the project. 
```cs
[TestMethod]
public void PrimitiveAndClassTests()
{
  var a = new TestClass
  {
    FieldA = 4,
    FieldB = "Hello\nHello",
    FieldC = true,
    FieldD = '\n',
    FieldE = 123443435465,
    FieldF = 123.453F
  };
  var myDeclaration = VariableLiteral.GetNewLiteral(a).GetLiteral(); //That's the API currently supported

  Assert.AreEqual("new TestClass {FieldA = 4,FieldB = \"Hello\\nHello\",FieldC = true,FieldD = '\\n',FieldE = 123443435465,FieldF =       123.453F}",myDeclaration);
}
```
Another example
```cs
[TestMethod]
public void ArrayTests()
{
  int[] myArray = {1, 2, 3, 4, 5};
  var myDeclaration = VariableLiteral.GetNewLiteral(myArray).GetLiteral();
  Assert.AreEqual("new System.Int32[] {1,2,3,4,5}",myDeclaration);
}
```
And last one
```cs
[TestMethod]
public void ListTests()
{
  var a = new TestClass
  {
    FieldA = 4,
    FieldB = "Hello\nHello",
    FieldC = true,
    FieldD = '\n',
    FieldE = 123443435465,
    FieldF = 123.453F
  };
  var b = new TestClass
  {
    FieldA = 4,
    FieldB = "Hello\nHello\nHello",
    FieldC = false,
    FieldD = '\n',
    FieldE = 12653443435465,
    FieldF = 123.47653F
  };

  List<TestClass> myList = new List<TestClass>
  {
    a,
    b
  };

  var myDeclaration = VariableLiteral.GetNewLiteral(myList).GetLiteral();

  Assert.AreEqual("new List<RuntimeSpiesTest.TestClass>{new TestClass {FieldA = 4,FieldB = \"Hello\\nHello\",FieldC = true,FieldD = \'\\n\',FieldE = 123443435465,FieldF = 123.453F},new TestClass {FieldA = 4,FieldB = \"Hello\\nHello\\nHello\",FieldC = false,FieldD = \'\\n\',FieldE = 12653443435465,FieldF = 123.4765F}}",myDeclaration);
}
```
### Known Limitations
This is to be use only in a testing environment. Not all Csharp types are supported.

Contact me for any querie or comment: yaki.koren@gmail.com or yaki@agilesparks.com

#### copyright notice

Copyright (C) 2018 Yaki Koren
 
Redistribution, modification and use of this source code is allowed. You do need to mention the copyright.
This software is intended to be used in a test environment only.
