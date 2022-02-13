# Today I Learned

## Basics of Golang: Variables

So, starting on the basics, there's something new already. Golang does variable
declarations in a different way than other languages. To declare variables, we do:

```go
  var variable_name int
```

It goes: var keyword, name and type. It usually goes: type/keyword name. Assigning
is done the normal way. To write less code, we can depend on the compiler to figure
out what type of variable we're using, assuming we assign the value at the same
time the variable is created, by doing:

```go
  i := 42
```

This tells the compiler that the "i" variable is an integer of value 42.

```go
  var i int       // First way is used when
  i = 42          // we're not ready to assign a value

  var j int = 27  // When the Go compile might no have a way to infer correctly
                  // Ex: j := 27.  -->  Infers a float64
                  //     j := 27   -->  Infers a int
                  // There's no way to infer a float32!

  k := 99
```

If we declare a variable at the 'package' level, we must declare its type. Like so:

```go
package main

import (
  "fmt"
)

var i int = 42

func main() {
  // some code
}
```

Another interesting thing we can do with variables is declare a variable block,
helps organize and make the code cleaner. Lik so:

```go
  var(
    actorName string = "Elisabeth Sladen"
    companion string = "Sarah Jane Smith"
    doctorNumber int = 3
    season int = 11
  )
```

Redeclaration of variables in Golang is possible if, and only if, they're in different
scopes. Also, when using a variable that is present in multiple scopes, the variable
with the inner most scope takes precedence.

```go
var i int = 27

func main() {
  fmt.Println(i)  // > 27
  var i int = 42
  fmt.Println(i)  // > 42
}
```

Variables in Golang must be used if declared. If a program is compiled with non
used variables, the compiler will declare an error.

There's some naming conventions in Golang. For instance, if we declare a variable
with a lower case, at the package level, that means that it's visible only inside
the package that it's in. If we use upper case, that tells the compiler that the
variable can be accessed outside it's own package. There's also variables declared
inside blocks, which are seen only to the block and inner blocks.

As any other language, we must apply descriptive names. Variables like "i" are
acceptable when used on loops or things that we don't need to keep track of it's
meaning.

Acronyms must be all upper case. Golang also applies the camel case as convencion.

We can convert variables using the type we want to convert to, like:

```go
var i int = 42
var j float32

j = float32(i) // j is assigned 42 as a float32 number
```

We can convert a number with more information to a number with less information
(like a float32 to a int), but we need to keep in mind that there'll be losing of
information. In any case - from less to more information and vice-versa - we must
explicitly use the conversion function, otherwise we get an error.

To work with conversions of string variables, if we use string(42), for example,
we get a '\*', since 42 is the Unicode value of the '\*'. If we want to convert
the int 42 to a string containing "42", we need to use the .Itoa() function that
is located in the "strconv" package.
