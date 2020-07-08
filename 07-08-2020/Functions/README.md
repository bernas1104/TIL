# Today I Learned

The next topic of the tutorial is: functions. I'll probably take notes just on a
few parts of the video, since functions are pretty standard in most languages.

- Basic Syntax;
- Parameters;
- Return values;
- Anonymous functions;
- Functions as types;
- Methods.

## Parameters

The first interesting thing about functions in Golang is that, if we have a list
of function parameters that share the same type, we don't need to type that type
for every single parameter. Example:

```go
func myFunction(arg1 string, arg2 string) {
  // some code...
}
```

This can actually be rewritten like:

```go
func myFunction(arg1, arg2 string) {  // Since both arguments are strings!
  // some code ...
}
```

We can also create functions that take a variable number of parameters. To do this
we can use the the following:

```go
func myFunction(values ...int) {
  // some code
}
```

What this does, is create a slice of all the values of the type (in this case, int)
passed. The slice will be available to the function to work with the variable data.
There's a catch: the variable data slice must be the last parameter of the function
because the compiler won't be able to diferentiate a variable data parameter from
any other parameters.

The return type of a function goes between the parameters and the opening curly
braces, and to return, we use the "return" keyword, as expected. We can also return
a pointer to local function variable, which is kind of weird. This is achived by
the compiler. It understands that you returning a pointer to a local variable and
promotes that variable to the Heap, instead of keeping it on the Stack.

Golang also allows you to do a "named return" by defining it on the function
signature:

```go
func myFunction() (result int) {
  // some code
  return        // returns the local variable "result"
}
```

Also, we can do multiple returns in Golang! It's very useful when dealing with
errors - to avoid panic the application. We can return an "error" type and then
we can check "if err != nil" to see if an error has occurred. To return the error,
we use "fmt.Errorf("msg")". If no error occurs, we return "nil".

When the function return multiple values, we need to write them all on the left
side, separated by commas.

Similarly to JavaScript, functions are "first-class citizens". We can assign them
to variables, pass them as parameters, return them...

## Methods

We can also create methods in Golang. Methods are functions that are assotiated
with a specific type of variable/struct.

```go
func main() {
  g := greeter {
    greeting: "Hello",
    name: "Go"
  }
  g.greet()                       // > Hello Go
}

type greeter struct {
  greeting string
  name string
}

func (g greeter) greet() {
  fmt.Println(g.greeting, g.name)
}
```

In the above exmaple, we create a method to the greeter struct, which can be called
by adding a ".greet()" in front of the g variable, that holds the struct value.
Also, in this example we are using a copy of the fields of the "g" struct. But,
we can also define a method to work by reference (pointers).
