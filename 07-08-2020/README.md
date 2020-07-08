# Today I Learned

The next subject on the tutorial is pointers:

- Creating pointers;
- Dereferencing pointers;
- The "new" function;
- Working with **nil**
- Types with internal pointers

## Creating Pointers

When working with variables, if you declare a varible, assign a value to it,
create another variable, assign the value of the first variable to it and print
both, you'll get the same value printed twice.

```go
a := 42
b := a
fmt.Println(a, b) // > 42 42
```

The above code is allocating two addresses on memory to store the value 42, once
for "a" and once for "b". When you assign "a" to "b", you are copying the **value**
42 from one variable to another.

For pointers, things are a little different. Suppose above code with some tweaks:

```go
var a int = 42
var b *int = &a
fmt.Println(a, b) // > 42 0x1040a124 (random memory address value!!)
```

What we are doing now is creating a pointer to an integer and making that pointer
point to the address of "a". In this example, "b" doesn't hold the value of "a",
but the address of the variable "a".

## Dereferencing pointers

Now, suppose you'd like to access the value of "a" through "b". That's possible
by using the "dereference" operator "\*".

```go
var a int = 42
var b *int = &a
fmt.Println(a, *b)  // > 42 42
```

Using the dereference operator returns the value that the memory address, your
variable points to, holds.

Unsurprisingly, if we were to change the value of the variable "a" and then print
the value of "a" and the dereferenced value of "b", we would get the same value.
The same is true if we would assign some value to the dereference of "b".

```go
var a int = 42
var b *int = &a
fmt.Println(a, *b)  // > 42 42
a = 27
fmt.Println(a, *b)  // > 27 27
*b = 14
fmt.Println(a, *b)  // > 14 14
```

Golang does not allow the use of pointer arithmetic (like in C, for example). If
you'd try:

```go
a := [3]int{1, 2, 3}
b := &a[0]
c := &a[1] - 4  // pointer arithmetic, should point to the same address as 'b'
fmt.Println("%v %p %p\n", a, b, c)
```

The compiler would throw an error. This kind of arithmetic is invalid because Golang
tries to be a very simple language and, allowing pointer arithmetic, could lead
to very complicated code. If you absolutely needs to do it, you can use the "unsafe"
package.

## The "new" function

Using the "new" function is another way to create pointers in Golang. You can use
to instantiate values or "objects" (like structs), but you can't initialize. So,
for example, if you use the function to create a pointer to a struct, the fields
of that struct would not be initialized.

```go
func main() {
  var ms *myStruct
  ms = new(myStruct)
  fmt.Println(ms) // > &{0}
}

type myStruct struct {
  foo int
}
```

Uninitialized pointers hold a "\<nil\>" value. It is best practise that, if a
function accepts a pointer, you should check if it is a "nil" pointer.

Although it is unexpected, we don't need to use "(*myPointer).field" to access the
fields on a pointer that points to a struct. We could simply write "myPointer.field"
and the compiler will understand what we are not trying to access a field of a
pointer, but trying to dereference the pointer and access the struct field.
