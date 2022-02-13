# Today I Learned

Going in more depth about control flow, the tutorial started talking about:

- Defer;
- Panic;
- Recover;

## Defer

Functions, in any languagem, usually execute one line after the other. For example,
if you write a function:

```go
fmt.Println(1)
fmt.Println(2)
fmt.Println(3)
```

It is no surprise that the program will print 1, 2 and 3. But, Golang offers the
"defer" keyword.

What "defer" does is, it delays the execution of the preceding line, until the end
of the current function, but before it returns.

You can use the "defer" keyword as many times as you want. But it is important to
note that the last defered line, will be the first to execute of all the defered
lines (LIFO order).

This makes sense because defering the execution of a line is usually used to close
resources, which may be open with dependencies of "last-to-first".

It allows to open a resource and "close" it near the opening statement, which makes
the code more readable and hard to introduce bugs.

Defering executions is not recommended if you're working with resources on a loop.

## Panic

In other languages, it is commom to have the program throw exceptions when some
unexpected thing happen, for example when you try to read a file that does not exist.

In Golang, these types of situations are not considered exceptions and, therefore,
there's no exception. But there are situations in Golang that the application
doesn't know what to do. When this happens, the applications throws a Panic.

As is the case with exceptions, we can create our own panics in an application.
To do this, we use the keyword "panic("err msg")".

In the example:

```go
func main() {
  http.HandleFunc("/", func(w http.ReponseWriter, r *http.Request) {
    w.Write([]byte("Hello, Go!"))
  })
  err := http.ListenAndServer(":8080", nil)
  if err != nil {
    panic(err.Error())
  }
}
```

Normally, the application would just return a simple error. By adding the "panic"
function, we're saying that, if the application can't listen and server requests
on the 8080 port, it is a panic situtation, since the web application won't function
at all.

Observation: in the order of execution of a function we have "normal lines" ->
defered lines -> panic -> return. This is important because, even if the application
panics, it can close resources (defered lines) before it completly exits.

## Recover

It works similarly to a try/catch statement. If you program throws a panic, you
can try to recover from it. Also, if you see that you can't handle the panic,
while trying to recover from it, you can throw a new panic.

If a function panics, it will not be able to continue executing, but functions
higher up on the call stack can, presumably, still execute, and will do so;
