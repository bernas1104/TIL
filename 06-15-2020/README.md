# Today I Learned

On the second week of the Codenation program, we were given a link to the Microsoft
Unit Testing documentation, even though the video classes didn't mention any of
it.

I decided to take a look at the documentation and here's what I thought was interesting
to write down.

## C# Unit Testing

There are, at least, three unit testing frameworks for .NET Core (and the other
.NET products as well): xUnit (which is used on the Codenation challenges), NUnit
and MSTest.

Although I've already seen this, I thought that the way the used to explain was
cool. Tests are important because they help with regression, documentation and
less coupled code.

- Regression: are defects introduced when a change is made to the application. With
unit testing, we can re-run all the tests to ensure that no regression defects were
introduced, even with a single line of change.
- Documentation: if your tests are well built, it can help with the application's
documentation. Developers can look at the test cases and see how a given set of
inputs will make the code behave, thus helping them better understand the code.
- Less coupled code: in almost every unit test, you can find dependencies to other
parts of your application. By creating unit tests, you are forced to think about
the design choices of your system, favoring thoses that embrace best practices and,
by doing so, your code becomes less coupled.

## Fakes, Mocks and Stubs

By the Microsoft's documentation definition:

- Fake: "A fake is a generic term
which can be used to describe either a stub or a mock object. Whether it is a
stub or a mock depends on the context in which it's used. So in other words, a
fake can be a stub or a mock." When I code unit tests on TypeScript applications,
I tend to use the "stub" definition;
- Mock: "A mock object is a fake object in the system that decides whether or not
a unit test has passed or failed. A mock starts out as a Fake until it is asserted
against."
- "A stub is a controllable replacement for an existing dependency (or collaborator)
in the system. By using a stub, you can test your code without dealing with the
dependency directly. By default, a fake starts out as a stub."

## Best Practices in Unit Testing

The documentation pointed out some interesting information on how we should code
our unit tests.

### How to name tests?

The name of the test should consist of three parts:
- Name of the method being tested;
- Scenario of the tests;
- Expected behaviour.

### How to write the tests?

It should follow the: Arrange, Act and Assert pattern
- Arrange your objects, creating and setting them up as necessary;
- Act on your object;
- Assert that something is as expected.

### What else?

We should avoid the use of logic - if, while, for, switch - on our tests, since
this can lead to bugs INSIDE the tests. To test multiple values on a given method,
we should use the [Theory] annotation, followed by the [InlineDate] annotations.

We also should avoid multiple "asserts" on a test. Once the first asserts fails,
the others won't be executed. This fogs our understing of the results.

If our code depends on something that is out of our control (like a DateTime.Now
call), we should use a "seam". This is done by creating an interface for the
dependency and implementing a fake dependency that we can control.

The following [link](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)
is a step by step tutorial to create you first xUnit test.
