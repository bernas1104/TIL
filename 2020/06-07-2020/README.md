# Today I Learned

So, I started a 10 week program on C# Development. I was one of the 40 people
selected, among 849 other people, to partcipate in this program.
This program uses the Challenge Based Learning (CBL). So, every week we get texts,
slides and video classes about C# topics, and, by the end, we are presented with
a code challenge.

This is our first week.

## AceleraDev - Module I

There are three "modules" (not sure if this is the best word) on the .NET plataform:
- .NET Framework, which only works on Windows;
- .NET Core, which is cross plataform (Windows, Linux and Mac);
- Xamarin, which focuses on cross plataform mobile appliactions (Android and iOS);

Microsoft intends to unify all of these with the .NET 5 launch, but this is all
trivia. Let's start with the actual important stuff.

### Pointers

C# accepts the of pointers. Initially, I thought that this was not possible, but
it is similar to C/C++. We can define a pointer by using the sintax:

```csharp
type* identifier;
void* identifier;  // allowed but not recommended
```

### Conversions

In C#, we can use Implicit or Explicit conversions.

On Implicit Conversion, there's no special syntax. It can be done when the stored
value can be adjusted to the variable without being rounded or truncated.

On Explicit Conversion, we need to use the 'cast' mechanism. Here, there might be
losing of information or even exceptions. The syntax of the 'cast' mechanism is
the same as C/C++:

```csharp
varInt = (int)varLong;  // Performs the casting of a Long to an Int
```

When trying to convert a String to a number (int, float, double), there's a safe
casting method called TryParse. The Parse method also works, but it won't handle
exceptions.
