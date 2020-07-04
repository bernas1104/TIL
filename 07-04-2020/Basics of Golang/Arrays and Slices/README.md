# Today I Learned

Moving forward, we be learning the basics of arrays and slices on Golang. The topics
we'll cover are:

- Arrays:
  - Creation;
  - Built-in functions;
  - Working with arrays.
- Slices:
  - Creation;
  - Built-in functions;
  - Working with slices.

## Arrays

To declare arrays in Golang, we can use [] as any other language, but the order
is a litter different (as any other types of variables/constants in Golang):

```go
grades := [3]int{97, 85, 93}  // Declares an array of size 3 and integer type
                              // And holds the values: 97, 85 and 93
```

If you print the array variable, it'll print all the values stored on it, like so:

```go
fmt.Printf("Grades: %v", grades)  // > Grades: [97, 85, 93]
```

If we initialize the values of the array, we can use "[...]type{values}". That tells
the compiler to create an array that is just big enought to hold the values informed.

To get the length of an array, we can use the "len(arr)" function.

In Golang, arrays are treated as values. What this means is that, if you create
an array "a" and an array "b", then assign "b" equals "a" and change, let's say,
the 2nd value of "b" and print both arrays, you'll see that you get two different
arrays:

```go
a := [...]int{1, 2, 3}
b := a
b[1] = 5
fmt.Println(a)  // [1, 2, 3]
fmt.Println(b)  // [1, 5, 3]
```

If you are assign small arrays, that's probably fine, but if you start dealing with
big arrays (millions), that could slow things down. If, on the other hand, you
want to avoid the copy, you could assign to a reference (pointers):

```go
a := [...]int{1, 2, 3}
b := &a
b[1] = 5
fmt.Println(a)  // [1, 5, 3]
fmt.Println(b)  // &[1, 5, 3]
```

The problem with arrays is that they have a fixed size (just like in C/C++). The
most common use for arrays is to back slices.

## Slices

Slices are declared in a very similar way to arrays and you can do almost everything
just as you'd do with arrays. To declare them:

```go
a := []int{1, 2, 3} // > [1, 2, 3]
```

Like arrays, slices also have the "len(sli)" function. There's also a capacity
function (cap(sli)) that returns the capacity of a slice. The length and the
capacity are different things and not always will return the same value.

Slices, unlike arrays, are reference types, which means that, if you assign one
slice to another, it'll pass the pointer to that slice.

When assign slices, you can pass only part of the slice. Here are some examples:

```go
a := []int{1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
b := a[:]   // Slice of all elements
c := a[3:]  // Slice of the 4th element to end
d := a[:6]  // Slice first 6 elements
e := a[3:6] // Slice 4th, 5th and 6th elements
```

It's important to remember that the first number is inclusive and the second number
is exclusive.

When you creating an slice, you can use the "make" function, which takes a type,
the size of the slice and the capacity. This is because slices don't have a fixed
size. We can add elements (append(sli, elem)) to the slice. When you append an
element, Golang will copy the existing array and create a new one to store the value.
Again, this is fine when dealing with small slices, but can get problematic if we
start dealing with larger slices. That is way we can take advantage of that third
argument of the "make" function. If we know that our slice will store many values,
despite the fact that it'll begin with just a few, we can avoid the copying operations
and make our application more efficient. Also, the "append" function can take 2
or more arguments. Everything after the source argument, will be appended to the
slice.

Now, suppose you want to concatenate two slices together. You can't simply do
something like:

```go
a = append(a, []int{1, 2, 3})
```

The compiler won't allow this, since it spects a integer to be appended and not
a slice of integers. So, to do this we can use the "..." operator (in JavaScript
it is called the "spread operator").

```go
a = append(a, []int{1, 2, 3}...)
```

This will force the slice to be applied on element at a time and the compiler
won't throw an error.
