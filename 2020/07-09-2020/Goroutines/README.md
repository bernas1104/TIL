# Today I Learned

The second to last subject of the tutorial is the use of concurrency in Golang,
by using the "Goroutines".

- Creating goroutines
- Synchronization
  - WaitGroups
  - Mutexes
- Parallelism
- Best practices

## Creating Goroutines

To create a goroutine, all we need to do is add the keyword "go" before calling
a function:

```go
func main() {
  go sayHello()         // Creates a goroutine
}

func sayHello() {
  fmt.Println("Hello")
}
```

By adding the "go" keyword, we create what is called a "Green Thread". Usually,
applications use "OS Threads" which are very expensive to create and destroy, and
we tend to avoid them. The "Green Thread" model creates an abstraction of a thread
(goroutine) that the go runtime scheduler will manage and assign processing time
to the OS Threads that are available. By doing this, we are not interacting with
the OS Threads.

The advantage is that a goroutine has much less overhead and the application can
start with a very little stack, since it is very quick and cheap to create and
destroy.

The above code, when executed, will not print the "Hello" string. This is because,
since we are not wating for the goroutine to execute, if the program exits (which
is also a goroutine itself), then all goroutines will be destroyed regardless.

It's important, when using goroutines, to avoid using the concept of closure. Take,
for example, the code:

```go
var msg = "Hello"
go func() {
  fmt.Println(msg)
}
msg = "Goodbye"
time.Sleep(100 * time.Millisecond)  // This is horrible!
```

The above code will, probably, print "Goodbye" instead of "Hello". By writing this
code, we created a race condition. Sometimes the goroutine will execute before the
'msg = "Goodbye"' is executed, sometimes it won't, so we can guarantee the result
of the Println statement.

To avoid this problem, in this example, we can pass the "msg" variable as an
argument to the goroutine. This will create a copy of the value, thus, eliminating
the race condition.

Now, in both examples, we used a "Sleep" call, which is not a good idea.

## Synchronization

To synchronize your goroutines, you can use one of two options: WaitGroups or
Mutexes.

### WaitGroups

The usage of WaitGroups is pretty simple: you create a WaitGroup instance, Add
a value to the WaitGroup counter, let the WaitGroup know when a goroutine is Done
and Wait for the counter to reach 0.

```go
var wg = sync.WaitGroup{} // Creates the WaitGroup

func main() {
  var msg = "Hello"
  wg.Add(1)               // Tells that there's a goroutine that will execute
  go func(msg string) {
    fmt.Println(msg)
    wg.Done()             // Tells the WaitGroup the goroutine is done
  }(msg)
  msg = "Goodbye"
  wg.Wait()               // Waits until all the goroutines are done
}
```

In this simple example, the WaitGroup works great because we don't access any shared
variables. If that was the case, we would have a race condition again.

### Mutexes

A simple way to synchronize goroutines is using Mutexes. Mutexes are locks that
prevent some code from being executed by another thread, until the current one is
executing it. There are simple Mutexes that lock the code for reads AND writes,
there are RWMutexes that lock the code only for writes.

## Parallelism

Take the example:

```go
var wg = sync.WaitGroup{}
var counter = 0
var m = sync.RWMutex{}

func main() {
  runtime.GOMAXPROCS(100)             // Set the value of threads to 100
  for i := 0; i < 10; i++ {
    wg.Add(2)
    m.RLock()
    go sayHello()
    m.Lock()
    go increment()
  }
  wg.Wait()
}

func sayHello() {
  fmt.Println("Hello #%v\n", counter)
  m.RUnlock()                         // Read unlock
  wg.Done()
}

func increment() {
  counter++
  m.Unlock()                          // Read/Write unlock
  wg.Done()
}
```

The above example works as intended (i.e. prints Hello #0 through 9). To make it
work properlly, we request the locks within the main function and only release it
when the goroutine is done processing. In other words, we're requesting the locks
in a single context. But, by doing so, we're basically working in a single threaded
way. All the advantages from multi-threaded are lost.

## Best Practices

- Don't create goroutines in libraries
  - Let consumer control concurrency
- When creating a goroutine, know how it will end
  - Avoids subtle memory leaks
- Check for race conditions at compile time
  - The compiler has a "-race" flag that helps finding race conditions!

