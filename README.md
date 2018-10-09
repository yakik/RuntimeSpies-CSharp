### The Problem We're trying to solve

When working with Legacy code one of the main problems is we do not have tests. To start refactoring code we better have tests to let us do it with calm.
While integration tests are good, unit tests are much better. Integration tests take a long time and a complicated environment (if that's not the case for you - use the integration tests). Unit tests are fast and do not need any special environment. When we code we run our unit tests every 2-3 minutes if not more, and it takes them under a second to complete.
Writing unit tests for legcy code isn't easy. One of the problems is that the code is usually entangled in many other pieces of code, which makes harnessing it inot a unit tests very difficult.
The problem we're trying to solve here is to help harnessing legacy code into a unit test.

### Runtime Spies - CSharp
The Runtime Spies are pieces of code you add to your code (on a side branch - it doesn't go into production) which capture traffic going into and out of the code you want to test (is it one function? A set of function? That's for you to decide).
After adding the code you run scenarios in the integration environenment. The Runtime spies capture the traffic and produce a harness for the unit test.

Currently Runtime Spies works well with simple parameters: primitives, arrays, lists, classes setup with straightforward properties. Odds are you will need to do some refactoring to get to the situation where it works. This is how it goes: to even start having some automated tests you need to do some refactoring. The refactoring you should do when not having automated tests should be very simple: do them one small step at a time, testing as much as you can. 

## Main Features
- Capture the arguments sent into a function and producing a harness to simulate the call.

### example (see explanation below)
This is taken from one unit test from the project. 
```cs
[public class RuntimeSpyTests
    {
        private string harness = "";

        private int[] testFunction(int a, int b) //This is function we want to create unit test harness for
        {
            // this is the bit of code (in this simple example it is bigger than the functioin itself...) we add to the original function, on a side branch
            var mySpy = new RuntimeSpy(); //Initialize the RuntimeSpy
            mySpy.HowToInstantiateMethodClass = "//No special treatment\n"; //Code to instantiate the class that the tested methid is on
            //In this case we don't need to instantiate any class as the function is accessible directly.
            //In another case it might be var a = new testedClass();
            mySpy.HowToCallMethod = "testFunction"; //How to call the method. Here it is simple.
            //In another case it would be a.testedmethod
            mySpy.SetMethodParameters(MethodBase.GetCurrentMethod(), a, b); //first parameter always MethodBase.GetCurrentMethod()
            //the other parameters are the parameters of the tested method, in the right order
           // End out bit of code
           
            var c = a + b; //original function
            
            mySpy.setMethodReturnValue(c); //this to assert the return value of the method
            mySpy.addToTestFile("myTestMethod", "myTestClass", "myNamespace", "testFile.cs"); // this creates a file "testFile.cs" with a test class called "myTestClass" (namespace is "myNameSpace"). The test method name will start with "myTestMethod". If this runs several times several test methods will be created with a sequence at the end ("myTestMethod0", "myTestMethod1" etc.)
            
            return c; //original functioin
        }

       [TestMethod]
        public void TestHarness()
        {
            RuntimeSpySequence.Reset();
            File.Delete("testFile.cs");
            testFunction(23, 3);
            testFunction(1, 2);
            testFunction(-34, 23);
            FileAssert.AreEqual(@"testFile.cs", @"..\..\testFileMASTER.cs");
            //All that's left to do is to add testFile.cs to your test project, open it and add the required using statements
        }
    }
```

### Known Limitations
This is to be use only in a testing environment. Not all Csharp types are supported.

Contact me for any querie or comment: yaki.koren@gmail.com or yaki@agilesparks.com

#### copyright notice

Copyright (C) 2018 Yaki Koren
 
Redistribution, modification and use of this source code is allowed. You do need to mention the copyright.
This software is intended to be used in a test environment only.
