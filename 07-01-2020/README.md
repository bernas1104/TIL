# Today I Learned

## Golang Primitive Types

On the next part of the freeCodeCamp tutorial on Golang of Beginners, we'll be
learning about the primitive types. Specifically:

- Boolean Type
- Numeric Types
  - Integers
  - Float point
  - Complex numbers
- Text Types

### Boolean Types

Is the same as any other language. It accepts "true" or "false" values and is
declared like so:

```go
  var n bool
  n = false         // n -> false

  var m bool = true // m -> true

  l := true         // l -> true
```

In go, if we declare a variable and don't initialize it, it'll have a zero (0)
value by default. In booleans, this evaluate to "false".

### Integer Types

Golang implements 4 types of integers that differ on their bit size:

- 8 bit: -128 to 127
- 16 bit: -32.768 to 32.767
- 32 bit: -2.147.483.648 to 2.147.483.647
- 64 bit: -9.223.372.036.854.775.808 to 9.223.372.036.854.775.807

If you need numbers that are bigger than 64bit, you'll need to use a package (big).
You can also work with unsigned ints that range from 8 to 32 bit numbers. There's
no 64bit version of an unsigned int, but there is a "byte" type, which is an alias
for 8bit unsigned that is usually used for data streams.

The Golang compiler is very strict about types. So, for instance, if you try to
add two integers of different bit sizes, the compiler will flag an error. You must
convert the integers to the same bit size in order to proceed.

To work with numbers, we can use bit operators. There are four bit operators: AND,
OR, XOR and NAND.

```go
a := 10 // 1010
b := 3  // 0011

fmt.Println(a & b)  // 2  - 0010 it returns '1' if both bits are set
fmt.Println(a | b)  // 11 - 1011 it returns '1' if one of the bits is set
fmt.Println(a ^ b)  // 9  - 1001 it returns '1' if one bit is set and the other isn't
fmt.Println(a &^ b) // 8  - 0100 it returns '1' if neither bits are set
```

There's also bit shift operators ">>" and "<<".

### Float Point Types

It follows the IEEE764 standard with 32 and 64 bit versions:

- 32bit: +/-1.18E-38 to +/-3.4E38
- 64bit: +/-2.23E-308 to +/-1.8E308

We can also use exponential notation while declaring float point numbers. If we
declare a variable using the "x := someNumber" method, it'll initialize the variable
to a float64 type. If you need the float32, you'll need to use the "var x float32"
declaration.

Float types don't have the remainder (%) operator, nor the bitwise (&, |, ^, &^)
or shift operators (>>, <<).

### Complex Numbers Type

It opens the possibility to work with data science. To declare a complex number,
you can use the "complex64" or "complex128" types, like so:

```go
var n complex64 = 1 + 2i // 1 is the real part, 2i is the imaginary part
```

The 64bit and 128bit versions divide the bits between the real and imaginary parts.
So, in a 64bit complex number you'll have float32 to both real and imaginary
(32 + 32 = 64), and in a 128bit complex number you'll have float64 to both real
and imaginary (64 + 64 = 128).

As the float point numbers, complex numbers don't have the remainder (%) operator,
nor the bitwise (&, |, ^, &^) or shift operators (>>, <<).

Suppose you need to operate with only the real or the imaginary parts of the complex
numbers. You can use the "real()" or the "imag()" functions to extract the part
you want.

If you need to create a complex number off of real numbers, you can use the "complex()"
function to do so.

### Text Types

There is two types to work with texts on Golang: String and Rune . The difference
between them is that strings work with UTF-8 encoding, which is very powerfull,
but doesn't cover all types of characters. To work with characters that aren't
present on the UTF-8 encoding, you'll to use the Rune type.

Strings, as expected, work as arrays for characters. But, if we try to print the
nth character of a string (str[n]), we'll get a uint8 result. That's because Golang
treats the characters as bytes. So, if we need to print the actual character, we
must convert the result back to a string:

```go
s := "this is a string"
fmt.Printf("%v, %T\n", string(s[2]), s[2])  // i, uint8
```

Strings are usually immutable. So, if you try to do something like "s[2] = "u"",
you'll get two errors. The first has to do with the immutable property and the second
is that we can't assing a string to a byte. But, there is an operation avaible,
which is the concatenation of strings with the "+" operator.

To make things more flexible, when working with strings, we can convert them to a
slice of bytes. This has many uses in Golang. To convert a string to a slice of bytes
we can do this:

```go
s := "this is a string"
b := []byte(s)
fmt.Printf("%v, %T\n", b, b)  // [116, 104, ..., 103], []uint8
```

The Rune type is used when you need more than UTF-8 encoding. It uses the UTF-32
encoding, which means that it can hold up to 4 bytes of that, but it doesn't have to.
To declare a Rune, you'll need single quotes.

If needed, the tutorial suggests looking at the Golang API for more information
runes are probably used only in very specific cases.
