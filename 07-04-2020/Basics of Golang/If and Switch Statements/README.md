# Today I Learned

Continuing the Golang tutorial, we saw some stuff about IF-ELSE-ELSE IF statements,
which are basically the same as any other language, except that, in Golang, the
predicate does not have parentheses, have short circuiting and always need curly
braces, even if it will execute only one line of code.

We also saw somethings about SWITCH statements and this is where things got
interesting.

## Switch Statements

In Golang, a SWITCH statement also doesn't use parentheses, but, more interestingly,
there's no "break" keyword or falling through. To achieve the same behaviour as
the falling through mechanism ("case x: case y: some_code; break;"), we can perform
multiple tests in a single case, but we cannot have overlaping tests, otherwise
the compiler will throw an error.

```go
switch 3 {
  case 1, 5, 10:
    fmt.Println("one, five or ten")
  case 2, 4, 6:
    fmt.Println("two, four or six")
  default:
    fmt.Println("another number")
}

// > another number
```

Go offers another SWITCH syntax, which can be very useful in some cases. It goes
as follows:

```go
i := 10   // defines some int value
switch {  // no variable/value is used with the switch keyword
  case i <= 10 :
    fmt.Println("less than or equal to 10")
  case i <= 20 :
    fmt.Println("less than or equal to twenty")
  default :
    fmt.Println("greater than twenty")
}

// > less then or equal to 10
```

It can be used as substitute to very complex IF-ELSE-ELSE IF statements. Also,
in this type of switch, overlaps can happen and the first true test will execute.

Since we don't have the fall through in Go and, sometimes, we might want a multiple
cases to execute, we can use the keyword "fallthrough":

```go
i := 10   // defines some int value
switch {  // no variable/value is used with the switch keyword
  case i <= 10 :
    fmt.Println("less than or equal to ten")
    fallthrough
  case i <= 20 :
    fmt.Println("less than or equal to twenty")
  default :
    fmt.Println("greater than twenty")
}

// > less than or equal to ten
// > less than or equal to twenty
```

The last case of SWITCH is a Type Switch. We can test if a variable is of a given
type:

```go
var i interface{} = 1
switch i.(type) {
  case int:
    fmt.Println("i is an int")
  case float64:
    fmt.Println("i is an float64")
  case string:
    fmt.Println("i is a string")
  default:
    fmt.Println("i is another type")
}

// > i is an int
```

Moving on, there might be some cases where you want to leave a case early. To do
this, you actually use the "break" keyword.

Disclaimer: while evaluating switch types, if you check for an array type, it'll
only evaluate to true if the array is the same size as the type you're checking
against. For example:

```go
var i interface{} = [3]int{}
switch i.(type) {
  case [2]int:
    fmt.Println("Size 2 int array")
  case [3]int:
    fmt.Println("Size 3 int array")
  default:
    fmt.Println("i is another type")
}

// > Size 3 int array
```
