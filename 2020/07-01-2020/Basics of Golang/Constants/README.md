# Today I Learned

## Golang Constants

On the next part of the freeCodeCamp tutorial on Golang of Beginners, we'll be
learning about the contants. Specifically:

- Naming convention
- Typed Constants
- Untyped Constants
- Enumerated Constants
- Enumeration Expressions

### Naming Convention

To declare constants in Golang, we can use the "const" keyword. Normally, we use
all upper case letters for constants, but, in Golang, a variable that starts with
upper case, means that it'll be exported. So, we'll declare constants the same
way we declare any other variables: Pascal (if we want to export) and Camel case.

### Typed Constants

We can declare types, just as any other variable.

```go
const myConst int = 42
```

Constants also need to be determined at compile time, which means, you can't
assign a return of a function to a constant, because the compiler won't know the
value the function will return.

Arrays cannot be defined as constants, since they are inherently mutable.

As "normal" variables, they can be shadowed, i.e., the inner scope has priority.
It's not recommended to use this mechanism with constants, since it can lead to
mistakes.

### Untyped Constants

The compiler can also infer the type of a constant in much the same way a normal
variable. But, if we try to do something like:

```go
const a = 42                          // an integer type
var b int16 = 27                      // a 2 byte integer type
fmt.Prinntf("%v, %T\n", a + b, a + b) // tries to add them
```

The compiler won't show an error. This is because the compiler sees "42", not a
variable with value "42". In this case, it infers that the constant is the same
byte size as the variable.

### Enumerated Constants

We can declare constants as values of a "counter". To do this, we can write:

```go
const a = iota  // a = 0 of type int
```

If we need to declare a series of constants which increment in value, we can do:

```go
const (
  a = iota  // a = 0 of type int
  b         // b = 1 of type int
  c         // c = 2 of type int
)
```

The compiler infers the pattern of increment. The iota values are scopped to each
block. If we used another iota in a different block, the value would start at 0
again.

The enumerators are a good option to assign types to some structure or something
similar. But, if we were to assign types starting at zero, this could lead to some
problems, since the default value of a non-initilized variable is also zero. We
could use the zero value of iota to assign a error type or, if we don't care about
the zero value, we could use a "_" to ignore the zero value.

We could also attribute some starting value to iota. For instance, we could do
"a = iota + 5" and this would make our iota count start at 5. We can also apply
bitwise operators and shift operators at the iota function.
