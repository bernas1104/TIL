# Today I Learned

We're getting near the end of the tutorial. We'll be learning about interfaces
next. The video argues that interfaces are the reason that the Golang has proven
to be as scalable and maintainable as it is. Here are the topics:

- Basics
- Composing interfaces
- Type conversion
  - The empty interface
  - Type switches
- Implementing with values vs. pointers
- Best practices

## Basics

The declaration of interfaces is similar to structs. They are used not to describe
data, but to describe behaviours. So, when using an interface, we'll be storing
methods defintions inside a interface.

```go
type Writer interface {
  Write([]byte) (int, error)  // A method that takes a slice of bytes and returns
                              // an integer and an error
}
```

To implement the interface, we'll use an implicit implementation. In other languages,
the implmentation is explicit, you need some "implements" keyword or something
similar. In Golang, you'll do as follows:

```go
type Writer interface {
  Write([]byte) (int, error)  // A method that takes a slice of bytes and returns
                              // an integer and an error
}

type ConsoleWriter struct {}  // Defines a ConsoleWriter struct

/*
 * Defines the implementation of the Write method of the Writer interface in the
 * ConsoleWriter struct
 */
func (cw ConsoleWriter) Write(data []byte) (int, error) {
  n, err := fmt.Println(string(data))
  return n, err
}
```

As expected, the interface abstracts information. So, if you were to create an
instance of the "ConsoleWriter" struct, you, now, could do:

```go
var w Writer = ConsoleWriter{}
w.Write([]byte("Hello, Go!"))
```

Although "ConsoleWriter" is not of type "Writer", it does implement the "Writer"
interface. The compiler understands the abstraction and allows to ommit the "real"
type. With this, you achieve a polymorphic behaviour.

There's also a naming convention to interfaces. If you are creating an interface
that defines the signature of just one method, you should name it with the method
name plus "er". For example, the code above defines the "Write" method, so we named
the interface "Write**r**". If we had created an interface for a "Read" method, we should've named it "Read**er**". For interfaces with multiple methods, you should
always choose a name that describes the behaviour of that interface.

You don't need to use structs to work with interfaces. Interfaces work with any
types that can implement methods.

## Composing interfaces

Another possibility is to compose interfaces. You can do that the same way you
embed structs. When you compose interfaces, if you try to assign a type that
implements the composed interface, it must implement all the interfaces that
compose that interface.

```go
func main() {
  var wc WriterCloser = NewBufferedWriterCloser()
  wc.Write([]byte("Hello, this is a test"))
  wc.Close()
}

type Writer interface {
  Write([]byte) (int, error)
}

type Closer interface {
  Close() error
}

type WriterCloser interface {
  Writer  // Composing
  Closer  // Composing
}

type BufferedWriterCloser struct {
  buffer *bytes.Buffer
}

// It prints the 'data' in characters increment
func (bwc *BufferedWriterCloser) Write(data []byte) (int, error) {
  n, err := bwc.buffer.Write(data)
  if err != nil {
    return 0, err
  }

  v := make([]byte, 8)
  for bwc.buffer.Len() > 8 {
    _, err := bwc.buffer.Read(v)
    if err != nil {
      return 0, err
    }
    _, err = fmt.Prinln(string(v))
    if err != nil {
      return 0, err
    }
  }

  return n, nil
}

// Prints the leftover data
func (bwc *BufferedWriterCloser) Close() error {
  for bwc.buffer.Len() > 0 {
    data := bwc.buffer.Next(8)
    _, err := fmt.Println(string(data))
    if err != nil {
      return err
    }
  }

  return nil
}

// Retunrs a pointer to a BufferedWriterCloser with the "buffer" field initialized
func NewBufferedWriterCloser() *BufferedWriterCloser {
  return &BufferedWriterCloser {
    buffer: bytes.NewBuffer([]bytes{}),
  }
}
```

## Type conversion

As said before, if you compose interfaces and you try to assign the composed
interface as type for a variable, that variable must implement the all the parts
that compose the interface. So:

```go
func main() {
  var wc WriterCloser = NewBufferedWriterCloser()
  wc.Write([]byte("Hello, this is a test"))
  wc.Close()

  bwc := wc.(*BufferedWriterCloser)
  fmt.Println(bwc)  // just to use the variable bwc
}
```

If you do the example above, you can convert the WriterCloser type to a
BufferedWriterCloser type, since the BufferedWriterClose implements WriterClose.
This makes any fields of the BufferedWriterCloser struct available to "bwc".

Now, suppose you try to convert when you are not sure if the variable implements
a given interface. If your variable doesn't implement, your program will panic,
and thats not good. What you can do is check if that conversion is valid:

```go
r, ok := wc.(io.Reader)
if ok {                             // Checks if the conversion is valid
  fmt.Println(r)
} else {                            // If invalid
  fmt.Println("Conversion failed")
}
```

### The Empty Interface

It allows to convert anything to its type, since it does not define any methods.
Even primitive types can be converted to an empty interface type. It can be useful
when you are working with multiple types and are not sure what type you are getting
on a given moment. But, there's a catch: since the interface is empty, the converted
variable won't have any methods avaible to it. So, to work with the type you'll
need to convert it to another interface that implements some methods or used the
"reflect" package to figure out what type you're dealing with.

## Implementing with values vs. pointers

It is important to note that when you implement the methods of an interface, if
you use a value type, the methods that implement the interface have to all have
value receivers. If you're implementing with a pointer, the receiver type does not
matter.

## Best Practises

- Use many, small interfaces;
- Don't export interfaces for types that will be consumed;
- Do export interfaces for types that will be used by package;
- Design functions and methods to receive interfaces whenever possible;
